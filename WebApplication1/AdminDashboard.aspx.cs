using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO; // Added System.IO for file operations

namespace WebApplication1
{
    public partial class AdminDashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUsers();
                LoadPosts();
            }
        }

        private void LoadUsers()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SocialMediaPlatformDb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Username, Email FROM Users";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable usersTable = new DataTable();
                adapter.Fill(usersTable);

                UsersGridView.DataSource = usersTable;
                UsersGridView.DataBind();
            }
        }

        private void LoadPosts()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SocialMediaPlatformDb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT PostId, Content, UserId, Approved FROM Posts";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable postsTable = new DataTable();
                adapter.Fill(postsTable);

                PostsGridView.DataSource = postsTable;
                PostsGridView.DataBind();
            }
        }

        protected void UsersGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int userId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "DeactivateUser")
            {
                DeactivateUser(userId);
            }
            else if (e.CommandName == "DeleteUser")
            {
                DeleteUser(userId);
            }
            else if (e.CommandName == "PromoteToAdmin")
            {
                PromoteToAdmin(userId);
            }

            LoadUsers();
        }

        protected void PostsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int postId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "ApprovePost")
            {
                ApprovePost(postId);
            }
            else if (e.CommandName == "DeletePost")
            {
                DeletePost(postId);
            }

            LoadPosts();
        }

        private void PromoteToAdmin(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SocialMediaPlatformDb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET Role = 'Admin' WHERE Id = @UserId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                cmd.ExecuteNonQuery();
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

        private void DeleteUser(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SocialMediaPlatformDb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Users WHERE Id = @UserId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void ApprovePost(int postId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SocialMediaPlatformDb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Posts SET Approved = 1 WHERE PostId = @PostId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@PostId", postId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
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

        protected void btnDownloadLog_Click(object sender, EventArgs e)
        {
            string logPath = Server.MapPath("~/log.txt");

            if (File.Exists(logPath))
            {
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=log.txt");
                Response.TransmitFile(logPath);
                Response.End();
            }
            else
            {
                lblMessage.Text = "Log file not found.";
            }
        }

    }
}
