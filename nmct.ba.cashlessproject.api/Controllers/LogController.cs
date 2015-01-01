using nmct.ba.cashlessproject.api.Models.DataAccess;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace nmct.ba.cashlessproject.api.Controllers
{
    public class LogController : ApiController
    {
        [AllowAnonymous]
        public void Post(ErrorLog log)
        {
            if (ModelState.IsValid)
            {
                LogDA.Insert(log);
            }
        }
    }
}
