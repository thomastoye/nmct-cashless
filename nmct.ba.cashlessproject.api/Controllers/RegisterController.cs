using nmct.ba.cashlessproject.api.Helpers;
using nmct.ba.cashlessproject.api.Models.DataAccess;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace nmct.ba.cashlessproject.api.Controllers
{
    [System.Web.Http.Authorize(Roles = "OrganisationManager")]
    public class RegisterController : ApiController
    {
        public List<RegisterOrganisation> Get()
        {
            return RegistersOrganisation.Get(Claims.GetConnectionString(User));
        }

        public RegisterOrganisation Post(RegisterOrganisation r)
        {
            int id = RegistersOrganisation.Insert(Claims.GetConnectionString(User), r);
            r.ID = id;

            return r;
        }

        public HttpStatusCode Put(long id, RegisterOrganisation r)
        {
            RegistersOrganisation.Update(Claims.GetConnectionString(User), id, r);
            return HttpStatusCode.OK;
        }

        public HttpStatusCode Delete(long id)
        {
            RegistersOrganisation.Delete(Claims.GetConnectionString(User), id);
            return HttpStatusCode.OK;
        }
    }
}