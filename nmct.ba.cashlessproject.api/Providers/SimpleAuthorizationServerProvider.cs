﻿using Microsoft.Owin.Security.OAuth;
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

            // try to log in
            if (org == null)
            {
                context.Rejected();
                return Task.FromResult(0);
            }

            var id = new ClaimsIdentity(context.Options.AuthenticationType);
            id.AddClaim(new Claim("username", context.UserName));
            id.AddClaim(new Claim("connectionString", org.DatabaseConnectionString));
            id.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            id.AddClaim(new Claim(ClaimTypes.Role, "OrganisationManager"));

            context.Validated(id);
            return Task.FromResult(0);
        }
    }

}