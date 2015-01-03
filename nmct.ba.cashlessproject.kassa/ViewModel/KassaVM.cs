using be.belgium.eid;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.ComponentModel.DataAnnotations;
using nmct.ba.cashlessproject.model;
using System.Windows.Threading;
using System.Net.Http;
using Newtonsoft.Json;
using System.Configuration;
using System.Collections.ObjectModel;
using System.Net;
using System.Drawing;
using nmct.ba.cashlessproject.kassa.Converters;
using System.Windows.Media;
using System.IO;
using System.Drawing.Imaging;
using nmct.ba.cashlessproject.common;

namespace nmct.ba.cashlessproject.kassa.ViewModel
{
    class KassaVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Kassa"; }
        }

        private Customer _klant = new Customer();

        public Customer Klant
        {
            get { return _klant; }
            set { _klant = value; OnPropertyChanged("Klant"); OnPropertyChanged("KanBestellingPlaatsen"); }
        }

        private bool _isNogNietGeregistreerd = true;

        public bool IsNogNietGeregistreerd
        {
            get { return _isNogNietGeregistreerd; }
            set { _isNogNietGeregistreerd = value;
                OnPropertyChanged("IsNogNietGeregistreerd");
                OnPropertyChanged("IsAlGeregistreerd");
                OnPropertyChanged("KanBestellingPlaatsen"); }
        }

        public bool CanIncreaseQuantity
        {
            get
            {
                if (SelectedProduct == null || SelectedProduct.Product == null) return false;
                return OrderTotal + SelectedProduct.Product.Price < 100;
            }
        }

        public bool CanDecreaseQuantity {
            get {
                if (SelectedProduct == null || SelectedProduct.Product == null) return false;
                return SelectedProduct.Quantity != 0;
            }
        }

        public bool IsAlGeregistreerd { get { return !IsNogNietGeregistreerd; } }

        private ObservableCollection<ProductOrder> _producten = new ObservableCollection<ProductOrder>();

        public ObservableCollection<ProductOrder> Producten
        {
            get { return _producten; }
            set { _producten = value; OnPropertyChanged("Producten"); }
        }

        private ProductOrder _selectedProduct;

        public ProductOrder SelectedProduct
        {
            get { return _selectedProduct; }
            set {
                _selectedProduct = value;
                OnPropertyChanged("KanBestellingPlaatsen");
                OnPropertyChanged("SelectedProduct");
                OnPropertyChanged("OrderTotal");
                OnPropertyChanged("CanDecreaseQuantity");
                OnPropertyChanged("CanIncreaseQuantity");
                OnPropertyChanged("OrderTotalTooHigh");
            }
        }

        public double OrderTotal
        {
            get {
                if (SelectedProduct == null || SelectedProduct.Product == null) return 0;
                return SelectedProduct.Quantity * SelectedProduct.Product.Price;
            }
        }

        public bool OrderTotalTooHigh {
            get {
                if (SelectedProduct == null || Klant == null || SelectedProduct.Product == null) return false;

                return OrderTotal > Klant.Balance;
            }
        }

        private ObservableCollection<Employee> _employees;

        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set { _employees = value; OnPropertyChanged("Employees"); }
        }

        private Employee _selectedEmployee;

        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value; OnPropertyChanged("SelectedEmployee"); OnPropertyChanged("KanBestellingPlaatsen"); }
        }

        public bool KanBestellingPlaatsen {
            get {
                return SelectedEmployee != null && Klant != null && IsAlGeregistreerd && SelectedProduct != null;
            }
        }

        private string _error;

        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }
        

        public KassaVM()
        {
            // set up SDK
            BEID_ReaderSet.initSDK();

        }

        public ICommand LaadEidCommand
        {
            get { return new RelayCommand(LaadEid); }
        }

        public ICommand ConfirmOrderCommand
        {
            get { return new RelayCommand(ConfirmOrder); }
        }
        public ICommand ReloadCommand
        {
            get { return new RelayCommand(Reload); }
        }

        public ICommand IncreaseProductQuantityCommand
        {
            get { return new RelayCommand(IncreaseProductQuantity); }
        }

        public ICommand DecreaseProductQuantityCommand
        {
            get { return new RelayCommand(DecreaseProductQuantity); }
        }

        public ICommand SaveCustomerCommand
        {
            get { return new RelayCommand(SaveCustomer); }
        }

        public async void SaveCustomer()
        {
            if (Klant == null) return;

            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ConfigurationManager.AppSettings["token"]);
                string Customer = JsonConvert.SerializeObject(Klant);
                HttpResponseMessage response = await client.PutAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/customer/" + Klant.ID, new StringContent(Customer, Encoding.UTF8, "application/json"));
            }
        }

        private async void IncreaseProductQuantity()
        {
            SelectedProduct.Quantity++;

            OnPropertyChanged("SelectedProduct");
            OnPropertyChanged("Producten");
            OnPropertyChanged("OrderTotal");
            OnPropertyChanged("CanIncreaseQuantity");
            OnPropertyChanged("CanDecreaseQuantity");
            OnPropertyChanged("OrderTotalTooHigh");
        }

        private async void DecreaseProductQuantity()
        {
            SelectedProduct.Quantity--;

            OnPropertyChanged("SelectedProduct");
            OnPropertyChanged("Producten");
            OnPropertyChanged("OrderTotal");
            OnPropertyChanged("CanDecreaseQuantity");
            OnPropertyChanged("CanIncreaseQuantity");
            OnPropertyChanged("OrderTotalTooHigh");
        }

        private async void Reload()
        {
            // reload products
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ConfigurationManager.AppSettings["token"]);
                HttpResponseMessage response = await
                client.GetAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/product");
                try
                {
                    response.EnsureSuccessStatusCode();
                    string json = await response.Content.ReadAsStringAsync();
                    var products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(json);

                    Producten = ProductOrder.ConstructsFromProducts(products);
                } catch(Exception e) {
                    Helpers.PostLog.Post(e);
                }
            }

            //reload employees
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ConfigurationManager.AppSettings["token"]);
                HttpResponseMessage response = await
                client.GetAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/employee");
                try
                {
                    response.EnsureSuccessStatusCode();
                    string json = await response.Content.ReadAsStringAsync();
                    Employees = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(json);

                    if(Employees.Count > 0) SelectedEmployee = Employees[0];
                }
                catch (Exception e)
                {
                    Helpers.PostLog.Post(e);
                }
            }
        }

        private async void ConfirmOrder()
        {
            if (KanBestellingPlaatsen)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.SetBearerToken(ConfigurationManager.AppSettings["token"]);
                    string order = JsonConvert.SerializeObject(SelectedProduct);
                    string registerID = ConfigurationManager.AppSettings["username"];
                    HttpResponseMessage response = await client.PostAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/Sales/?registerID=" + registerID + "&customerID=" + Klant.ID,
                        new StringContent(order, Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        Klant.Balance -= OrderTotal;
                        SelectedProduct.Quantity = 0;

                        OnPropertyChanged("SelectedProduct");
                        OnPropertyChanged("Producten");
                        OnPropertyChanged("OrderTotal");
                        OnPropertyChanged("CanDecreaseQuantity");
                        OnPropertyChanged("CanIncreaseQuantity");
                        OnPropertyChanged("OrderTotalTooHigh");
                        OnPropertyChanged("Klant");
                    }
                }
            }
            else
            {
                if(!IsAlGeregistreerd)
                    Helpers.PostLog.Post(new Exception("Klant was nog niet geregistreerd maar bestelling werd toch geplaatst"));

                if (SelectedEmployee == null)
                    Helpers.PostLog.Post(new Exception("Er was geen medewerker geselecteerd maar bestelling werd toch geplaatst"));

                if (Klant == null)
                    Helpers.PostLog.Post(new Exception("Er was geen klant geselecteerd maar bestelling werd toch geplaatst"));
            }
        }

        private async void ControleerOfKlantAlGeregistreerd()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ConfigurationManager.AppSettings["token"]);
                HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/customer/exists?name=" + WebUtility.HtmlEncode(Klant.Name));
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Customer bestaatAl = JsonConvert.DeserializeObject<Customer>(json);

                    if (bestaatAl != null)
                    {
                        Klant = bestaatAl;
                        IsNogNietGeregistreerd = false;
                    }
                    else
                    {
                        IsNogNietGeregistreerd = true;
                        Error = "Klant nog niet geregistreerd!";
                    }
                }
            }
        }

        private async void LaadEid()
        {
            IsNogNietGeregistreerd = false;
            Error = "";

            try
            {
                if (BEID_ReaderSet.instance().readerCount() > 0)
                {
                    BEID_ReaderContext readerContext = readerContext = BEID_ReaderSet.instance().getReader();
                    if (readerContext != null)
                    {
                        if (readerContext.getCardType() == BEID_CardType.BEID_CARDTYPE_EID)
                        {
                            BEID_EIDCard card = readerContext.getEIDCard();
                            BEID_Picture picture = card.getPicture();
                            byte[] bytearray = picture.getData().GetBytes();

                            try
                            {
                                Customer newCustomer = new Customer()
                                {
                                    Name = card.getID().getFirstName() + " " + card.getID().getSurname(),
                                    Address = card.getID().getStreet() + " " + card.getID().getZipCode(),
                                    Image = bytearray

                                };

                                Klant = newCustomer;

                                OnPropertyChanged("SelectedProduct");
                                OnPropertyChanged("Producten");
                                OnPropertyChanged("OrderTotal");
                                OnPropertyChanged("CanDecreaseQuantity");
                                OnPropertyChanged("CanIncreaseQuantity");
                                OnPropertyChanged("OrderTotalTooHigh");
                                OnPropertyChanged("Klant");
                                ControleerOfKlantAlGeregistreerd();
                            }
                            catch (Exception e)
                            {
                                Error = "Er was een fout bij het converteren van je foto.";
                                Helpers.PostLog.Post(e);
                            }

                        }
                        else
                        {
                            Error = "Kon geen kaart vinden. Probeer opnieuw.";
                        }
                    }
                    else
                    {
                        Error = "Kon geen kaart vinden. Probeer opnieuw.";
                        await Helpers.PostLog.Post(new Exception("readerContext was null"));
                    }
                }
                else
                {
                    Error = "Kon geen kaartlezer vinden. Probeer opnieuw.";
                    await Helpers.PostLog.Post(new Exception("BEID_ReaderSet.instance().readerCount() was 0"));
                }
            }
            catch(Exception e)
            {
                Error = "Kon geen kaart vinden. Probeer opnieuw.";
                Helpers.PostLog.Post(e);
            }

        }
        
    }
}
