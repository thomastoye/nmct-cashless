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
                client.GetAsync(ConfigurationManager.AppSettings["apiUrl"] + "/api/register");
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
            using (HttpClient client = new HttpClient())
            {
                string register = JsonConvert.SerializeObject(newRegister);
                HttpResponseMessage response = await client.PostAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/register", new StringContent(register, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Register reg = JsonConvert.DeserializeObject<Register>(json);
                    if (reg != null)
                    {
                        // we get the Register returned by the webservice and not the one we generated client-side
                        // reason for this is that the server will set an ID etc. for the new register
                        Registers.Add(reg);
                        SelectedRegister = reg;
                    }

                }
            }
        }
        public async void DeleteRegister()
        {
            if (SelectedRegister != null)
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await
                    client.DeleteAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/register/" + SelectedRegister.ID);
                    if (response.IsSuccessStatusCode)
                    {
                        Registers.Remove(SelectedRegister);
                    }
                }
            }
        }
        public async void SaveRegister()
        {
            using (HttpClient client = new HttpClient())
            {
                string Register = JsonConvert.SerializeObject(SelectedRegister);
                HttpResponseMessage response = await
                client.PutAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/register/" + SelectedRegister.ID, new StringContent(Register, Encoding.UTF8, "application/json"));
            }
        }
    }
}
