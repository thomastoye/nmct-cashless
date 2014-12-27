﻿using nmct.ba.cashlessproject.model;
using nmct.ba.cashlessproject.model.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.api.Models
{
    public class RegisterManagement
    {
        private long _id;

        public long ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private Organisation _assignedTo;

        public Organisation AssignedTo
        {
            get { return _assignedTo; }
            set { _assignedTo = value; }
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

        private DateTime _purchaseDate;

        [Required(ErrorMessage = "Aankoopdatum is verplicht")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PurchaseDate
        {
            get { return _purchaseDate; }
            set { _purchaseDate = value; }
        }

        private DateTime _expiresDate;

        [Required(ErrorMessage = "Vervaldatum is verplicht")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
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