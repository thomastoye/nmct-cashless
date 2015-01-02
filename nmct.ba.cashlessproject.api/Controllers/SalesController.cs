using nmct.ba.cashlessproject.api.Helpers;
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
    [Authorize(Roles="Register")]
    public class SalesController : ApiController
    {
        public void Post(ProductOrder order, int customerID, int registerID)
        {
            ProductOrderDA.Insert(Claims.GetConnectionString(User), order, customerID, registerID);
        }
    }
}
