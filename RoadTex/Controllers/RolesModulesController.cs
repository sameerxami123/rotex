using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RoadTex;
using RoadTex.Models;

namespace RoadTex.Controllers
{
    public class RolesModulesController : Controller
    {
        private ExtendedUserDbContext db = new ExtendedUserDbContext();

        // GET: RolesModules
        public ActionResult Index()
        {
            return View(db.RolesModules.Include("Roles").Include("Modules").ToList());
        }

        // GET: RolesModules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolesModules rolesModules = db.RolesModules.Find(id);
            if (rolesModules == null)
            {
                return HttpNotFound();
            }
            return View(rolesModules);
        }

        // GET: RolesModules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolesModules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IsAccess")] RolesModules rolesModules)
        {
            if (ModelState.IsValid)
            {
                db.RolesModules.Add(rolesModules);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rolesModules);
        }

        // GET: RolesModules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RolesModules rolesModules = db.RolesModules.Include("Roles").Include("Modules").Where(x=>x.Id==id).FirstOrDefault();
            if (rolesModules == null)
            {
                return HttpNotFound();
            }
            return View(rolesModules);
        }

        // POST: RolesModules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IsAccess")] RolesModules rolesModules)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rolesModules).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rolesModules);
        }

        // GET: RolesModules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolesModules rolesModules = db.RolesModules.Find(id);
            if (rolesModules == null)
            {
                return HttpNotFound();
            }
            return View(rolesModules);
        }

        // POST: RolesModules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RolesModules rolesModules = db.RolesModules.Find(id);
            db.RolesModules.Remove(rolesModules);
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
