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
    public class OfferSalesController : Controller
    {
        private ExtendedUserDbContext db = new ExtendedUserDbContext();

        // GET: OfferSales
        public ActionResult Index()
        {
            var offerSales = db.OfferSales.Include(o => o.Customer).Include(o => o.Offer);
            return View(offerSales.ToList());
        }

        // GET: OfferSales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferSale offerSale = db.OfferSales.Find(id);
            if (offerSale == null)
            {
                return HttpNotFound();
            }
            return View(offerSale);
        }

        // GET: OfferSales/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "CustomerName");
            ViewBag.OfferId = new SelectList(db.Offers, "Id", "OfferTitle");
            return View();
        }

        // POST: OfferSales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OfferId,CustomerId,TotalAmount")] OfferSale offerSale)
        {
            if (ModelState.IsValid)
            {
                db.OfferSales.Add(offerSale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "CustomerName", offerSale.CustomerId);
            ViewBag.OfferId = new SelectList(db.Offers, "Id", "OfferTitle", offerSale.OfferId);
            return View(offerSale);
        }

        // GET: OfferSales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferSale offerSale = db.OfferSales.Find(id);
            if (offerSale == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "CustomerName", offerSale.CustomerId);
            ViewBag.OfferId = new SelectList(db.Offers, "Id", "OfferTitle", offerSale.OfferId);
            return View(offerSale);
        }

        // POST: OfferSales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OfferId,CustomerId,TotalAmount")] OfferSale offerSale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offerSale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "CustomerName", offerSale.CustomerId);
            ViewBag.OfferId = new SelectList(db.Offers, "Id", "OfferTitle", offerSale.OfferId);
            return View(offerSale);
        }

        // GET: OfferSales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferSale offerSale = db.OfferSales.Find(id);
            if (offerSale == null)
            {
                return HttpNotFound();
            }
            return View(offerSale);
        }

        // POST: OfferSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OfferSale offerSale = db.OfferSales.Find(id);
            db.OfferSales.Remove(offerSale);
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
