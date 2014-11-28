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
                client.GetAsync(ConfigurationManager.AppSettings["apiUrl"] + "/api/customer");
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
        public ICommand AddCustomerCommand
        {
            get { return new RelayCommand(AddCustomer); }
        }
        public ICommand DeleteCustomerCommand
        {
            get { return new RelayCommand(DeleteCustomer); }
        }
        public ICommand SaveCustomerCommand
        {
            get { return new RelayCommand(SaveCustomer); }
        }
        public ICommand RefreshCustomersCommand
        {
            get { return new RelayCommand(RefreshCustomers); }
        }
        public async void AddCustomer()
        {
            Customer newCustomer = new Customer();
            using (HttpClient client = new HttpClient())
            {
                string customer = JsonConvert.SerializeObject(newCustomer);
                HttpResponseMessage response = await client.PostAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/customer", new StringContent(customer, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Customer cust = JsonConvert.DeserializeObject<Customer>(json);
                    if (cust != null)
                    {
                        Customers.Add(cust);
                        SelectedCustomer = cust;
                    }
                }
            }
        }
        public async void DeleteCustomer()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await
                client.DeleteAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/customer/" + SelectedCustomer.ID);
                if (response.IsSuccessStatusCode)
                {
                    Customers.Remove(SelectedCustomer);
                }
            }
        }
        public async void SaveCustomer()
        {
            using (HttpClient client = new HttpClient())
            {
                string Customer = JsonConvert.SerializeObject(SelectedCustomer);
                HttpResponseMessage response = await client.PutAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/customer/" + SelectedCustomer.ID, new StringContent(Customer, Encoding.UTF8, "application/json"));
            }
        }
        public async void RefreshCustomers()
        {
            GetCustomers();
        }
    }
}
