using Assignment_1.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace Assignment_1
{
    public partial class EditUser : System.Web.UI.Page
    {
        const string CONNECTION_STRING = "Database = Assignment 1; Data Source = us-cdbr-azure-east-a.cloudapp.net; User Id = be1420028142d2; Password=9582933c";

        protected void Page_Load (object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session ["user"] == null)
                    Response.Redirect ("/Site/Login.aspx");

                User user = (User) Session ["user"];
                firstName.Text = user.FirstName;
                lastname.Text = user.LastName;
                phone.Text = user.Phone;
                dob.SelectedDate = user.DOB;
                country.Text = user.Country;
                admin.Checked = user.IsAdmin;
            }
        }

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

                    UInt64 userID = ((User) Session ["user"]).ID;

                    using (MySqlCommand insert = new MySqlCommand ("Update users set is_admin = @admin, username = @username, password = @password, email = @email, firstname = @firstname, lastname = @lastname, dob = @dob, country = @country, phone = @phone where id = @userID", database))
                    {
                        insert.Parameters.AddWithValue ("@admin", admin.Checked);
                        insert.Parameters.AddWithValue ("@username", usernameHash);
                        insert.Parameters.AddWithValue ("@password", passwordHash);
                        insert.Parameters.AddWithValue ("@email", emailHash);
                        insert.Parameters.AddWithValue ("@firstname", firstName.Text.Trim ());
                        insert.Parameters.AddWithValue ("@lastname", lastname.Text.Trim ());
                        insert.Parameters.AddWithValue ("@dob", dob.SelectedDate);
                        insert.Parameters.AddWithValue ("@country", country.Text.Trim ());
                        insert.Parameters.AddWithValue ("@userID", userID);
                        insert.Parameters.AddWithValue ("@phone", phone.Text.Trim ());

                        insert.ExecuteNonQuery ();

                        Session ["user"] = null;
                        Session ["user"] = new User (userID, admin.Checked, firstName.Text.Trim (), lastname.Text.Trim (), dob.SelectedDate, country.Text.Trim (), phone.Text.Trim ());

                        Response.Redirect ("Default.aspx");
                            
                        
                    }
                }
            }
        }
    }
}