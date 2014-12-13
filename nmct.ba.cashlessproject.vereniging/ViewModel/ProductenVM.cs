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
    class ProductenVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Producten"; }
        }

         public ProductenVM()
        {
            GetProducts();
        }
        private async void GetProducts()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await
                client.GetAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/Product");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(json);
                }
            }
        }
        private ObservableCollection<Product> _Products;
        public ObservableCollection<Product> Products
        {
            get { return _Products; }
            set { _Products = value; OnPropertyChanged("Products"); }
        }
        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged("SelectedProduct"); }
        }
        public ICommand AddProductCommand
        {
            get { return new RelayCommand(AddProduct); }
        }
        public ICommand DeleteProductCommand
        {
            get { return new RelayCommand(DeleteProduct); }
        }
        public ICommand SaveProductCommand
        {
            get { return new RelayCommand(SaveProduct); }
        }
        public ICommand RefreshProductsCommand
        {
            get { return new RelayCommand(GetProducts); }
        }
        public async void AddProduct()
        {
            Product newProduct = new Product();
            using (HttpClient client = new HttpClient())
            {
                string Product = JsonConvert.SerializeObject(newProduct);
                HttpResponseMessage response = await client.PostAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/product", new StringContent(Product, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Product emp = JsonConvert.DeserializeObject<Product>(json);
                    if (emp != null)
                    {
                        Products.Add(emp);
                        SelectedProduct = emp;
                    }
                }
            }
        }
        public async void DeleteProduct()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await
                client.DeleteAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/product/" + SelectedProduct.ID);
                if (response.IsSuccessStatusCode)
                {
                    Products.Remove(SelectedProduct);
                }
            }
        }
        public async void SaveProduct()
        {
            using (HttpClient client = new HttpClient())
            {
                string Product = JsonConvert.SerializeObject(SelectedProduct);
                HttpResponseMessage response = await
                client.PutAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/product/" + SelectedProduct.ID, new StringContent(Product, Encoding.UTF8, "application/json"));
            }
        }
    }
}
