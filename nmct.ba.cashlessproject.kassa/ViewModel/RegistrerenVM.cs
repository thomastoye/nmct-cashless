using be.belgium.eid;
using GalaSoft.MvvmLight.CommandWpf;
using nmct.ba.cashlessproject.kassa.View.Converters;
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

namespace nmct.ba.cashlessproject.kassa.ViewModel
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
            set { _klant = value; OnPropertyChanged("Klant"); ControleerOfKlantAlGeregistreerd(); }
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


        public RegistrerenVM()
        {
            // set up SDK
            BEID_ReaderSet.initSDK();
        }

        public ICommand LaadEidCommand
        {
            get { return new RelayCommand(LaadEid); }
        }

        private async void ControleerOfKlantAlGeregistreerd()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/customer/exists?name=" + WebUtility.HtmlEncode(Klant.Name));
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    bool bestaatAl = JsonConvert.DeserializeObject<Boolean>(json);

                    if (bestaatAl)
                    {
                        KanKaartOpladen = true;
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

                            Customer newCustomer = new Customer() {
                                Name = card.getID().getFirstName() + " " + card.getID().getSurname(),
                                Address = card.getID().getStreet() + " " + card.getID().getZipCode(),
                                Image = StringToImageConverter.BitmapImageFromBytes(bytearray)

                            };

                            Klant = newCustomer;

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
