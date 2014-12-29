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
        public string DbName { get { return "klant_" + OrganisationName.Replace(" ", ""); } }
        [Required]
        [UniqueLoginName]
        public string DbLogin { get; set; }
        [Required]
        public string DbPassword { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "De naam moet tussen de 3 en 25 karakters bevatten ")]
        public string OrganisationName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }

        public string DatabaseConnectionString {
            get{
                return "Data Source=THOMAS-ZBOOK\\SQLEXPRESS;Initial Catalog=" + DbName + ";Integrated Security=True;User Id=" + DbLogin + ";Password=" + DbPassword;
            }
        }
    }
}
