using Assignment_1.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace Assignment_1
{
    public partial class Register : System.Web.UI.Page
    {
        const string CONNECTION_STRING = "Database = Assignment 1; Data Source = us-cdbr-azure-east-a.cloudapp.net; User Id = be1420028142d2; Password=9582933c";

        protected void Page_Load (object sender, EventArgs e)
        {
            //Redirect if logged in
            if (!Page.IsPostBack)
                if (Session ["user"] != null)
                    Response.Redirect ("/Site/Default.aspx");
        }

        //Add the user to the database
        protected void submit_Click (object sender, EventArgs e)
        {
            using (SHA256CryptoServiceProvider encrypt = new SHA256CryptoServiceProvider ())
            {
                byte [] usernameBytes = Encoding.Unicode.GetBytes (username.Text.Trim ());
                byte [] passwordBytes = Encoding.Unicode.GetBytes (password.Text.Trim ());
                byte [] emailBytes = Encoding.Unicode.GetBytes (email.Text.Trim ());

                byte [] usernameHash = encrypt.ComputeHash (usernameBytes);
                byte [] passwordHash = encrypt.ComputeHash (passwordBytes);
                byte [] emailHash = encrypt.ComputeHash (emailBytes);

                using (MySqlConnection database = new MySqlConnection (CONNECTION_STRING))
                {
                    database.Open ();

                    using (MySqlCommand insert = new MySqlCommand ("Insert into users (is_admin, username, password, email, firstname, lastname, dob, country, phone) values (@admin, @username, @password, @email, @firstname, @lastname, @dob, @country, @phone)", database))
                    {
                        insert.Parameters.AddWithValue ("@admin", admin.Checked);
                        insert.Parameters.AddWithValue ("@username", usernameHash);
                        insert.Parameters.AddWithValue ("@password", passwordHash);
                        insert.Parameters.AddWithValue ("@email", emailHash);
                        insert.Parameters.AddWithValue ("@firstname", firstName.Text.Trim ());
                        insert.Parameters.AddWithValue ("@lastname", lastname.Text.Trim ());
                        insert.Parameters.AddWithValue ("@dob", dob.SelectedDate);
                        insert.Parameters.AddWithValue ("@country", country.Text.Trim ());
                        insert.Parameters.AddWithValue ("@phone", phone.Text.Trim ());

                        insert.ExecuteNonQuery ();

                        //Get the id of the user that was just added
                        using (MySqlCommand lastID = new MySqlCommand ("Select max(id) as id from users", database))
                        {
                            using (MySqlDataReader reader = lastID.ExecuteReader ())
                            {
                                reader.Read ();

                                Session ["user"] = new User (reader.GetUInt64 ("id"), admin.Checked, firstName.Text.Trim (), lastname.Text.Trim (), dob.SelectedDate, country.Text.Trim (), phone.Text.Trim ());

                                Response.Redirect ("Default.aspx");
                            }
                        }
                    }
                }
            }
        }
    }
}
