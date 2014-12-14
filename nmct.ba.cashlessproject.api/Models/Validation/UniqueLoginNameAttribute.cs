using nmct.ba.cashlessproject.api.Models.DataAccess;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Models.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class UniqueLoginNameAttribute : ValidationAttribute
    {
        public UniqueLoginNameAttribute() { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is string))
                return new ValidationResult("The database username was not a string.");

            //Organisation org = validationContext.ObjectInstance as Organisation;
            string dbLogin = value as string;
            bool valid = Organisations.GetByUser(dbLogin) == null;

            if (valid)
            {
                return null;
            }
            else
            {
                return new ValidationResult(this.FormatErrorMessage("This database username is already taken. Please choose another one."));
            }
        }
    }
}