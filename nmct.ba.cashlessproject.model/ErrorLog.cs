using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model
{
    public class ErrorLog
    {
        private long _registerId;

        public long RegisterID
        {
            get { return _registerId; }
            set { _registerId = value; }
        }

        private DateTime _timestamp;

        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private string _stacktrace;

        public string StackTrace
        {
            get { return _stacktrace; }
            set { _stacktrace = value; }
        }

    }
}
