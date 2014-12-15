using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model
{
    public class ErrorLog : IDataErrorInfo
    {
        private long _registerId;

        public long RegisterID
        {
            get { return _registerId; }
            set { _registerId = value; }
        }

        private DateTime _timestamp;

        [Required(ErrorMessage  = "Timestamp is werplicht")]
        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        private string _message;

        [Required(ErrorMessage = "Message is verplicht")]
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private string _stacktrace;

        [Required( ErrorMessage = "Stacktrace is verplicht")]
        public string StackTrace
        {
            get { return _stacktrace; }
            set { _stacktrace = value; }
        }

        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
        }

        public string this[string columnName]
        {
            get
            {
                try
                {
                    object value = this.GetType().GetProperty(columnName).GetValue(this);
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = columnName });
                }
                catch (ValidationException ex)
                {
                    return ex.Message;
                }
                return String.Empty;
            }
        }

        public string Error
        {
            get { return null; }
        }

    }
}
