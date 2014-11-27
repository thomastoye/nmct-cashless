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
    public class Products
    {

        public static List<Product> GetProducts()
        {
            List<Product> list = new List<Product>();

            string sql = "SELECT ID, ProductName, Price FROM Products";
            DbDataReader reader = Database.GetData("KlantConnection", sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }


        public static void InsertProduct(Product Product)
        {
            string sql = "INSERT INTO Product(ProductName,Price) VALUES(@ProductName,@Price)";
            DbParameter par1 = Database.AddParameter("KlantConnection", "@ProductName", Product.Name);
            DbParameter par2 = Database.AddParameter("KlantConnection", "@Price", Product.Price);

            Database.InsertData("KlantConnection", sql, par1, par2);
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
    }
}