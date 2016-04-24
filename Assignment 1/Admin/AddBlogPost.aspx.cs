using Assignment_1.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Web.UI;

namespace Assignment_1.Admin
{
    public partial class AddBlogPost : System.Web.UI.Page
    {
        const string CONNECTION_STRING = "Database = Assignment 1; Data Source = us-cdbr-azure-east-a.cloudapp.net; User Id = be1420028142d2; Password=9582933c";

        protected void Page_Load (object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            //Redirect if not logged in or not an admin
            if (Session ["user"] == null || !((User) Session ["user"]).IsAdmin)
                Response.Redirect ("/Site/Login.aspx");
        }

        //Add post to database
        protected void submit_Click (object sender, EventArgs e)
        {
            using (MySqlConnection database = new MySqlConnection (CONNECTION_STRING))
            {
                database.Open ();

                using (MySqlCommand insertPost = new MySqlCommand ("Insert into posts (author_id, is_available, title, description, text) values (@authorID, @isAvailable, @title, @description, @text)", database))
                {
                    User user = (User) Session ["user"];
                    insertPost.Parameters.AddWithValue ("@authorID", user.ID);
                    insertPost.Parameters.AddWithValue ("@isAvailable", available.Checked);
                    insertPost.Parameters.AddWithValue ("@title", title.Text.Trim ());
                    insertPost.Parameters.AddWithValue ("@description", description.Text.Trim ());
                    insertPost.Parameters.AddWithValue ("@text", text.Text.Trim ());

                    insertPost.ExecuteNonQuery ();
                }
            }

            Response.Redirect ("/Admin/AdminHome.aspx");
        }
    }
}