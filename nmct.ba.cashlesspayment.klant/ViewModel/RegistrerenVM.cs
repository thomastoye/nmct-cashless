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
using nmct.ba.cashlessproject.model;
using System.Windows.Threading;
using System.Net.Http;
using Newtonsoft.Json;
using System.Configuration;
using System.Collections.ObjectModel;
using System.Net;
using System.Drawing;
using nmct.ba.cashlessproject.klant.Converters;
using System.Windows.Media;
using System.IO;
using System.Drawing.Imaging;
using nmct.ba.cashlessproject.common;
using Thinktecture.IdentityModel.Client;

namespace nmct.ba.cashlessproject.klant.ViewModel
{
    class RegistrerenVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Registreren"; }
        }

        private Customer _klant = new Customer();

        public Customer Klant
        {
            get { return _klant; }
            set { _klant = value; OnPropertyChanged("Klant"); }
        }

        private bool _isNogNietGeregistreerd = false;

        public bool IsNogNietGeregistreerd
        {
            get { return _isNogNietGeregistreerd; }
            set { _isNogNietGeregistreerd = value; OnPropertyChanged("IsNogNietGeregistreerd"); }
        }

        private bool _kanOpladen = false;

        public bool KanKaartOpladen
        {
            get { return _kanOpladen; }
            set { _kanOpladen = value; OnPropertyChanged("KanKaartOpladen"); }
        }

        public string StatusMessage { get; set; }

        private string _error;

        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }

        public string Token { get; set; }


        public RegistrerenVM()
        {
            // get OAuth token
            string username = ConfigurationManager.AppSettings["username"];
            string password = ConfigurationManager.AppSettings["password"];

            var client = new OAuth2Client(new Uri(ConfigurationManager.AppSettings["apiUrl"] + "token"));
            TokenResponse result = client.RequestResourceOwnerPasswordAsync(username, password).Result;
            if (result.Error == null)
            {
                Token = result.AccessToken;
                StatusMessage = "Succelvol geauthenticeerd over OAuth.";
            }
            else
            {
                StatusMessage = "Kon niet authenticeren over OAuth!";
            }

            // set up SDK
            BEID_ReaderSet.initSDK();
        }

        public ICommand LaadEidCommand
        {
            get { return new RelayCommand(LaadEid); }
        }

        public ICommand RegisterClientCommand
        {
            get { return new RelayCommand(RegisterClient); }
        }

        private async void RegisterClient()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(Token);
                string customer = JsonConvert.SerializeObject(Klant);
                HttpResponseMessage response = await client.PostAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/customer", new StringContent(customer, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    IsNogNietGeregistreerd = false;
                    KanKaartOpladen = true;
                }
            }
        }

        private async void ControleerOfKlantAlGeregistreerd()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(Token);
                HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/customer/exists?name=" + WebUtility.HtmlEncode(Klant.Name));
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Customer bestaatAl = JsonConvert.DeserializeObject<Customer>(json);

                    if (bestaatAl != null)
                    {
                        KanKaartOpladen = true;
                        Klant = bestaatAl;
                    }
                    else
                    {
                        IsNogNietGeregistreerd = true;
                    }
                }
            }
        }

        private async void LaadEid()
        {
            KanKaartOpladen = false;
            IsNogNietGeregistreerd = false;
            StatusMessage = "";
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
                                //Image img = StringToImageConverter.ImageFromBytes(bytearray);
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
                //BEID_ReaderSet.releaseSDK();
            }
            catch
            {
                Error = "Kon geen kaart vinden. Probeer opnieuw.";
            }


        }
        
    }
}
