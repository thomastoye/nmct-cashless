using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Thinktecture.IdentityModel.Client;
using GalaSoft.MvvmLight.CommandWpf;

namespace nmct.ba.cashlessproject.kassa.ViewModel
{
    class LoginVM : ObservableObject, IPage
    {
        public bool IsLoggedIn { get; set; }

        public string Name
        {
            get { return "Log in"; }
        }

        private string _loginStatus;

        public string LoginStatus
        {
            get { return _loginStatus; }
            set { _loginStatus = value; OnPropertyChanged("LoginStatus"); }
        }


        public ICommand LoginCommand
        {
            get { return new RelayCommand(Login); }
        }

        public async void Login()
        {

            string username = ConfigurationManager.AppSettings["username"];
            string password = ConfigurationManager.AppSettings["password"];

            var client = new OAuth2Client(new Uri(ConfigurationManager.AppSettings["apiUrl"] + "token"));
            TokenResponse response = client.RequestResourceOwnerPasswordAsync(username, password).Result;

            if (response.Error == null)
            {
                IsLoggedIn = true;
                ConfigurationManager.AppSettings["token"] = response.AccessToken;
                LoginStatus = "Kassa ingelogd.";
            }
            else
            {
                LoginStatus = "Kon niet inloggen met OAuth. Neem contact op met de leverencier.";
            }

        }
    }
}
