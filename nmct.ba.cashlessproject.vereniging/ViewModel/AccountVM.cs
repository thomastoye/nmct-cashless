using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Thinktecture.IdentityModel.Client;

namespace nmct.ba.cashlessproject.vereniging.ViewModel
{
    class AccountVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Account"; }
        }

        private TokenResponse _token;

        public TokenResponse Token
        {
            get { return _token;  }
            set {
                _token = value;
                if (value != null)
                {
                    ConfigurationManager.AppSettings["token"] = value.AccessToken;
                }
                else
                {
                    ConfigurationManager.AppSettings["token"] = "";
                }
            }
        }

        private Boolean _tokenok = false;

        public Boolean TokenOk
        {
            get { return _tokenok; }
            set { _tokenok = value; OnPropertyChanged("TokenOk"); }
        }

        private string _username;

        public string UserName
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged("UserName"); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged("Password"); }
        }

        private string _oldpass;

        public string OldPassword
        {
            get { return _oldpass; }
            set { _oldpass = value; OnPropertyChanged("OldPassword"); }
        }
        private string _newpass;

        public string NewPassword
        {
            get { return _newpass; }
            set { _newpass = value; OnPropertyChanged("NewPassword"); }
        }

        private string _passChangeMsg = "";

        public string PasswordChangeMessage
        {
            get { return _passChangeMsg; }
            set { _passChangeMsg = value; OnPropertyChanged("PasswordChangeMessage"); }
        }

        private string _loginMsg;

        public string LoginMessage
        {
            get { return _loginMsg; }
            set { _loginMsg = value; OnPropertyChanged("LoginMessage"); }
        }
        

        public ICommand LoginCommand {
            get { return new RelayCommand(Login); }
        }

        private void Login()
        {
            Token = Webaccess.GetToken(UserName, Password);
            if (Token.IsError)
            {
                TokenOk = false;
                LoginMessage = "Verkeerde combinatie!";
            }
            else
            {
                TokenOk = true;
                LoginMessage = "Ingelogd!";
            }

            Password = "";
        }

        public ICommand LogoutCommand
        {
            get { return new RelayCommand(Logout); }
        }

        private async void Logout()
        {
            Token = null;
            TokenOk = false;
            LoginMessage = "";
            PasswordChangeMessage = "";
        }

        public ICommand ChangePasswordCommand
        {
            get { return new RelayCommand(ChangePassword); }
        }

        private async void ChangePassword()
        {
            var succeeded = await Webaccess.ChangePassword(Token.AccessToken, OldPassword, NewPassword);

            if (succeeded)
            {
                PasswordChangeMessage = "Paswoord is gewijzigd!";
            } else {
                PasswordChangeMessage = "Verkeerd wachtwoord!";
            }

            OldPassword = "";
            NewPassword = "";
        }
    }
}
