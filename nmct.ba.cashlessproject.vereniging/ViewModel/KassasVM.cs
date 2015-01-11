using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.vereniging.ViewModel
{
    class KassasVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Kassas"; }
        }

        public KassasVM()
        {
            GetRegisters();
        }
        private async void GetRegisters()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ConfigurationManager.AppSettings["token"]);
                HttpResponseMessage response = await
                client.GetAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/register");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Registers = JsonConvert.DeserializeObject<ObservableCollection<RegisterOrganisation>>(json);
                }
            }
        }
        private ObservableCollection<RegisterOrganisation> _registers = new ObservableCollection<RegisterOrganisation>();
        public ObservableCollection<RegisterOrganisation> Registers
        {
            get { return _registers; }
            set { _registers = value; OnPropertyChanged("Registers"); }
        }
        private RegisterOrganisation _selectedRegister;
        public RegisterOrganisation SelectedRegister
        {
            get { return _selectedRegister; }
            set { _selectedRegister = value; OnPropertyChanged("SelectedRegister"); }
        }
        public ICommand RefreshCommand
        {
            get { return new RelayCommand(GetRegisters); }
        }
    }
}
