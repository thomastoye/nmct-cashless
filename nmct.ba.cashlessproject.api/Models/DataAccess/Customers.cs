using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nmct.ba.cashlessproject.model;
using System.Data.Common;
using nmct.ba.cashlessproject.api.Helpers;
using System.Data;
using System.Configuration;
using nmct.ba.cashlessproject.common;
using System.Drawing;

namespace nmct.ba.cashlessproject.api.Models.DataAccess
{
    public class Customers
    {

        public static List<Customer> GetCustomers()
        {
            List<Customer> list = new List<Customer>();

            string sql = "SELECT ID, CustomerName, Address, Picture, Balance FROM Customers";
            DbDataReader reader = Database.GetData(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }

        
        public static int InsertCustomer(Customer customer)
        {
            string sql = "INSERT INTO Customers(CustomerName,Address,Balance, Picture) VALUES(@CustomerName,@Address,@Balance, @Picture)";

            if (customer.Name == null) customer.Name = "";
            if (customer.Address == null) customer.Address = "";

            DbParameter par1 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@CustomerName", customer.Name);
            DbParameter par2 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@Address", customer.Address);
            DbParameter par3 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@Picture", customer.Image);
            DbParameter par4 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@Balance", customer.Balance);

            return Database.InsertData(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], sql, par1, par2, par3, par4);
        }

        private static Customer Create(IDataRecord record)
        {
            byte[] bytes = null;
            if (record["Picture"] != null && record["Picture"] != DBNull.Value && record["Picture"] is byte[])
            {
                bytes = (byte[])record["Picture"];
            }

            return new Customer()
            {
                ID = Int32.Parse(record["ID"].ToString()),
                Address = record["Address"].ToString(),
                Balance = Double.Parse(record["Balance"].ToString()),
                Name = record["CustomerName"].ToString(),
                Image = bytes
            };
        }

        public static void UpdateCustomer(long id, Customer customer)
        {
            string sql = "UPDATE customers SET CustomerName=@CustomerName,Address=@Address,Picture=@Picture,Balance=@Balance WHERE ID=@ID;";

            DbParameter custName = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@CustomerName", customer.Name);
            DbParameter custAddress = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@Address", customer.Address);
            DbParameter custId = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@ID", id);
            DbParameter custPic = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@Picture", customer.Image);
            DbParameter custBalance = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@Balance", customer.Balance);

            Database.ModifyData(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], sql, custName, custAddress, custId, custPic, custBalance);
        }

        public static void DeleteCustomer(long id)
        {
            string sql = "DELETE FROM customers WHERE ID=@ID";

            DbParameter parId = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@ID", id);

            Database.ModifyData(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], sql, parId);
        }

        public static Customer ExistsWithName(string name)
        {
            string sql = "SELECT * FROM customers WHERE CustomerName=@Name";

            DbParameter par = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@Name", name);

            DbDataReader reader = Database.GetData(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], sql, par);

            Customer res = null;
            if (reader.Read())
                res = Create(reader);

            reader.Close();
            return res;
        }
    }
}