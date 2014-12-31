using Microsoft.Owin.Security.OAuth;
using nmct.ba.cashlessproject.api.Models;
using nmct.ba.cashlessproject.api.Models.DataAccess;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace nmct.ba.cashlessproject.api.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            Organisation org = Organisations.TryLogin(context.UserName, context.Password);
            RegisterManagement reg = null;
            int tryId;
            
            if(Int32.TryParse(context.UserName, out tryId)
                && RegistersManagement.GetRegisters().Exists(r => r.ID == Int32.Parse(context.UserName) && r.AssignedTo != null))
            {
                reg = RegistersManagement.GetRegisters().FirstOrDefault(r => r.ID == Int32.Parse(context.UserName));
            }

            // try to log in
            if (org != null)
            {
                var id = new ClaimsIdentity(context.Options.AuthenticationType);
                id.AddClaim(new Claim("username", context.UserName));
                id.AddClaim(new Claim("connectionString", org.DatabaseConnectionString));
                id.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                id.AddClaim(new Claim(ClaimTypes.Role, "OrganisationManager"));

                context.Validated(id);
            } else if(reg != null && reg.RemotePassword == context.Password) {
                var id = new ClaimsIdentity(context.Options.AuthenticationType);
                id.AddClaim(new Claim("username", context.UserName));
                id.AddClaim(new Claim("connectionString", reg.AssignedTo.DatabaseConnectionString));
                id.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                id.AddClaim(new Claim(ClaimTypes.Role, "Register"));

                context.Validated(id);
            } else {
                context.Rejected();
            }

            return Task.FromResult(0);
        }
    }

}