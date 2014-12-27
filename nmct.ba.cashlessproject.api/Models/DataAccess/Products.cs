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
    public class Products
    {

        public static List<Product> GetProducts(ConnectionStringSettings connectionString)
        {
            List<Product> list = new List<Product>();

            string sql = "SELECT ID, ProductName, Price FROM Products";
            DbDataReader reader = Database.GetData(connectionString, sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }


        public static int InsertProduct(ConnectionStringSettings connectionString, Product Product)
        {
            string sql = "INSERT INTO Products(ProductName,Price) VALUES(@ProductName,@Price)";
            DbParameter par1 = Database.AddParameter(connectionString, "@ProductName", Product.Name);
            DbParameter par2 = Database.AddParameter(connectionString, "@Price", Product.Price);

            return Database.InsertData(connectionString, sql, par1, par2);
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

        public static void UpdateProduct(ConnectionStringSettings connectionString, long id, Product prod)
        {
            string sql = "UPDATE products SET ProductName=@ProductName,Price=@Price WHERE ID=@ID;";

            if (prod.Name == null) prod.Name = "";

            DbParameter parName = Database.AddParameter(connectionString, "@ProductName", prod.Name);
            DbParameter parPrice = Database.AddParameter(connectionString, "@Price", prod.Price);
            DbParameter parId = Database.AddParameter(connectionString, "@ID", id);

            Database.ModifyData(connectionString, sql, parName, parPrice, parId);
        }

        public static void DeleteProduct(ConnectionStringSettings connectionString, long id)
        {
            string sql = "DELETE FROM products WHERE ID=@ID";

            DbParameter parId = Database.AddParameter(connectionString, "@ID", id);

            Database.ModifyData(connectionString, sql, parId);
        }
    }
}