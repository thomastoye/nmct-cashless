using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model.Validation
{
    public class GeenSpecialeTekensAttribute : RegularExpressionAttribute
    {
        public GeenSpecialeTekensAttribute() : base(@"^[a-zA-Z''-'\s]*$") {
            ErrorMessage = "Er zijn geen speciale tekens toegelaten";
        }
    }
}
