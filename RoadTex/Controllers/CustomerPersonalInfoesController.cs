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
    public class CustomerPersonalInfoesController : Controller
    {
        private ExtendedUserDbContext db = new ExtendedUserDbContext();

        // GET: CustomerPersonalInfoes
        public ActionResult Index()
        {
            var customerPersonalInfoes = db.CustomerPersonalInfoes.Include(c => c.Customer);
            return View(customerPersonalInfoes.ToList());
        }

        // GET: CustomerPersonalInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerPersonalInfo customerPersonalInfo = db.CustomerPersonalInfoes.Find(id);
            if (customerPersonalInfo == null)
            {
                return HttpNotFound();
            }
            return View(customerPersonalInfo);
        }

        // GET: CustomerPersonalInfoes/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "CustomerName");
            return View();
        }

        // POST: CustomerPersonalInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HomePhone,CellPhone,Email,CustomerId")] CustomerPersonalInfo customerPersonalInfo)
        {
            if (ModelState.IsValid)
            {
                db.CustomerPersonalInfoes.Add(customerPersonalInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "CustomerName", customerPersonalInfo.CustomerId);
            return View(customerPersonalInfo);
        }

        // GET: CustomerPersonalInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerPersonalInfo customerPersonalInfo = db.CustomerPersonalInfoes.Find(id);
            if (customerPersonalInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "CustomerName", customerPersonalInfo.CustomerId);
            return View(customerPersonalInfo);
        }

        // POST: CustomerPersonalInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HomePhone,CellPhone,Email,CustomerId")] CustomerPersonalInfo customerPersonalInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerPersonalInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "CustomerName", customerPersonalInfo.CustomerId);
            return View(customerPersonalInfo);
        }

        // GET: CustomerPersonalInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerPersonalInfo customerPersonalInfo = db.CustomerPersonalInfoes.Find(id);
            if (customerPersonalInfo == null)
            {
                return HttpNotFound();
            }
            return View(customerPersonalInfo);
        }

        // POST: CustomerPersonalInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerPersonalInfo customerPersonalInfo = db.CustomerPersonalInfoes.Find(id);
            db.CustomerPersonalInfoes.Remove(customerPersonalInfo);
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
