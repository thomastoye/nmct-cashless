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
                HttpResponseMessage response = await
                client.GetAsync(ConfigurationSettings.AppSettings.Get("apiUrl") + "/api/register");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Registers = JsonConvert.DeserializeObject<ObservableCollection<Register>>(json);
                }
            }
        }
        private ObservableCollection<Register> _registers;
        public ObservableCollection<Register> Registers
        {
            get { return _registers; }
            set { _registers = value; OnPropertyChanged("Registers"); }
        }
        private Register _selectedRegister;
        public Register SelectedRegister
        {
            get { return _selectedRegister; }
            set { _selectedRegister = value; OnPropertyChanged("SelectedRegister"); }
        }
        public ICommand AddRegisterCommand
        {
            get { return new RelayCommand(AddRegister); }
        }
        public ICommand DeleteRegisterCommand
        {
            get { return new RelayCommand(DeleteRegister); }
        }
        public ICommand SaveRegisterCommand
        {
            get { return new RelayCommand(SaveRegister); }
        }
        public async void AddRegister()
        {
            Register newRegister = new Register();
            Registers.Add(newRegister);
            using (HttpClient client = new HttpClient())
            {
                string Register = JsonConvert.SerializeObject(newRegister);
                HttpResponseMessage response = await
                client.PostAsync(ConfigurationSettings.AppSettings.Get("apiUrl") + "api/register", new StringContent(Register, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    SelectedRegister = newRegister;
                }
            }
        }
        public async void DeleteRegister()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await
                client.DeleteAsync(ConfigurationSettings.AppSettings.Get("apiUrl") + "api/register/1");
                if (response.IsSuccessStatusCode)
                {
                    Registers.Remove(SelectedRegister);
                }
            }
        }
        public async void SaveRegister()
        {
            using (HttpClient client = new HttpClient())
            {
                string Register = JsonConvert.SerializeObject(SelectedRegister);
                HttpResponseMessage response = await
                client.PutAsync(ConfigurationSettings.AppSettings.Get("apiUrl") + "api/register", new StringContent(Register, Encoding.UTF8, "application/json"));
            }
        }
    }
}
