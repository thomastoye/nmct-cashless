using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Models
{
    public class Organisation_RegisterBindModel : IDataErrorInfo
    {
        private int _orgId;

        [Required]
        public int OrganisationID
        {
            get { return _orgId; }
            set { _orgId = value; }
        }

        [Required]
        private int _regId;

        public int RegisterID
        {
            get { return _regId; }
            set { _regId = value; }
        }

        private DateTime _fromDate;

        [Required]
        public DateTime FromDate
        {
            get { return _fromDate; }
            set { _fromDate = value; }
        }

        private DateTime _untilDate;

        [Required]
        public DateTime UntilDate
        {
            get { return _untilDate; }
            set { _untilDate = value; }
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