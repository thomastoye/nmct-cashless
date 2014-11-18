using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nmct.ba.cashlessproject.model;
using System.Data.Common;
using nmct.ba.cashlessproject.api.Helpers;
using System.Data;

namespace nmct.ba.cashlessproject.api.Models.DataAccess
{
    public class Customers
    {

        public static List<Customer> GetCustomers()
        {
            List<Customer> list = new List<Customer>();

            string sql = "SELECT ID, CustomerName, Address, Picture, Balance FROM Customers";
            DbDataReader reader = Database.GetData("ConnectionString", sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }

        
        public static int InsertCustomer(Customer customer)
        {
            string sql = "INSERT INTO Customers VALUES(@CustomerName,@Address,@Picture,@Balance)";
            DbParameter par1 = Database.AddParameter("ConnectionString", "@CustomerName", customer.Name);
            DbParameter par2 = Database.AddParameter("ConnectionString", "@Address", customer.Address);
            DbParameter par3 = Database.AddParameter("ConnectionString", "@Picture", null);
            DbParameter par4 = Database.AddParameter("ConnectionString", "@Balance", customer.Balance);
            return Database.InsertData("ConnectionString", sql, par1, par2, par3);
        }

        private static Customer Create(IDataRecord record)
        {
            return new Customer()
            {
                ID = Int32.Parse(record["ID"].ToString()),
                Address = record["Address"].ToString(),
                Balance = Double.Parse(record["Balance"].ToString()),
                Name = record["CustomerName"].ToString()
            };
        }
    }
}