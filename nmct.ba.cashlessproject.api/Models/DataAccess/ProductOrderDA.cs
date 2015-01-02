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
        public static void Process(ConnectionStringSettings settings, ProductOrder order, int customerID, int registerID) {
            Customer customer = Customers.GetCustomers(settings).First(c => c.ID == customerID);

            double totalPrice = order.Quantity * order.Product.Price;
            double newBalance = customer.Balance - totalPrice;

            if (order == null || order.Product == null || customer == null || totalPrice == 0 || newBalance < 0)
                return;

            DbTransaction trans = null;
            try
            {
                trans = Database.BeginTransaction(settings);

                string sql = "INSERT INTO Sales VALUES(@Timestamp, @CustomerID, @RegisterID, @ProductID, @Amount, @TotalPrice)";

                DbParameter par0 = Database.AddParameter(settings, "@Timestamp", DateTime.Now);
                DbParameter par1 = Database.AddParameter(settings, "@CustomerID", customerID);
                DbParameter par2 = Database.AddParameter(settings, "@RegisterID", registerID); ;
                DbParameter par3 = Database.AddParameter(settings, "@ProductID", order.Product.ID);
                DbParameter par4 = Database.AddParameter(settings, "@Amount", order.Quantity);
                DbParameter par5 = Database.AddParameter(settings, "@TotalPrice", totalPrice);

                Database.InsertData(settings, sql, par0, par1, par2, par3, par4, par5);

                // adjust balance

                sql = "UPDATE Customers SET Balance=@NewBalance WHERE ID=@ID";

                DbParameter parId = Database.AddParameter(settings, "@ID", customer.ID);
                DbParameter parBalance = Database.AddParameter(settings, "@NewBalance", newBalance);

                Database.ModifyData(settings, sql, parId, parBalance);

                trans.Commit();

            }
            catch (Exception e)
            {
                trans.Rollback();
            }

        }
    }
}