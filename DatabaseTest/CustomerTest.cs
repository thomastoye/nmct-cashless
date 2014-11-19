using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nmct.ba.cashlessproject.model;

namespace CashlessTests
{
    [TestClass]
    public class Customertest
    {
        [TestMethod]
        public void Equality()
        {
            Customer c1 = new Customer { Address = "Straatlaan 55", Balance = 50, Name = "Thomas", ID = 5 };
            Customer c2 = new Customer { Address = "Straatlaan 55", Balance = 50, Name = "Thomas", ID = 6 };

            Assert.IsFalse(c1.Equals(c2));
            Assert.IsFalse(c2.Equals(c1));

            c2.ID = 5;

            Assert.IsTrue(c1.Equals(c2));
            Assert.IsTrue(c2.Equals(c1));
        }
    }
}
