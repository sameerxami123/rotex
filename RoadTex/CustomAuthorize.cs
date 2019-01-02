using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using RoadTex.Models;

namespace RoadTex
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class CustomAuthorize:AuthorizeAttribute
    {
        ExtendedUserDbContext context;

        private string claims { get; set; }
        public CustomAuthorize(string claim)
        {

            this.claims = claim;
            this.context = new ExtendedUserDbContext();
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool bol=base.AuthorizeCore(httpContext);
            if (!bol)
            {
                return false;
            }

            var user = this.context.Users.FirstOrDefault(x => x.UserName == httpContext.User.Identity.Name);
            IdentityUserRole userRoles = user.Roles.FirstOrDefault();
            List<RolesModules> roleModules = this.context.RolesModules.Include("Roles").
                Include("Modules").
                Where(x => ( x.Roles.Id == userRoles.RoleId&&x.IsAccess==true)).ToList();
            List<String> str = new List<string>();
            foreach (var rolmod in roleModules)
            {
                var module = this.context.Modules.FirstOrDefault(x => x.Id == rolmod.Modules.Id);
                str.Add(module.Name);
            }
            bool booool = str.Contains(this.claims);
            return booool;
            
           
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (AuthorizeCore(filterContext.HttpContext))
            {

            }
            else
            {
                HandleUnauthorizedRequest(filterContext);
            }


            //  filterContext.Controller.ViewBag.AutherizationMessage = "Custom Authorization: Message from OnAuthorization method.";
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/unauthorized.cshtml"
            };
        }

    }
}