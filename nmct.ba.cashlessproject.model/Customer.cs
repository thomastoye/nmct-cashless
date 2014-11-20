using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model
{
    public class Customer
    {
        private long _id;

        public long ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        /*private BitmapImage myVar;

        public BitmapImage MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }*/

        private double _balance;

        public double Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            Customer c = obj as Customer;
            if ((System.Object)c == null) return false;

            return /*c.ID.Equals(ID) &&*/ c.Address.Equals(Address) && c.Balance.Equals(Balance) && c.Name.Equals(Name);
        }
        
    }
}
