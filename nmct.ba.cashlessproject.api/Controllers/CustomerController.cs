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
    [Authorize(Roles = "OrganisationManager")]
    public class CustomerController : ApiController
    {

        public List<Customer> Get()
        {
            return Customers.GetCustomers(Claims.GetConnectionString(User));
        }

        public Customer Post(Customer c)
        {
            int id = Customers.InsertCustomer(Claims.GetConnectionString(User), c);
            c.ID = id;

            return c;
        }

        public HttpStatusCode Put(long id, Customer c)
        {
            Customers.UpdateCustomer(Claims.GetConnectionString(User), id, c);
            return HttpStatusCode.OK;
        }

        public HttpStatusCode Delete(long id)
        {
            Customers.DeleteCustomer(Claims.GetConnectionString(User), id);
            return HttpStatusCode.OK;
        }


        [HttpGet]
        public Customer Exists(string name)
        {
            return Customers.ExistsWithName(Claims.GetConnectionString(User), name);
        }
    }
}