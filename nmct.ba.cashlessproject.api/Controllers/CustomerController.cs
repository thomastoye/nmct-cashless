﻿using nmct.ba.cashlessproject.api.Helpers;
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
    public class CustomerController : ApiController
    {
        [Authorize(Roles = "OrganisationManager")]
        public List<Customer> Get()
        {
            return Customers.GetCustomers(Claims.GetConnectionString(User));
        }

        [Authorize(Roles = "OrganisationManager,Register")]
        public Customer Post(Customer c)
        {
            int id = Customers.InsertCustomer(Claims.GetConnectionString(User), c);
            c.ID = id;

            return c;
        }

        [Authorize(Roles = "OrganisationManager,Register")]
        public HttpStatusCode Put(long id, Customer c)
        {
            Customers.UpdateCustomer(Claims.GetConnectionString(User), id, c);
            return HttpStatusCode.OK;
        }

        [Authorize(Roles = "OrganisationManager")]
        public HttpStatusCode Delete(long id)
        {
            Customers.DeleteCustomer(Claims.GetConnectionString(User), id);
            return HttpStatusCode.OK;
        }

        [System.Web.Http.Authorize(Roles = "OrganisationManager,Register")]
        [HttpGet]
        public Customer Exists(string name)
        {
            return Customers.ExistsWithName(Claims.GetConnectionString(User), name);
        }
    }
}