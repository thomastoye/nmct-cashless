using nmct.ba.cashlessproject.api.Helpers;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Models.DataAccess
{
    public class Employees
    {
        public static List<Employee> GetEmployees()
        {
            List<Employee> list = new List<Employee>();

            string sql = "SELECT ID, EmployeeName, Address, Email, Phone FROM Employee";
            DbDataReader reader = Database.GetData("KlantConnection", sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }


        public static void InsertEmployee(Employee employee)
        {
            string sql = "INSERT INTO Employee(EmployeeName,Address,Email,Phone) VALUES(@EmployeeName,@Address,@Email,@Phone)";
            DbParameter par1 = Database.AddParameter("KlantConnection", "@EmployeeName", employee.Name);
            DbParameter par2 = Database.AddParameter("KlantConnection", "@Address", employee.Address);
            DbParameter par3 = Database.AddParameter("KlantConnection", "@Email", employee.Email);
            DbParameter par4 = Database.AddParameter("KlantConnection", "@Phone", employee.Phone);

            Database.InsertData("KlantConnection", sql, par1, par2, par3, par4);
        }

        private static Employee Create(IDataRecord record)
        {
            return new Employee()
            {
                ID = Int32.Parse(record["ID"].ToString()),
                Address = record["Address"].ToString(),
                Email = record["Email"].ToString(),
                Name = record["EmployeeName"].ToString(),
                Phone = record["Phone"].ToString()
            };
        }
    }
}