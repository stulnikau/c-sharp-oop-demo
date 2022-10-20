using System;

namespace AuctionHouse
{
    /// <summary>
    /// Holds the name, account access details, and address of a client to an auction house.
    /// </summary>
    public class Client
    {
        private string name;
        private string email;
        private string address;
        private string password;

        /// <summary>
        /// Displays the name and email of a client.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name} {Email}";
        }

        /// <summary>
        /// Sets or gets a validated client name. Returns an ArgumentException if set to a
        /// zero-length string.
        /// </summary>
        public string Name {
            get {
                return name;
            }
            set {
                if (value.Length == 0)
                {
                    throw new ArgumentException("You have not entered a full name");
                }
                name = value;
            }
        }

        /// <summary>
        /// Sets or gets a validated client email. Returns an ArgumentException if set to a
        /// zero-length string or a string without an '@' character.
        /// </summary>
        public string Email {
            get {
                return email;
            }
            set {
                if (value.Length == 0 || !value.Contains("@"))
                {
                    throw new ArgumentException("You have entered an invalid email");
                }
                email = value;
            }
        }

        /// <summary>
        /// Sets or gets a free-form validated client address. Returns an ArgumentException
        /// if set to a zero-length string.
        /// </summary>
        public string Address {
            get {
                return address;
            }
            set {
                if (value.Length == 0)
                {
                    throw new ArgumentException("You have not entered an address");
                }
                address = value;
            }
        }

        /// <summary>
        /// Sets or gets a validated client password. Returns an ArgumentException if set to a
        /// zero-length string.
        /// </summary>
        public string Password
        {
            get {
                return password;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("You have not entered a password");
                }
                password = value;
            }
        }
    }
}
