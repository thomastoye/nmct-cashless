using nmct.ba.cashlessproject.model.Validation;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Windows.Media.Imaging;


namespace nmct.ba.cashlessproject.model
{
    public class Customer : IDataErrorInfo
    {
        private long _id;

        public long ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;

        [Required(ErrorMessage = "De naam is verplicht")]
        [GeenSpecialeTekens]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "De naam moet tussen de 3 en 50 karakters bevatten ")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _address;

        [Required(ErrorMessage = "Het adres is verplicht")]
        [StringLength(50, MinimumLength = 3,ErrorMessage = "Het adres moet tussen de 3 en 50 karakters bevatten.")]
        [GeenSpecialeTekensWelCijfers]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private byte[] _image;

        public byte[] Image
        {
            get { return _image; }
            set { _image = value; }
        }

        private double _balance;

        [Required(ErrorMessage = "De balans is verplicht")]
        [Range(0,100, ErrorMessage = "De balans moet tussen 0 en 100 liggen")]
        public double Balance
        {
            get { return _balance; }
            set { _balance = value; }
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
