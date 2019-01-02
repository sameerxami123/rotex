using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using System.Collections.Generic;
using System.Security.Claims;
using RoadTex.Models;

[assembly: OwinStartup(typeof(RoadTex.Startup))]

namespace RoadTex
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new ExtendedUserDbContext());

            app.CreatePerOwinContext<UserStore<ExtendedUser>>((opt, cont) => new UserStore<ExtendedUser>(cont.Get<ExtendedUserDbContext>()));
            app.CreatePerOwinContext<UserManager<ExtendedUser>>((opt, cont) => new UserManager<ExtendedUser>(cont.Get<UserStore<ExtendedUser>>()));

            app.CreatePerOwinContext<RoleStore<ExtendedRole>>((opt, cont) => new RoleStore<ExtendedRole>(cont.Get<ExtendedUserDbContext>()));
            app.CreatePerOwinContext<RoleManager<ExtendedRole>>((opt, cont) => new RoleManager<ExtendedRole>(cont.Get<RoleStore<ExtendedRole>>()));

            app.CreatePerOwinContext<SignInManager<ExtendedUser, string>>((opt, cont) => new SignInManager<ExtendedUser, string>(cont.Get<UserManager<ExtendedUser>>(), cont.Authentication));
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });
            CreateLocalRoles();
        }

        private void CreateLocalRoles()
        {
            ExtendedUserDbContext context = new ExtendedUserDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ExtendedUser>(new UserStore<ExtendedUser>(context));

            List<string> roles = new List<string>()
            {
                 "admin","manager","employee"
            };

            string adminuserId="";
            foreach (var rolename in roles)
            {
                if (roleManager.RoleExists(rolename)) continue;
               
                var role = new IdentityRole { Name = rolename };

                var result=roleManager.Create(role);
                if (rolename == "admin")
                {
                    adminuserId = role.Id;
                }

            }

            var rootUser = userManager.FindByEmail("root@root.com");

            if (rootUser == null)
            {
                var user = new ExtendedUser
                {
                    UserName = "root@root.com",
                    FirstName = "Sameer",
                    LastName="Tariq",
                    Email = "root@root.com",
                    Role=adminuserId
                };
                string pass = "123467";
                var chkUser = userManager.Create(user, pass);
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "admin");

                }
            }
        }
    }
}
