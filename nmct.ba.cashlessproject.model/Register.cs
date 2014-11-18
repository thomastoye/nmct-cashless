using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model
{
    public class Register
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

        private string _device;

        public string Device
        {
            get { return _device; }
            set { _device = value; }
        }

        private DateTime _purchaseDate;

        public DateTime PurchaseDate
        {
            get { return _purchaseDate; }
            set { _purchaseDate = value; }
        }

        private DateTime _expiresDate;

        public DateTime ExpiresDate
        {
            get { return _expiresDate; }
            set { _expiresDate = value; }
        }

        private List<ErrorLog> _errors;

        public List<ErrorLog> Errors
        {
            get { return _errors; }
            set { _errors = value; }
        }
        
        
    }
}
