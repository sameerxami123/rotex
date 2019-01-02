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
    public class FutureAppointmentsController : Controller
    {
        private ExtendedUserDbContext db = new ExtendedUserDbContext();

        // GET: FutureAppointments
        public ActionResult Index()
        {
            return View(db.FutureAppointments.ToList());
        }

        // GET: FutureAppointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FutureAppointment futureAppointment = db.FutureAppointments.Find(id);
            if (futureAppointment == null)
            {
                return HttpNotFound();
            }
            return View(futureAppointment);
        }

        // GET: FutureAppointments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FutureAppointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type,AppointmentWith,AppointmentDate,TimeStart,TimeEnd,Duration")] FutureAppointment futureAppointment)
        {
            if (ModelState.IsValid)
            {
                db.FutureAppointments.Add(futureAppointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(futureAppointment);
        }

        // GET: FutureAppointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FutureAppointment futureAppointment = db.FutureAppointments.Find(id);
            if (futureAppointment == null)
            {
                return HttpNotFound();
            }
            return View(futureAppointment);
        }

        // POST: FutureAppointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type,AppointmentWith,AppointmentDate,TimeStart,TimeEnd,Duration")] FutureAppointment futureAppointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(futureAppointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(futureAppointment);
        }

        // GET: FutureAppointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FutureAppointment futureAppointment = db.FutureAppointments.Find(id);
            if (futureAppointment == null)
            {
                return HttpNotFound();
            }
            return View(futureAppointment);
        }

        // POST: FutureAppointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FutureAppointment futureAppointment = db.FutureAppointments.Find(id);
            db.FutureAppointments.Remove(futureAppointment);
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
