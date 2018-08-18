using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KindergartenApp.Models;

namespace KindergartenApp.Controllers
{
    public class TripDetailsController : Controller
    {
        private Kindergarten_DBEntities db = new Kindergarten_DBEntities();

        // GET: TripDetails
        public ActionResult Index()
        {
            var tripDetails = db.TripDetails.Include(t => t.Trip);
            return View(tripDetails.ToList());
        }

        // GET: TripDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TripDetail tripDetail = db.TripDetails.Find(id);
            if (tripDetail == null)
            {
                return HttpNotFound();
            }
            return View(tripDetail);
        }

        // GET: TripDetails/Create
        public ActionResult Create()
        {
            ViewBag.TripId = new SelectList(db.Trips, "Id", "TripTitle");
            return View();
        }

        // POST: TripDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Destination,TimeStart,TimeEnd,Activity,Food,Budget,TripId")] TripDetail tripDetail)
        {
            if (ModelState.IsValid)
            {
                db.TripDetails.Add(tripDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TripId = new SelectList(db.Trips, "Id", "TripTitle", tripDetail.TripId);
            return View(tripDetail);
        }

        // GET: TripDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TripDetail tripDetail = db.TripDetails.Find(id);
            if (tripDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.TripId = new SelectList(db.Trips, "Id", "TripTitle", tripDetail.TripId);
            return View(tripDetail);
        }

        // POST: TripDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Destination,TimeStart,TimeEnd,Activity,Food,Budget,TripId")] TripDetail tripDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tripDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TripId = new SelectList(db.Trips, "Id", "TripTitle", tripDetail.TripId);
            return View(tripDetail);
        }

        // GET: TripDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TripDetail tripDetail = db.TripDetails.Find(id);
            if (tripDetail == null)
            {
                return HttpNotFound();
            }
            return View(tripDetail);
        }

        // POST: TripDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TripDetail tripDetail = db.TripDetails.Find(id);
            db.TripDetails.Remove(tripDetail);
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
