using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.klant.ViewModel
{
    class AddCustomerVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Nieuwe klant"; }
        }

        private Customer _cust = new Customer { Name = "Naam hier", Address = "Thuis" };

        public Customer NewCustomer
        {
            get { return _cust; }
            set { _cust = value; OnPropertyChanged("NewCustomer"); }
        }
        

        public ICommand CreateCustomerCommand
        {
            get { return new RelayCommand(CreateCustomer); }
        }

        private async void CreateCustomer()
        {

            using (HttpClient client = new HttpClient())
            {
                string customer = JsonConvert.SerializeObject(NewCustomer);
                HttpResponseMessage response = await client.PostAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/customer", new StringContent(customer, Encoding.UTF8, "application/json"));
            }
        }


    }
}
