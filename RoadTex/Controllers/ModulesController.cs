using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using RoadTex;
using RoadTex.Models;

namespace RoadTex.Controllers
{
    public class ModulesController : Controller
    {
        ExtendedUserDbContext context => HttpContext.GetOwinContext().Get<ExtendedUserDbContext>();

        // GET: Modules
        public ActionResult Index()
        {
            return View(context.Modules.ToList());
        }

        // GET: Modules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modules modules = context.Modules.Find(id);
            if (modules == null)
            {
                return HttpNotFound();
            }
            return View(modules);
        }

        // GET: Modules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Modules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Modules modules)
        {
            if (ModelState.IsValid)
            {


                if (!this.context.Modules.ToList().Contains(modules))
                {
                    this.context.Modules.Add(modules);

                    this.context.SaveChanges();

                    List<IdentityRole> roles = this.context.Roles.ToList();
                    List<RolesModules> rolesModules = new List<RolesModules>();
                    foreach (var role in roles)
                    {
                        RolesModules roleModule = new RolesModules();
                        roleModule.Modules = modules;
                        roleModule.Roles =role;
                        roleModule.IsAccess = false;
                        this.context.RolesModules.Add(roleModule);

                        
                    }
                    try
                    {
                        this.context.SaveChanges();
                    }
                    catch (Exception e)
                    {

                    }
                }
                      
                return RedirectToAction("Index");
            }

            return View(modules);
        }

        // GET: Modules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modules modules = context.Modules.Find(id);
            if (modules == null)
            {
                return HttpNotFound();
            }
            return View(modules);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Modules modules)
        {
            if (ModelState.IsValid)
            {
                context.Entry(modules).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modules);
        }

        // GET: Modules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modules modules = context.Modules.Find(id);
            if (modules == null)
            {
                return HttpNotFound();
            }
            return View(modules);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Modules modules = context.Modules.Find(id);
            context.Modules.Remove(modules);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
