using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nmct.ba.cashlessproject.api.Models.DataAccess;
using nmct.ba.cashlessproject.api.Helpers;
using nmct.ba.cashlessproject.model;
using System.Collections.Generic;

namespace CashlessTests
{
    [TestClass]
    public class CustomersDATest
    {
        [TestMethod]
        public void TestConnection()
        {
            Assert.IsTrue(Database.NumConnectionStrings() > 1);
        }

        [TestMethod]
        public void GetCustomers()
        {
            List<Customer> list = Customers.GetCustomers();
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void AddCustomer()
        {
            Customer cust = new Customer { Address = "Straatlaan 55", Balance = 50, Name = "Thomas" };

            Customers.InsertCustomer(cust);

            List<Customer> lijst = Customers.GetCustomers();

            bool inDB = Array.Exists<Customer>(Customers.GetCustomers().ToArray(), c => { return c.Equals(cust); });
            Assert.IsTrue(inDB);
        }
    }
}
