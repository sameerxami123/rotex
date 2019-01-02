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
    public class CallHistoriesController : Controller
    {
        private ExtendedUserDbContext db = new ExtendedUserDbContext();

        // GET: CallHistories
        public ActionResult Index()
        {
            return View(db.CallHistories.ToList());
        }

        // GET: CallHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CallHistory callHistory = db.CallHistories.Find(id);
            if (callHistory == null)
            {
                return HttpNotFound();
            }
            return View(callHistory);
        }

        // GET: CallHistories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CallHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Duration,CallerNo,RecipientNo,UserId")] CallHistory callHistory)
        {
            if (ModelState.IsValid)
            {
                db.CallHistories.Add(callHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(callHistory);
        }

        // GET: CallHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CallHistory callHistory = db.CallHistories.Find(id);
            if (callHistory == null)
            {
                return HttpNotFound();
            }
            return View(callHistory);
        }

        // POST: CallHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Duration,CallerNo,RecipientNo,UserId")] CallHistory callHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(callHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(callHistory);
        }

        // GET: CallHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CallHistory callHistory = db.CallHistories.Find(id);
            if (callHistory == null)
            {
                return HttpNotFound();
            }
            return View(callHistory);
        }

        // POST: CallHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CallHistory callHistory = db.CallHistories.Find(id);
            db.CallHistories.Remove(callHistory);
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
