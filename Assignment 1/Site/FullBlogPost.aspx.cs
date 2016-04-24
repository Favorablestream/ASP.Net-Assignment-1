using Assignment_1.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment_1
{
    public partial class FullBlogPost : System.Web.UI.Page
    {
        const string CONNECTION_STRING = "Database = Assignment 1; Data Source = us-cdbr-azure-east-a.cloudapp.net; User Id = be1420028142d2; Password=9582933c";

        //Bind comments and replace bad words with "****", comments are joined with users to have an Author column in the gridview
        private void getComments (MySqlConnection database, UInt64 postID)
        {
            if (database == null)
                throw new ArgumentNullException ();

            List<string> badWords = new List<string> ();

            using (MySqlCommand selectBadWords = new MySqlCommand ("Select * from badwords", database))
            {
                using (MySqlDataReader reader = selectBadWords.ExecuteReader ())
                {
                    while (reader.Read ())
                        badWords.Add (reader.GetString ("word"));
                }
            }

            using (DataTable table = new DataTable ())
            {
                using (MySqlCommand selectComments = new MySqlCommand ("Select * from comments left join users on comments.author_id = users.id and comments.post_id = @postID", database))
                {
                    selectComments.Parameters.AddWithValue ("@postID", postID);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter (selectComments))
                    {
                        adapter.Fill (table);

                        if (Session ["user"] != null && ((User) Session ["user"]).IsAdmin)
                            comments.Columns [3].Visible = true;

                        comments.DataSource = table;
                        comments.DataBind ();

                        foreach (GridViewRow row in comments.Rows)
                            foreach (string word in badWords)
                                row.Cells [0].Text = row.Cells [0].Text.Replace (word, "****");

                    }
                }
            }
        }

        protected void Page_Load (object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            //Redirect if no id is supplied in the url
            if (Request.QueryString ["postID"] == null)
                Response.Redirect ("/Site/Default.aspx");

            UInt64 postID = Convert.ToUInt64 (Request.QueryString ["postID"]);
            UInt64 authorID = 0UL;

            //Load post information
            using (MySqlConnection database = new MySqlConnection (CONNECTION_STRING))
            {
                database.Open ();

                using (MySqlCommand selectPost = new MySqlCommand ("Select * from posts where id = @postID", database))
                {
                    selectPost.Parameters.AddWithValue ("@postID", postID);

                    using (MySqlDataReader postReader = selectPost.ExecuteReader ())
                    {
                        postReader.Read ();

                        //Redirect if no result
                        if (!postReader.HasRows)
                            Response.Redirect ("/Site/Default.aspx");

                        //Redirect if the post is not available (allow if the user is an admin)
                        if (Session ["user"] != null)
                        {
                            if ((!postReader.GetBoolean ("is_available") && !((User) Session ["user"]).IsAdmin))
                                Response.Redirect ("/Site/Default.aspx");
                        }
                        else
                        {
                            if ((!postReader.GetBoolean ("is_available")))
                                Response.Redirect ("/Site/Default.aspx");
                        }

                        authorID = postReader.GetUInt64 ("author_id");
                        title.Text = postReader.GetString ("title");
                        description.Text = postReader.GetString ("description");
                        text.Text = postReader.GetString ("text");
                        posted.Text = postReader.GetDateTime ("posted").ToString ();
                    }
                }

                //Select the author's name
                using (MySqlCommand selectAuthor = new MySqlCommand ("Select * from users where id = @authorID", database))
                {
                    selectAuthor.Parameters.AddWithValue ("@authorID", authorID);

                    using (MySqlDataReader authorReader = selectAuthor.ExecuteReader ())
                    {
                        authorReader.Read ();

                        if (!authorReader.HasRows)
                            return;

                        author.Text = authorReader.GetString ("firstname") + " " + authorReader.GetString ("lastname");
                    }
                }

                getComments (database, postID);
            }
        }

        //Submit a comment
        protected void submitComment_Click (object sender, EventArgs e)
        {
            UInt64 postID = Convert.ToUInt64 (Request.QueryString ["postID"]);
            UInt64 authorID = 0UL;

            if (Session ["user"] == null)
            {
                error.Text = "You must be logged in to post a comment<br /><a href = \"/Site/Login.aspx\">Login Page</a><br />";
                error.Visible = true;
                return;
            }

            authorID = ((User) Session ["user"]).ID;

            using (MySqlConnection database = new MySqlConnection (CONNECTION_STRING))
            {
                database.Open ();

                using (MySqlCommand insertComment = new MySqlCommand ("Insert into comments (post_id, author_id, text) values (@postID, @authorID, @text)", database))
                {
                    insertComment.Parameters.AddWithValue ("@postID", postID);
                    insertComment.Parameters.AddWithValue ("@authorID", authorID);
                    insertComment.Parameters.AddWithValue ("@text", writeComment.Text.Trim ());

                    insertComment.ExecuteNonQuery ();
                }

                //Rebind comments
                getComments (database, postID);

                writeComment.Text = "";
            }
        }

        //Delete comment
        protected void comments_RowDeleting (object sender, GridViewDeleteEventArgs e)
        {
            UInt64 postID = Convert.ToUInt64 (Request.QueryString ["postID"]);

            using (MySqlConnection database = new MySqlConnection (CONNECTION_STRING))
            {
                database.Open ();

                using (MySqlCommand delete = new MySqlCommand ("Delete from comments where id = @commentID", database))
                {
                    delete.Parameters.AddWithValue ("@commentID", Int32.Parse (comments.Rows [e.RowIndex].Cells [4].Text));
                    delete.ExecuteNonQuery ();
                }

                using (DataTable table = new DataTable ())
                {
                    using (MySqlCommand selectComments = new MySqlCommand ("Select * from comments where post_id = @postID", database))
                    {
                        selectComments.Parameters.AddWithValue ("@postID", postID);

                        using (MySqlDataAdapter select = new MySqlDataAdapter (selectComments))
                        {
                            select.Fill (table);

                            comments.DataSource = table;
                            comments.DataBind ();
                        }
                    }
                }
            }
        }
    }
}