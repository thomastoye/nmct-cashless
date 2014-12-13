using nmct.ba.cashlessproject.api.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace nmct.ba.cashlessproject.api.Controllers
{
    public class VerenigingApiController : ApiController
    {
        [Authorize(Roles = "OrganisationManager")]
        [HttpGet]
        public string ConnectionString()
        {
            var name = User.Identity.Name;
            var isin = User.IsInRole("OrganisationManager");
            
            return Organisations.GetByUser(name).DatabaseConnectionString;
        }
    }
}