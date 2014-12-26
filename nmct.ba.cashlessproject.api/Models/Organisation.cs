using nmct.ba.cashlessproject.api.Models.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace nmct.ba.cashlessproject.api.Models
{
    public class Organisation
    {
        public int ID { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string DbName { get; set; }
        [Required]
        [UniqueLoginName]
        public string DbLogin { get; set; }
        [Required]
        public string DbPassword { get; set; }
        [Required]
        public string OrganisationName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }

        public string DatabaseName
        {
            get { return "org_" + OrganisationName; }
        }
        public string DatabaseConnectionString {
            get{
                return "Data Source=THOMAS-ZBOOK\\SQLEXPRESS;Initial Catalog=" + DatabaseName + ";Integrated Security=True;";
            }
        }
    }
}
