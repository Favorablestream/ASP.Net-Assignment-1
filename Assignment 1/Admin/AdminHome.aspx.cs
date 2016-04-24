using Assignment_1.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment_1.Admin
{
    public partial class AdminHome : System.Web.UI.Page
    {
        const string CONNECTION_STRING = "Database = Assignment 1; Data Source = us-cdbr-azure-east-a.cloudapp.net; User Id = be1420028142d2; Password=9582933c";

        protected void Page_Load (object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            //Redirect if not logged in or not an admin
            if (Session ["user"] == null || !((User) Session ["user"]).IsAdmin)
                Response.Redirect ("/Site/Login.aspx");

            else
            {
                User user = (User) Session ["user"];
                userLabel.Text = "Welcome " + user.FirstName + ". Would you like to <a href = \"/Site/EditUser.aspx\">edit your information?</a><br /><a href = \"/Site/Default.aspx\">Regular Home</a>";
            }

            //Load all posts
            using (MySqlConnection database = new MySqlConnection (CONNECTION_STRING))
            {
                database.Open ();

                using (DataTable table = new DataTable ())
                {
                    using (MySqlDataAdapter select = new MySqlDataAdapter (new MySqlCommand ("Select * from posts order by posted desc", database)))
                    {
                        select.Fill (table);

                        posts.DataSource = table;
                        posts.DataBind ();
                    }
                }
            }
        }

        //Edit post
        protected void posts_RowEditing (object sender, GridViewEditEventArgs e)
        {
            string editURL = "/Admin/EditBlogPost.aspx?postID=" + Server.UrlEncode (posts.Rows [e.NewEditIndex].Cells [5].Text);
            Response.Redirect (editURL);
        }

        //Delete post
        protected void posts_RowDeleting (object sender, GridViewDeleteEventArgs e)
        {
            //Hide page
            userLabel.Visible = false;
            posts.Visible = false;

            //Display confirmation
            confirmLabel.Visible = true;
            deleteButton.Visible = true;
            cancelButton.Visible = true;

            Session ["deleteIndex"] = e.RowIndex;
        }

        //If the user confirms the delete, thenr emove from the database
        protected void deleteButton_Click (object sender, EventArgs e)
        {
            using (MySqlConnection database = new MySqlConnection (CONNECTION_STRING))
            {
                database.Open ();

                using (MySqlCommand delete = new MySqlCommand ("Delete from posts where id = @postID", database))
                {
                    delete.Parameters.AddWithValue ("@postID", Int32.Parse (posts.Rows [Convert.ToInt32 (Session ["deleteIndex"])].Cells [5].Text));
                    delete.ExecuteNonQuery ();
                }

                using (DataTable table = new DataTable ())
                {
                    using (MySqlDataAdapter select = new MySqlDataAdapter (new MySqlCommand ("Select * from posts order by posted desc", database)))
                    {
                        select.Fill (table);

                        posts.DataSource = table;
                        posts.DataBind ();
                    }
                }
            }

            //Restore the page
            userLabel.Visible = true;
            posts.Visible = true;

            confirmLabel.Visible = false;
            deleteButton.Visible = false;
            cancelButton.Visible = false;
        }

        //If they cancel then restore the page
        protected void cancelButton_Click (object sender, EventArgs e)
        {
            userLabel.Visible = true;
            posts.Visible = true;

            confirmLabel.Visible = false;
            deleteButton.Visible = false;
            cancelButton.Visible = false;
        }
    }
    
}