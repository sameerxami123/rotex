using AspNetWebApplication.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using RoadTex;
using RoadTex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RoadTex.Controllers
{
    public class AccountController : Controller
    {
        UserManager<ExtendedUser> manager => HttpContext.GetOwinContext().Get<UserManager<ExtendedUser>>();
        SignInManager<ExtendedUser,string> signInManager => HttpContext.GetOwinContext().Get<SignInManager<ExtendedUser, string>>();
        ExtendedUserDbContext context=>HttpContext.GetOwinContext().Get<ExtendedUserDbContext>();
        public AccountController()
        {
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {

          var signInStatus= await  signInManager.PasswordSignInAsync(model.Username, model.Password, true, true);
            switch (signInStatus)
            {
                case SignInStatus.Success:
                    {
                        return RedirectToAction("Index", "Customer");
                    }
                default:
                    ModelState.AddModelError("", "Invalid Credential");
                    return View(model);
            }
        }
        
        public ActionResult Register()
        {
            ViewBag.Name = new SelectList(context.Roles.Where(u => !u.Name.Contains("Admin"))
                                           .ToList(), "Name", "Name");
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {

            var user = new ExtendedUser
            {
                UserName = model.Username,
                FirstName = model.FirstName,
                Email=model.Email,
                LastName=model.LastName,
                IsSalesRep=model.IsSalesRep,
                IsPreprer=model.IsPreprer
            };
          
           var IdentityResult= await manager.CreateAsync(user, model.PasswordHash);
            
            if (IdentityResult.Succeeded)
            {
            
                manager.AddToRole(user.Id, model.Role);
                return RedirectToAction("Index", "Customer");
            }
            else
            {
                ModelState.AddModelError("", IdentityResult.Errors.FirstOrDefault());
                return View(model);
            }
        }

       
    }
}