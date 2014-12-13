using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;

namespace nmct.ba.cashlessproject.vereniging
{
    public class Webaccess
    {
        private static string URL = ConfigurationManager.AppSettings["apiUrl"];

        public static TokenResponse GetToken(string username, string password)
        {
            var client = new OAuth2Client(new Uri(URL + "token"));
            return client.RequestResourceOwnerPasswordAsync(username, password).Result;
        }

        public static async Task<bool> ChangePassword(string token, string oldPassword, string newPassword)
        {
            if (oldPassword == null || newPassword == null)
                return false;

            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(token);
                HttpResponseMessage response = await client.PostAsync(URL + "api/OrganisationApi/ChangePassword?oldPassword=" + WebUtility.UrlEncode(oldPassword) + "&newPassword=" + WebUtility.UrlEncode(newPassword), null);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<bool>(json);
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
