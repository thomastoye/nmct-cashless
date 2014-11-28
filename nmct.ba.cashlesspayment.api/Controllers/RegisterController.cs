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
        public List<Register> Get()
        {
            return Registers.GetRegisters();
        }

        public Register Post(Register r)
        {
            int id = Registers.InsertRegister(r);
            r.ID = id;

            return r;
        }

        public HttpStatusCode Put(long id, Register r)
        {
            Registers.UpdateRegister(id, r);
            return HttpStatusCode.OK;
        }

        public HttpStatusCode Delete(long id)
        {
            Registers.Delete(id);
            return HttpStatusCode.OK;
        }
    }
}