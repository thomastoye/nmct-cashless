using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nmct.ba.cashlessproject.model
{
    public class Organisation
    {
        private long _id;

        public long ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _login;

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _dbName;

        public string DBName
        {
            get { return _dbName; }
            set { _dbName = value; }
        }

        private string _dbLogin;

        public string DBLogin
        {
            get { return _dbLogin; }
            set { _dbLogin = value; }
        }

        private string _dbPassword;

        public string DBPassword
        {
            get { return _dbPassword; }
            set { _dbPassword = value; }
        }

        private string _organisationName;

        public string OrganisationName
        {
            get { return _organisationName; }
            set { _organisationName = value; }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        
        
    }
}
