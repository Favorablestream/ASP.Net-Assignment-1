using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;
using Assignment_1.Classes;

namespace Assignment_1
{
    public partial class Login : System.Web.UI.Page
    {
        const string CONNECTION_STRING = "Database = Assignment 1; Data Source = us-cdbr-azure-east-a.cloudapp.net; User Id = be1420028142d2; Password=9582933c";

        protected void Page_Load (object sender, EventArgs e)
        {
            //Redirect if the user is alreadt logged in
            if (!Page.IsPostBack)
                if (Session ["user"] != null)
                    Response.Redirect ("/Site/Default.aspx");
        }

        //Validate username and password
        protected void login_Authenticate (object sender, AuthenticateEventArgs e)
        {
            using (SHA256CryptoServiceProvider encrypt = new SHA256CryptoServiceProvider ())
            {
                byte [] username = Encoding.Unicode.GetBytes (login.UserName.Trim ());
                byte [] password = Encoding.Unicode.GetBytes (login.Password.Trim ());

                byte [] usernameHash = encrypt.ComputeHash (username);
                byte [] passwordHash = encrypt.ComputeHash (password);

                using (MySqlConnection database = new MySqlConnection (CONNECTION_STRING))
                {
                    database.Open ();

                    using (MySqlCommand select = new MySqlCommand ("Select * from users where username = @usernameHash and password = @passwordHash", database))
                    {
                        select.Parameters.AddWithValue ("@usernameHash", usernameHash);
                        select.Parameters.AddWithValue ("@passwordHash", passwordHash);

                        using (MySqlDataReader reader = select.ExecuteReader ())
                        {
                            reader.Read ();

                            //If we have a result we are authenticated
                            if (reader.HasRows)
                            {
                                e.Authenticated = true;

                                Session ["user"] = new User (reader.GetUInt64 ("id"), reader.GetBoolean ("is_admin"), reader.GetString ("firstname"), reader.GetString ("lastname"), reader.GetDateTime ("dob"), reader.GetString ("country"), reader.GetString ("phone"));
                                Response.Redirect ("Default.aspx");
                            }
                            else
                                e.Authenticated = false;
                        }
                    }
                }
            }
        }
    }
}