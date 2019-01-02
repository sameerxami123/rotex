using AspNetWebApplication.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using RoadTex.ViewInputModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RoadTex.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        ExtendedUserDbContext db => HttpContext.GetOwinContext().Get<ExtendedUserDbContext>();
        UserManager<ExtendedUser> manager => HttpContext.GetOwinContext().Get<UserManager<ExtendedUser>>();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Roles).ToList();
            List<RegisterModel> elist = new List<RegisterModel>();
            foreach (ExtendedUser user in users)
            {
                string RoleId = user.Roles.FirstOrDefault().RoleId;
                string RoleName = this.db.Roles.FirstOrDefault(x => x.Id == RoleId).Name;
                RegisterModel ex = new RegisterModel();
                ex.Id = user.Id;
                ex.Username = user.UserName;
                ex.PasswordHash = user.PasswordHash;
                ex.Email = user.Email;
                ex.FirstName = user.FirstName;
                ex.LastName = user.LastName;
                ex.Role = RoleName;
                ex.IsSalesRep = user.IsSalesRep;
                ex.IsPreprer = user.IsPreprer;

                elist.Add(ex);
            }
            return View(elist);
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtendedUser user = manager.Users.Where(x=>x.Id==id).FirstOrDefault();
            RegisterModel user2 = new RegisterModel
            {
                Id=user.Id,
                Username = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                IsSalesRep = user.IsSalesRep,
                IsPreprer = user.IsPreprer
            };
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user2);
        }

        public ActionResult Create()
        {
            ViewBag.Roles = db.Roles.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExtendedUser user)
        {
            if (ModelState.IsValid)
            {
                ExtendedUser user2 = new ExtendedUser
                {
                    Id =user.Id,
                    UserName=user.UserName,
                   
                    Email=user.Email,
                    FirstName=user.FirstName,

                    LastName=user.LastName,
                    Role=user.Role,
                    IsSalesRep=user.IsSalesRep,
                    IsPreprer=user.IsPreprer

                };
                string pass = user.PasswordHash;
                var chkUser = manager.Create(user2, pass);
                if (chkUser.Succeeded)
                {
                    string RoleName = this.db.Roles.FirstOrDefault(x => x.Id == user.Role).Name;
                    var result1 = manager.AddToRole(user.Id,RoleName);
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                }
                return RedirectToAction("Index");
            }

            ViewBag.UserTypeId = new SelectList(db.Roles, "Id", "Type", 1);
            return View(user);
        }

        public ActionResult Edit(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtendedUser user = manager.Users.Where(x => x.Id == id).FirstOrDefault();
            ExtendedUser user2 = new ExtendedUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                IsSalesRep = user.IsSalesRep,
                IsPreprer = user.IsPreprer
            };
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Roles = db.Roles.ToList();
            ViewBag.SelectedRole = db.Roles.Where(x => x.Id == user2.Role).FirstOrDefault().Name;

            return View(user2);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ExtendedUser user)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user2 = new ExtendedUser
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role,
                    IsSalesRep = user.IsSalesRep,
                    IsPreprer = user.IsPreprer

                };
                db.Entry(user2).State = EntityState.Modified;
                try { 
                  db.SaveChanges();
                  
                }
                catch(Exception e)
                {
                }
                return RedirectToAction("Index");
            }
            ViewBag.UserTypeId = new SelectList(db.Roles, "Id", "Type", 1);
            return View(user);
        }
        public ActionResult Delete(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            IdentityUser user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}