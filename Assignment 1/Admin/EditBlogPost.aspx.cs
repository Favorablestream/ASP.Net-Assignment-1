using Assignment_1.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Web.UI;

namespace Assignment_1.Admin
{
    public partial class EditBlogPost : System.Web.UI.Page
    {
        const string CONNECTION_STRING = "Database = Assignment 1; Data Source = us-cdbr-azure-east-a.cloudapp.net; User Id = be1420028142d2; Password=9582933c";

        protected void Page_Load (object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            //Redirect if not logged in or an admin
            if (Session ["user"] == null || !((User) Session ["user"]).IsAdmin)
                Response.Redirect ("/Site/Login.aspx");

            Int32 postID = Int32.Parse (Request.QueryString ["postID"]);

            //Load the post's current information
            using (MySqlConnection database = new MySqlConnection (CONNECTION_STRING))
            {
                database.Open ();

                using (MySqlCommand selectPosts = new MySqlCommand ("Select * from posts where id = @postID", database))
                {
                    selectPosts.Parameters.AddWithValue ("@postID", postID);

                    using (MySqlDataReader reader = selectPosts.ExecuteReader ())
                    {
                        reader.Read ();
                        title.Text = reader.GetString ("title");
                        description.Text = reader.GetString ("description");
                        text.Text = reader.GetString ("text");
                        available.Checked = reader.GetBoolean ("is_available");
                    }
                }
            }
        }

        //Update the posts values (overwrite)
        protected void submit_Click (object sender, EventArgs e)
        {
            Int32 postID = Int32.Parse (Request.QueryString ["postID"]);

            using (MySqlConnection database = new MySqlConnection (CONNECTION_STRING))
            {
                database.Open ();

                using (MySqlCommand insertPost = new MySqlCommand ("Update posts set author_id = @authorID, is_available = @isAvailable, title = @title, description = @description, text = @text where id = @postID", database))
                {
                    User user = (User) Session ["user"];
                    insertPost.Parameters.AddWithValue ("@authorID", user.ID);
                    insertPost.Parameters.AddWithValue ("@isAvailable", available.Checked);
                    insertPost.Parameters.AddWithValue ("@title", title.Text.Trim ());
                    insertPost.Parameters.AddWithValue ("@description", description.Text.Trim ());
                    insertPost.Parameters.AddWithValue ("@text", text.Text.Trim ());
                    insertPost.Parameters.AddWithValue ("@postID", postID);
                    insertPost.ExecuteNonQuery ();
                }
            }
            Response.Redirect ("/Admin/AdminHome.aspx");
        }
    }
}