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
    class KlantenVM : ObservableObject, IPage
    {

        public string Name
        {
            get { return "Klanten"; }
        }

        public KlantenVM()
        {
            GetCustomers();
        }
        private async void GetCustomers()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await
                client.GetAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/customer");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Customers = JsonConvert.DeserializeObject<ObservableCollection<Customer>>(json);
                }
            }
        }
        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { _customers = value; OnPropertyChanged("Customers"); }
        }
        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { _selectedCustomer = value; OnPropertyChanged("SelectedCustomer"); }
        }
        public ICommand SaveCustomerCommand
        {
            get { return new RelayCommand(SaveCustomer); }
        }
        public ICommand RefreshCustomersCommand
        {
            get { return new RelayCommand(GetCustomers); }
        }
        public async void SaveCustomer()
        {
            using (HttpClient client = new HttpClient())
            {
                string Customer = JsonConvert.SerializeObject(SelectedCustomer);
                HttpResponseMessage response = await client.PutAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/customer/" + SelectedCustomer.ID, new StringContent(Customer, Encoding.UTF8, "application/json"));
            }
        }
    }
}
