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
    public class EmployeeController : ApiController
    {

        [Authorize(Roles = "OrganisationManager,Register")]
        public List<Employee> Get()
        {
            return Employees.GetEmployees(Claims.GetConnectionString(User));
        }

        [Authorize(Roles = "OrganisationManager")]
        public Employee Post(Employee emp)
        {
            int id = Employees.InsertEmployee(Claims.GetConnectionString(User), emp);
            emp.ID = id;
            return emp;
        }

        [Authorize(Roles = "OrganisationManager")]
        public HttpStatusCode Put(long id, Employee emp)
        {
            Employees.UpdateEmployee(Claims.GetConnectionString(User), id, emp);
            return HttpStatusCode.OK;
        }

        [Authorize(Roles = "OrganisationManager")]
        public HttpStatusCode Delete(long id)
        {
            Employees.DeleteEmployee(Claims.GetConnectionString(User), id);
            return HttpStatusCode.OK;
        }
    }
}