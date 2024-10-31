using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class UserDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserPosts();
                LoadUserProfile();
            }
        }

        private void LoadUserPosts()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            string connectionString = ConfigurationManager.ConnectionStrings["SocialMediaPlatformDb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT PostId, Content, ImagePath  FROM Posts WHERE UserId = @UserId";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@UserId", userId);

                DataTable postsTable = new DataTable();
                adapter.Fill(postsTable);

                PostsGridView.DataSource = postsTable;
                PostsGridView.DataBind();
            }
        }

        private void LoadUserProfile()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            string connectionString = ConfigurationManager.ConnectionStrings["SocialMediaPlatformDb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ProfilePicturePath, Username, Email FROM Users WHERE Id = @UserId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@UserId", userId);
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ProfilePicture.ImageUrl = reader["ProfilePicturePath"].ToString();
                    lblUsername.Text = reader["Username"].ToString();
                    lblEmail.Text = reader["Email"].ToString();
                }
            }
        }

        protected void UploadProfilePictureButton_Click(object sender, EventArgs e)
        {
            if (ProfilePictureUpload.HasFile)
            {
                string filePath = "~/ProfilePictures/" + Path.GetFileName(ProfilePictureUpload.FileName);
                ProfilePictureUpload.SaveAs(Server.MapPath(filePath));

                int userId = Convert.ToInt32(Session["UserId"]);
                string connectionString = ConfigurationManager.ConnectionStrings["SocialMediaPlatformDb"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Users SET ProfilePicturePath = @ProfilePicturePath WHERE Id = @UserId";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ProfilePicturePath", filePath);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                LoadUserProfile();
            }
        }
        private void DeactivateUser(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SocialMediaPlatformDb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET IsActive = 0 WHERE Id = @UserId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
 


        protected void PostsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditPost" || e.CommandName == "DeletePost")
            {
       
                int rowIndex = Convert.ToInt32(e.CommandArgument);


                if (rowIndex >= 0 && rowIndex < PostsGridView.DataKeys.Count)
                {
                    int postId = Convert.ToInt32(PostsGridView.DataKeys[rowIndex].Value);

                    if (e.CommandName == "EditPost")
                    {
                        Response.Redirect($"EditPost.aspx?PostId={postId}");
                    }
                    else if (e.CommandName == "DeletePost")
                    {
                        DeletePost(postId); 
                        LoadUserPosts(); 
                    }
                }
                //else
                //{
                //}
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserDashboard.aspx");
        }
        private void LogEvent(string eventType, string username)
        {
            string logPath = Server.MapPath("~/log.txt");
            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine($"{DateTime.Now}: {eventType} - User: {username}");
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {

            LogEvent("Logout", Session["Username"].ToString());
     
            Session.Clear();
    
            if (Request.Cookies["UserCookie"] != null)
            {
                HttpCookie userCookie = new HttpCookie("UserCookie");
                userCookie.Expires = DateTime.Now.AddDays(-1); 
                Response.Cookies.Add(userCookie);
            }

            Response.Redirect("Login.aspx");
        }

        protected void btnEditProfile_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            Response.Redirect($"UserProfileEdit.aspx?UserId={userId}");
        }

        private void DeletePost(int postId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SocialMediaPlatformDb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Posts WHERE PostId = @PostId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@PostId", postId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}