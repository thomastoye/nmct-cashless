using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace nmct.ba.cashlessproject.api.Controllers
{
    public class VerenigingApiController : ApiController
    {
        [Authorize(Roles="organisationManager")]
        [HttpGet]
        public string ConnectionString()
        {
            return "aoesunaosnehtu";
        }
    }
}