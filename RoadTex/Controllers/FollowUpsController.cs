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
    public class FollowUpsController : Controller
    {
        private ExtendedUserDbContext db = new ExtendedUserDbContext();
        UserManager<ExtendedUser> manager => HttpContext.GetOwinContext().Get<UserManager<ExtendedUser>>();

        // GET: FollowUps
        public ActionResult Index()
        {
            var followup = db.FollowUps.Include("User").Include("Customer").ToList();
            return View(followup);
        }

        // GET: FollowUps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FollowUp followUp = db.FollowUps.Find(id);
            if (followUp == null)
            {
                return HttpNotFound();
            }
            return View(followUp);
        }

        // GET: FollowUps/Create
        public ActionResult Create()
        {
            ViewBag.Users = db.Users.ToList();
            ViewBag.Customers = db.Customers.ToList();

            return View();
        }

        // POST: FollowUps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FollowUp followUp)
        {
            if (ModelState.IsValid)
            {
                
               
                db.FollowUps.Add(followUp);
                try
                {
                    db.SaveChanges();
                }catch(Exception e)
                {

                }
                return RedirectToAction("Index");
            }
            ViewBag.Users = db.Users.ToList();
            ViewBag.Customers = db.Customers.ToList();
            return View(followUp);
        }

        // GET: FollowUps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FollowUp followUp = db.FollowUps.Find(id);
            if (followUp == null)
            {
                return HttpNotFound();
            }
            return View(followUp);
        }

        // POST: FollowUps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Topic,ResultDesc,User_Id,Customer_Id")] FollowUp followUp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(followUp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(followUp);
        }

        // GET: FollowUps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FollowUp followUp = db.FollowUps.Find(id);
            if (followUp == null)
            {
                return HttpNotFound();
            }
            return View(followUp);
        }

        // POST: FollowUps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FollowUp followUp = db.FollowUps.Find(id);
            db.FollowUps.Remove(followUp);
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
