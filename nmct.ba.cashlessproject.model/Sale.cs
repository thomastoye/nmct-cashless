using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model
{
    class Sale
    {
        private long _id;

        public long ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private DateTime _timestamp;

        public DateTime TimeStamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        private Customer _customer;

        public Customer Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }

        private Product _product;

        public Product Product
        {
            get { return _product; }
            set { _product = value; }
        }

        private int _amount;

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public double TotalAmount { get { return Amount * Product.Price } }
    }
}
