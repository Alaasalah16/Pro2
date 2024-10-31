using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration; // Add this import for ConfigurationManager
using System.Web.UI;

namespace WebApplication1
{
    public partial class UserS_Posts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAllPosts();
            }
        }

        private void LoadAllPosts()
        {
            DataTable postsTable = GetAllPosts();
            AllPostsGridView.DataSource = postsTable;
            AllPostsGridView.DataBind();
        }

        private DataTable GetAllPosts()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SocialMediaPlatformDb"].ConnectionString;
            DataTable postsTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Users.Username, Posts.Content, Posts.ImagePath, Posts.CreatedDate FROM Posts " +
                               "JOIN Users ON Posts.UserId = Users.Id WHERE Posts.Approved = 1 ORDER BY Posts.CreatedDate DESC";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.Fill(postsTable);
                }
            }

            return postsTable;
        }
    }
}
