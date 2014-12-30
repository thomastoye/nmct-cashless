using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace nmct.ba.cashlessproject.api.Helpers
{
    public class Claims
    {
        public static ConnectionStringSettings GetConnectionString(System.Security.Principal.IPrincipal user)
        {
            if (user.Identity is ClaimsIdentity && (user.IsInRole("OrganisationManager") || user.IsInRole("Register")))
            {
                var identity = (ClaimsIdentity)user.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                foreach (var claim in claims)
                {
                    if (claim.Type == "connectionString") return new ConnectionStringSettings("KlantDynamicConnection", claim.Value, "System.Data.SqlClient");
                }
                return null;
            }
            else return null;
        }
    }
}