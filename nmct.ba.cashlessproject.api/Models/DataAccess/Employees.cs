using nmct.ba.cashlessproject.api.Helpers;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Models.DataAccess
{
    public class Employees
    {
        public static List<Employee> GetEmployees(ConnectionStringSettings connectionString)
        {
            List<Employee> list = new List<Employee>();

            string sql = "SELECT ID, EmployeeName, Address, Email, Phone FROM Employee";
            DbDataReader reader = Database.GetData(connectionString, sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }


        public static int InsertEmployee(ConnectionStringSettings connectionString, Employee employee)
        {
            string sql = "INSERT INTO Employee(EmployeeName,Address,Email,Phone) VALUES(@EmployeeName,@Address,@Email,@Phone)";

            if (employee.Address == null) employee.Address = "";
            if (employee.Email == null) employee.Email = "";
            if (employee.Name == null) employee.Name = "";
            if (employee.Phone == null) employee.Phone = "";

            DbParameter parName = Database.AddParameter(connectionString, "@EmployeeName", employee.Name);
            DbParameter parAddress = Database.AddParameter(connectionString, "@Address", employee.Address);
            DbParameter parMail = Database.AddParameter(connectionString, "@Email", employee.Email);
            DbParameter parPhone = Database.AddParameter(connectionString, "@Phone", employee.Phone);

            return Database.InsertData(connectionString, sql, parName, parAddress, parMail, parPhone);
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

        public static void UpdateEmployee(ConnectionStringSettings connectionString, long id, Employee employee)
        {
            string sql = "UPDATE employee SET EmployeeName=@EmployeeName,Address=@Address,Email=@Email,Phone=@Phone WHERE ID=@ID;";

            DbParameter parName = Database.AddParameter(connectionString, "@EmployeeName", employee.Name);
            DbParameter parAddress = Database.AddParameter(connectionString, "@Address", employee.Address);
            DbParameter parMail = Database.AddParameter(connectionString, "@Email", employee.Email);
            DbParameter parPhone = Database.AddParameter(connectionString, "@Phone", employee.Phone);

            DbParameter parId = Database.AddParameter(connectionString, "@ID", employee.ID);

            Database.ModifyData(connectionString, sql, parName, parAddress, parMail, parPhone, parId);
        }

        public static void DeleteEmployee(ConnectionStringSettings connectionString, long id)
        {
            string sql = "DELETE FROM employee WHERE ID=@ID";

            DbParameter parId = Database.AddParameter(connectionString, "@ID", id);

            Database.ModifyData(connectionString, sql, parId);
        }
    }
}