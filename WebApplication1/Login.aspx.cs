using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = HashPassword(txtPassword.Text);

            string connectionString = ConfigurationManager.ConnectionStrings["SocialMediaPlatformDb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Id, Username, Role FROM Users WHERE Email = @Email AND Password = @Password";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Session["UserId"] = reader["Id"].ToString();
                            Session["Username"] = reader["Username"].ToString();
                            Session["Role"] = reader["Role"].ToString();

                       
                            HttpCookie userCookie = new HttpCookie("UserCookie");
                            userCookie["UserId"] = reader["Id"].ToString();
                            userCookie["Username"] = reader["Username"].ToString();

                       
                            userCookie.Expires = DateTime.Now.AddDays(7);
                            Response.Cookies.Add(userCookie);

                            LogEvent("Login", reader["Username"].ToString());

                            string role = reader["Role"].ToString();
                            if (role == "Admin")
                                Response.Redirect("AdminDashboard.aspx");
                            else
                                Response.Redirect("UserDashboard.aspx");
                        }
                        else
                        {
                            lblMessage.Text = "Invalid email or password.";
                        }
                    }
                }
            }
        }
        private void LogEvent(string eventType, string username)
        {
            string logPath = Server.MapPath("~/log.txt");
            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine($"{DateTime.Now}: {eventType} - User: {username}");
            }
        }
 
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}