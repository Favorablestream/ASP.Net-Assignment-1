using Assignment_1.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment_1.Admin
{
    public partial class BadWords : System.Web.UI.Page
    {
        const string CONNECTION_STRING = "Database = Assignment 1; Data Source = us-cdbr-azure-east-a.cloudapp.net; User Id = be1420028142d2; Password=9582933c";

        protected void Page_Load (object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            //Redirect if not logged in or an admin
            if (Session ["user"] == null || !((User) Session ["user"]).IsAdmin)
                Response.Redirect ("/Site/Login.aspx");

            //Fill bad words gridview
            using (MySqlConnection database = new MySqlConnection (CONNECTION_STRING))
            {
                database.Open ();

                using (DataTable table = new DataTable ())
                {
                    using (MySqlDataAdapter select = new MySqlDataAdapter (new MySqlCommand ("Select * from badwords", database)))
                    {
                        select.Fill (table);

                        badwords.DataSource = table;
                        badwords.DataBind ();
                    }
                }
            }
        }

        //Submit new bad word
        protected void submitBadWord_Click (object sender, EventArgs e)
        {
            using (MySqlConnection database = new MySqlConnection (CONNECTION_STRING))
            {
                database.Open ();

                using (MySqlCommand insertWord = new MySqlCommand ("Insert into badwords (word) values (@word)", database))
                {
                    insertWord.Parameters.AddWithValue ("@word", writeBadWord.Text.Trim ());
                    insertWord.ExecuteNonQuery ();
                }

                writeBadWord.Text = "";

                //Rebind bad words
                using (DataTable table = new DataTable ())
                {
                    using (MySqlDataAdapter select = new MySqlDataAdapter (new MySqlCommand ("Select * from badwords", database)))
                    {
                        select.Fill (table);

                        badwords.DataSource = table;
                        badwords.DataBind ();
                    }
                }
            }
        }

        //Delete bad word
        protected void badwords_RowDeleting (object sender, GridViewDeleteEventArgs e)
        {
            using (MySqlConnection database = new MySqlConnection (CONNECTION_STRING))
            {
                database.Open ();

                using (MySqlCommand deleteWord = new MySqlCommand ("Delete from badwords where id = @wordID", database))
                {
                    deleteWord.Parameters.AddWithValue ("@wordID", Int32.Parse (badwords.Rows [e.RowIndex].Cells [3].Text));

                    deleteWord.ExecuteNonQuery ();
                }

                //Rebind bad words
                using (DataTable table = new DataTable ())
                {
                    using (MySqlDataAdapter select = new MySqlDataAdapter (new MySqlCommand ("Select * from badwords", database)))
                    {
                        select.Fill (table);

                        badwords.DataSource = table;
                        badwords.DataBind ();
                    }
                }
            }
        }
    }
}