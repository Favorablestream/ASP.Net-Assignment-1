using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment_1.Classes
{
    public class User
    {
        private UInt64 id;
        public UInt64 ID
        {
            get
            {
                return id;
            }
            set
            {
                if (value < 0)
                    return;

                id = value;
            }
        }

        private bool isAdmin;
        public bool IsAdmin
        {
            get
            {
                return isAdmin;
            }
            set
            {
                isAdmin = value;
            }
        }

        private string firstName;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
            }
        }

        private string lastName;
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }

        private DateTime dob;
        public DateTime DOB
        {
            get
            {
                return dob;
            }
            set
            {
                dob = value;
            }
        }

        private string country;
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }

        private string phone;
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
            }
        }

        public User (UInt64 id, bool isAdmin, string firstName, string lastName, DateTime dob, string country, string phone)
        {
            this.id = id;
            this.isAdmin = isAdmin;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dob = dob;
            this.country = country;
            this.phone = phone;
        }

        public override bool Equals (object obj)
        {
            User equal = (User) obj;

            return (isAdmin == equal.isAdmin && firstName == equal.firstName && lastName == equal.lastName && dob == equal.dob && country == equal.country);
        }

        public override int GetHashCode ()
        {
            return isAdmin.GetHashCode () * firstName.GetHashCode () * lastName.GetHashCode () * dob.GetHashCode () * country.GetHashCode ();
        }

        public override string ToString ()
        {
            return "First Name: " + firstName + ", Last Name: " + lastName + ", DOB: " + dob.ToString () + ", Country: " + country;
        }

        public static bool operator == (User user1, User user2)
        {
            return user1.Equals (user2);
        }

        public static bool operator != (User user1, User user2)
        {
            return !user1.Equals (user2);
        }
    }
}