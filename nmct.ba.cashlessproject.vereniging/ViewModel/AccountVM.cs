using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
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

        public TokenResponse Token { get; set; }

        private string _constring;

        public string ConString
        {
            get { return _constring; }
            set { _constring = value; OnPropertyChanged("ConString"); }
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

        public ICommand LoginCommand {
            get { return new RelayCommand(Login); }
        }

        private void Login()
        {
            Token = Webaccess.GetToken(UserName, Password);
            if (Token.IsError)
                TokenOk = false;
            else
                TokenOk = true;
        }

        public ICommand LogoutCommand
        {
            get { return new RelayCommand(Logout); }
        }

        private async void Logout()
        {
            ConString = await Webaccess.GetConnectionString(Token.AccessToken);
        }
    }
}
