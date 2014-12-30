namespace nmct.ba.cashlessproject.api.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using nmct.ba.cashlessproject.api.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<nmct.ba.cashlessproject.api.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(nmct.ba.cashlessproject.api.Models.ApplicationDbContext context)
        {
            string roleAdmin = "Administrator";
            string roleNormalUser = "User";
            string roleOrgManager = "OrganisationManager";
            string roleRegister = "Register";

            IdentityResult roleResult;
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!RoleManager.RoleExists(roleNormalUser))
                roleResult = RoleManager.Create(new IdentityRole(roleNormalUser));
            if (!RoleManager.RoleExists(roleAdmin))
                roleResult = RoleManager.Create(new IdentityRole(roleAdmin));
            if (!RoleManager.RoleExists(roleOrgManager))
                roleResult = RoleManager.Create(new IdentityRole(roleOrgManager));
            if (!RoleManager.RoleExists(roleRegister))
                roleResult = RoleManager.Create(new IdentityRole(roleRegister));

            if (!context.Users.Any(u => u.Email.Equals("root@dev.null")))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser() { Email = "root@dev.null", UserName = "root@dev.null" };
                manager.Create(user, "Password");
                manager.AddToRole(user.Id, roleAdmin);
            }
        }
    }
}
