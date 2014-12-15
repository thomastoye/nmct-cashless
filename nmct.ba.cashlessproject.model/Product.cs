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
    public class Product : IDataErrorInfo
    {
        private long _id;

        public long ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;

        [Required(ErrorMessage = "Naam is verplicht")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Naam moeten tussen de 3 en 50 karakters lang zijn")]
        [GeenSpecialeTekensWelCijfers]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private double _price;

        [Required(ErrorMessage = "Prijs is verplicht")]
        [Range(0,100, ErrorMessage = "Prijs moet tussen 0 en honderd zijn")]
        public double Price
        {
            get { return _price; }
            set { _price = value; }
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
