using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using RoadTex;
using RoadTex.Models;

namespace RoadTex.Controllers
{
    public class RolesController : Controller
    {
        private ExtendedUserDbContext db = new ExtendedUserDbContext();
        //RoleManager<ExtendedRole> manager => HttpContext.GetOwinContext().Get<RoleManager<ExtendedRole>>();
        UserManager<ExtendedUser> manager => HttpContext.GetOwinContext().Get<UserManager<ExtendedUser>>();
       
        // GET: Roles
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: Roles/Details/5
        public ActionResult Details(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole roles = db.Roles.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return View(roles);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] IdentityRole roles)
        {
            if (ModelState.IsValid)
            {
                if (!this.db.Roles.ToList().Contains(roles))
                {
                    this.db.Roles.Add(roles);
                    this.db.SaveChanges();

                    List<Modules> modules = this.db.Modules.ToList();
                    foreach (Modules mod in modules)
                    {
                        RolesModules roleModule = new RolesModules();
                        roleModule.Modules = mod;
                        roleModule.Roles = roles;
                        roleModule.IsAccess = false;
                        this.db.RolesModules.Add(roleModule);
                    }
                    try
                    {
                        this.db.SaveChanges();
                    }
                    catch (Exception e)
                    {

                    }

                    return RedirectToAction("Index");
                }

            }
            return View(roles);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole roles = db.Roles.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return View(roles);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] IdentityRole roles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roles);
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole roles= roleManager.Roles.Where(x => x.Id == id).FirstOrDefault();
           
            if (roles == null)
            {
                return HttpNotFound();
            }
            return View(roles);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {

            IdentityRole roles = db.Roles.Find(id);
            db.Roles.Remove(roles);
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
