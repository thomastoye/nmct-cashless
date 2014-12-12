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
    public class EmployeeController : ApiController
    {
        public List<Employee> Get()
        {
            return Employees.GetEmployees();
        }

        public Employee Post(Employee emp)
        {
            int id = Employees.InsertEmployee(emp);
            emp.ID = id;
            return emp;
        }

        public HttpStatusCode Put(long id, Employee emp)
        {
            Employees.UpdateEmployee(id, emp);
            return HttpStatusCode.OK;
        }

        public HttpStatusCode Delete(long id)
        {
            Employees.DeleteEmployee(id);
            return HttpStatusCode.OK;
        }
    }
}