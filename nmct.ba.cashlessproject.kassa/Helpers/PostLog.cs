using Newtonsoft.Json;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.kassa.Helpers
{
    public class PostLog
    {
        // returns true if posting the log succeeded
        public async static Task<bool> Post(Exception e)
        {
            string kassa = ConfigurationManager.AppSettings["username"];

            var log = new ErrorLog() {
                Timestamp = DateTime.Now,
                Message = "Kassa " + kassa + ": " + e.Message,
                StackTrace = e.StackTrace,
                RegisterID = Int32.Parse(kassa)
            };

            using (HttpClient client = new HttpClient())
            {
                //client.SetBearerToken(ConfigurationManager.AppSettings["token"]);
                string logString = JsonConvert.SerializeObject(log);
                HttpResponseMessage response = await client.PostAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/log", new StringContent(logString, Encoding.UTF8, "application/json"));
                return response.IsSuccessStatusCode;
            }
        }
    }
}
