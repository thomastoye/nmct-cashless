using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace nmct.ba.cashlessproject.api.Helpers
{
    public class Claims
    {
        public static string GetConnectionString(System.Security.Principal.IPrincipal user)
        {
            if (user.Identity is ClaimsIdentity && user.IsInRole("OrganisationManager"))
            {
                var identity = (ClaimsIdentity)user.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                foreach (var claim in claims)
                {
                    if (claim.Type == "connectionString") return claim.Value;
                }
                return null;
            }
            else return null;
        }
    }
}