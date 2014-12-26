using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model.Validation
{
    public class GeenSpecialeTekensWelCijfersAttribute : RegularExpressionAttribute
    {
        public GeenSpecialeTekensWelCijfersAttribute() : base(@"^[a-zA-Z0-9''-'\s]*$")
        {
            ErrorMessage = "Er zijn geen speciale tekens toegelaten";
        }
    }
}
