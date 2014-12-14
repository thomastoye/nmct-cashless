﻿using nmct.ba.cashlessproject.api.Helpers;
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
    public class Products
    {

        public static List<Product> GetProducts()
        {
            List<Product> list = new List<Product>();

            string sql = "SELECT ID, ProductName, Price FROM Products";
            DbDataReader reader = Database.GetData(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }


        public static int InsertProduct(Product Product)
        {
            string sql = "INSERT INTO Product(ProductName,Price) VALUES(@ProductName,@Price)";
            DbParameter par1 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@ProductName", Product.Name);
            DbParameter par2 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@Price", Product.Price);

            return Database.InsertData(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], sql, par1, par2);
        }

        private static Product Create(IDataRecord record)
        {
            return new Product()
            {
                ID = Int32.Parse(record["ID"].ToString()),
                Price = Double.Parse(record["Price"].ToString()),
                Name = record["ProductName"].ToString()
            };
        }

        public static void UpdateProduct(long id, Product prod)
        {
            string sql = "UPDATE products SET ProductName=@ProductName,Price=@Price WHERE ID=@ID;";

            if (prod.Name == null) prod.Name = "";

            DbParameter parName = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@ProductName", prod.Name);
            DbParameter parPrice = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@Price", prod.Price);
            DbParameter parId = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@ID", id);

            Database.ModifyData(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], sql, parName, parPrice, parId);
        }

        public static void DeleteProduct(long id)
        {
            string sql = "DELETE FROM products WHERE ID=@ID";

            DbParameter parId = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@ID", id);

            Database.ModifyData(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], sql, parId);
        }
    }
}