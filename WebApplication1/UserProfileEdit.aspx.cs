using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class UserProfileEdit : System.Web.UI.Page
    {
      
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    LoadUserProfile();
                }
            }

            private void LoadUserProfile()
            {
                int userId = Convert.ToInt32(Request.QueryString["UserId"]);
                string connectionString = ConfigurationManager.ConnectionStrings["SocialMediaPlatformDb"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Username, Email FROM Users WHERE Id = @UserId";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtUsername.Text = reader["Username"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                    }
                }
            }

            protected void btnSaveChanges_Click(object sender, EventArgs e)
            {
                int userId = Convert.ToInt32(Request.QueryString["UserId"]);
                string newUsername = txtUsername.Text.Trim();
                string newEmail = txtEmail.Text.Trim();
                string newPassword = txtPassword.Text.Trim();

                string connectionString = ConfigurationManager.ConnectionStrings["SocialMediaPlatformDb"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Users SET Username = @Username, Email = @Email" +
                                   (string.IsNullOrEmpty(newPassword) ? "" : ", Password = @Password") +
                                   " WHERE Id = @UserId";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Username", newUsername);
                    cmd.Parameters.AddWithValue("@Email", newEmail);
                    if (!string.IsNullOrEmpty(newPassword))
                    {
                        cmd.Parameters.AddWithValue("@Password", newPassword);
                    }

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Profile updated successfully!";
                }
            }

            protected void btnCancel_Click(object sender, EventArgs e)
            {
                Response.Redirect("UserDashboard.aspx");
            }
        }
    }
