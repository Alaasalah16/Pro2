using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class EditPost : System.Web.UI.Page
    {
       
       
            protected int PostId
            {
                get
                {
                    return Convert.ToInt32(Request.QueryString["PostId"]);
                }
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    LoadPostData();
                }
            }

        private void LoadPostData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SocialMediaPlatformDb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Content, ImagePath FROM Posts WHERE PostId = @PostId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@PostId", PostId);

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtContent.Text = reader["Content"].ToString();

                        string imagePath = reader["ImagePath"].ToString();
                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            CurrentImage.ImageUrl = imagePath;
                            CurrentImage.Visible = true;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Post not found.";
                        lblMessage.CssClass = "text-danger";
                    }
                }
            }
        }
    


        protected void btnSave_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text;
            string imagePath = null;

            if (ImageUpload.HasFile)
            {
                imagePath = "~/PostImages/" + Path.GetFileName(ImageUpload.FileName);
                ImageUpload.SaveAs(Server.MapPath(imagePath));
            }

            string connectionString = ConfigurationManager.ConnectionStrings["SocialMediaPlatformDb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Posts SET Content = @Content, ImagePath = @ImagePath WHERE PostId = @PostId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Content", content);
                cmd.Parameters.AddWithValue("@PostId", PostId);

                if (!string.IsNullOrEmpty(imagePath))
                {
                    cmd.Parameters.AddWithValue("@ImagePath", imagePath);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ImagePath", DBNull.Value);
                }

                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    lblMessage.Text = "Post updated successfully!";
                    lblMessage.CssClass = "text-success";
                }
                else
                {
                    lblMessage.Text = "Failed to update post.";
                    lblMessage.CssClass = "text-danger";
                }
            }
        }


        protected void btnCancel_Click(object sender, EventArgs e)
            {
                Response.Redirect("UserDashboard.aspx");
            }
        }
    }

        