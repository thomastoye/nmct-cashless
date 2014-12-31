﻿using be.belgium.eid;
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
            set { _klant = value; OnPropertyChanged("Klant"); }
        }

        private bool _isNogNietGeregistreerd = true;

        public bool IsNogNietGeregistreerd
        {
            get { return _isNogNietGeregistreerd; }
            set { _isNogNietGeregistreerd = value; OnPropertyChanged("IsNogNietGeregistreerd"); OnPropertyChanged("IsAlGeregistreerd"); }
        }

        public bool IsAlGeregistreerd { get { return !IsNogNietGeregistreerd; } }

        private ObservableCollection<ProductOrder> _besteldeProducten = new ObservableCollection<ProductOrder>();

        public ObservableCollection<ProductOrder> BesteldeProducten
        {
            get { return _besteldeProducten; }
            set { _besteldeProducten = value; OnPropertyChanged("BesteldeProducten"); }
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

        private async void Reload()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ConfigurationManager.AppSettings["token"]);
                HttpResponseMessage response = await
                client.GetAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/product");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(json);

                    BesteldeProducten = ProductOrder.ConstructsFromProducts(products);
                }
            }
        }

        private async void ConfirmOrder()
        {
            if (!IsNogNietGeregistreerd)
            {
                // ...
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
                                ControleerOfKlantAlGeregistreerd();
                            }
                            catch (Exception e)
                            {
                                Error = "Er was een fout bij het converteren van je foto.";
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
                    }
                }
                else
                {
                    Error = "Kon geen kaart vinden. Probeer opnieuw.";
                }
            }
            catch
            {
                Error = "Kon geen kaart vinden. Probeer opnieuw.";
            }

        }
        
    }
}