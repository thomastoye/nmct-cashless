﻿using nmct.ba.cashlessproject.api.Models.DataAccess;
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
    public class EmployeeController : ApiController
    {
        public List<Employee> Get()
        {
            return Employees.GetEmployees();
        }

        public HttpResponseMessage Post(Employee e)
        {
            Employees.InsertEmployee(e);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}