using nmct.ba.cashlessproject.model.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model
{
    public class RegisterOrganisation : IDataErrorInfo
    {
        private long _id;

        public long ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;

        [Required(ErrorMessage = "Naam is verplicht")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Naam moet tussen de 5 en 30 karakters lang zijn")]
        [GeenSpecialeTekensWelCijfers]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _device;

        [Required(ErrorMessage = "Toestel is verplicht")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Toestel moet tussen de 5 en 30 karakters lang zijn")]
        [GeenSpecialeTekensWelCijfers]
        public string Device
        {
            get { return _device; }
            set { _device = value; }
        }

        private List<ErrorLog> _errors;

        public List<ErrorLog> Errors
        {
            get { return _errors; }
            set { _errors = value; }
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
