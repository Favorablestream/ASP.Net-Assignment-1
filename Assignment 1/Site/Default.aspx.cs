using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using Assignment_1.Classes;

namespace Assignment_1
{
    public partial class Default : System.Web.UI.Page
    {
        const string CONNECTION_STRING = "Database = Assignment 1; Data Source = us-cdbr-azure-east-a.cloudapp.net; User Id = be1420028142d2; Password=9582933c";

        protected void Page_Load (object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            if (Session ["user"] == null)
                userLabel.Text = "Would you like to <a href = \"/Site/Login.aspx\">login?</a>";

            else
            {
                User user = (User) Session ["user"];
                userLabel.Text = "Welcome " + user.FirstName + ". Would you like to <a href = \"/Site/EditUser.aspx\">edit your information?</a>";

                if (user.IsAdmin)
                    userLabel.Text += "<br /><a href = \"/Admin/AdminHome.aspx\">Administrator Home</a>";

                logout.Visible = true;
            }

            using (MySqlConnection database = new MySqlConnection (CONNECTION_STRING))
            {
                database.Open ();

                using (DataTable table = new DataTable ())
                {
                    using (MySqlDataAdapter select = new MySqlDataAdapter (new MySqlCommand ("Select id, title, description, posted from posts where is_available = 1 order by posted desc", database)))
                    {
                        select.Fill (table);

                        top5Posts.DataSource = table;
                        top5Posts.DataBind ();
                    }
                }
            }
        }

        protected void top5Posts_PageIndexChanging (object sender, GridViewPageEventArgs e)
        {
            using (MySqlConnection database = new MySqlConnection (CONNECTION_STRING))
            {
                database.Open ();

                using (DataTable table = new DataTable ())
                {
                    using (MySqlDataAdapter select = new MySqlDataAdapter (new MySqlCommand ("Select id, title, description, posted from posts where is_available = 1 order by posted desc", database)))
                    {
                        select.Fill (table);

                        top5Posts.PageIndex = e.NewPageIndex;
                        top5Posts.DataSource = table;
                        top5Posts.DataBind ();
                    }
                }
            }
        }

        protected void logout_Click (object sender, EventArgs e)
        {
            Session ["user"] = null;
            userLabel.Text = "Would you like to <a href = \"/Site/Login.aspx\">login?</a>";
            logout.Visible = false;
        }
    }
}