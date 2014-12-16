using nmct.ba.cashlessproject.api.Models.DataAccess;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace nmct.ba.cashlessproject.api.Controllers
{
    public class RegisterController : ApiController
    {
        public List<RegisterOrganisation> Get()
        {
            return RegistersOrganisation.GetRegisters();
        }

        public RegisterOrganisation Post(RegisterOrganisation r)
        {
            int id = RegistersOrganisation.InsertRegister(r);
            r.ID = id;

            return r;
        }

        public HttpStatusCode Put(long id, RegisterOrganisation r)
        {
            RegistersOrganisation.UpdateRegister(id, r);
            return HttpStatusCode.OK;
        }

        public HttpStatusCode Delete(long id)
        {
            RegistersOrganisation.DeleteRegister(id);
            return HttpStatusCode.OK;
        }
    }
}