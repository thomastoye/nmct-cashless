﻿using nmct.ba.cashlessproject.api.Models.DataAccess;
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

        public List<Customer> Get()
        {
            return Customers.GetCustomers();
        }

        public Customer Post(Customer c)
        {
            int id = Customers.InsertCustomer(c);
            c.ID = id;

            return c;
        }

        public HttpStatusCode Put(long id, Customer c)
        {
            Customers.UpdateCustomer(id, c);
            return HttpStatusCode.OK;
        }

        public HttpStatusCode Delete(long id)
        {
            Customers.DeleteCustomer(id);
            return HttpStatusCode.OK;
        }


        [HttpGet]
        public object Exists(string name)
        {
            return Customers.ExistsWithName(name);
        }
    }
}