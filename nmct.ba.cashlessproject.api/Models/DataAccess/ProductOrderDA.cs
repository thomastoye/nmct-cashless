using nmct.ba.cashlessproject.api.Helpers;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Models.DataAccess
{
    public class ProductOrderDA
    {
        public static int Insert(ConnectionStringSettings settings, ProductOrder order, int customerID, int registerID) {
            if (order == null || order.Product == null) return 0;

            string sql = "INSERT INTO Sales VALUES(@Timestamp, @CustomerID, @RegisterID, @ProductID, @Amount, @TotalPrice)";

            DbParameter par0 = Database.AddParameter(settings, "@Timestamp", DateTime.Now);
            DbParameter par1 = Database.AddParameter(settings, "@CustomerID", customerID);
            DbParameter par2 = Database.AddParameter(settings, "@RegisterID", registerID); ;
            DbParameter par3 = Database.AddParameter(settings, "@ProductID", order.Product.ID);
            DbParameter par4 = Database.AddParameter(settings, "@Amount", order.Quantity);
            DbParameter par5 = Database.AddParameter(settings, "@TotalPrice", order.Quantity * order.Product.Price);


            return Database.InsertData(settings, sql, par0, par1, par2, par3, par4, par5);
        }
    }
}