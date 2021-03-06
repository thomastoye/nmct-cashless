﻿using nmct.ba.cashlessproject.model.Validation;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace nmct.ba.cashlessproject.model
{
    public class Employee : IDataErrorInfo
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
        [GeenSpecialeTekensWelCijfers]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Het adres moet tussen de 3 en 50 karakters bevatten ")]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private string _email;

        [Required(ErrorMessage = "Emailadres is verplicht")]
        [EmailAddress(ErrorMessage = "Emailadres is niet in de juiste vorm")]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _phone;

        [Required(ErrorMessage = "Telefoonnummer is verplicht")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "Telefoonnummer moet tussen de 10 en 20 karakters bevatten")]
        [RegularExpression(@"^[0-9''-'\s\(\)\-\+]*$", ErrorMessage = "Geen geldig telefoonnummer")]
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
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
