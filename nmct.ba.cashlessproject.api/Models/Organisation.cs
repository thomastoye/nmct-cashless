using nmct.ba.cashlessproject.api.Models.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace nmct.ba.cashlessproject.api.Models
{
    public class Organisation
    {
        public int ID { get; set; }
        [DisplayName("Loginnaam")]
        [Required]
        public string Login { get; set; }
        [DisplayName("Login paswoord")]
        [Required]
        public string Password { get; set; }
        [DisplayName("Database")]
        public string DbName { get { return "klant_" + OrganisationName.Replace(" ", ""); } }
        [DisplayName("Databaselogin")]
        [Required]
        [UniqueLoginName]
        public string DbLogin { get; set; }
        [DisplayName("Databasepaswoord")]
        [Required]
        public string DbPassword { get; set; }
        [DisplayName("Naam")]
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "De naam moet tussen de 3 en 25 karakters bevatten ")]
        public string OrganisationName { get; set; }
        [DisplayName("Adres")]
        [Required]
        public string Address { get; set; }
        [DisplayName("Emailadres")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [DisplayName("Telefoonnummer")]
        [Required]
        public string Phone { get; set; }

        public string DatabaseConnectionString {
            get{
                return "Data Source=THOMAS-ZBOOK\\SQLEXPRESS;Initial Catalog=" + DbName + ";Integrated Security=True;User Id=" + DbLogin + ";Password=" + DbPassword;
            }
        }
    }
}
