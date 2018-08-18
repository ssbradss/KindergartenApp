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
    public class Class_TripController : Controller
    {
        private Kindergarten_DBEntities db = new Kindergarten_DBEntities();

        // GET: Class_Trip
        public ActionResult Index()
        {
            var class_Trip = db.Class_Trip.Include(c => c.Class).Include(c => c.Trip);
            return View(class_Trip.ToList());
        }

        // GET: Class_Trip/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class_Trip class_Trip = db.Class_Trip.Find(id);
            if (class_Trip == null)
            {
                return HttpNotFound();
            }
            return View(class_Trip);
        }

        // GET: Class_Trip/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "ClassTitle");
            ViewBag.TripId = new SelectList(db.Trips, "Id", "TripTitle");
            return View();
        }

        // POST: Class_Trip/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClassId,TripId")] Class_Trip class_Trip)
        {
            if (ModelState.IsValid)
            {
                db.Class_Trip.Add(class_Trip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.Classes, "Id", "ClassTitle", class_Trip.ClassId);
            ViewBag.TripId = new SelectList(db.Trips, "Id", "TripTitle", class_Trip.TripId);
            return View(class_Trip);
        }

        // GET: Class_Trip/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class_Trip class_Trip = db.Class_Trip.Find(id);
            if (class_Trip == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "ClassTitle", class_Trip.ClassId);
            ViewBag.TripId = new SelectList(db.Trips, "Id", "TripTitle", class_Trip.TripId);
            return View(class_Trip);
        }

        // POST: Class_Trip/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClassId,TripId")] Class_Trip class_Trip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(class_Trip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "ClassTitle", class_Trip.ClassId);
            ViewBag.TripId = new SelectList(db.Trips, "Id", "TripTitle", class_Trip.TripId);
            return View(class_Trip);
        }

        // GET: Class_Trip/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class_Trip class_Trip = db.Class_Trip.Find(id);
            if (class_Trip == null)
            {
                return HttpNotFound();
            }
            return View(class_Trip);
        }

        // POST: Class_Trip/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Class_Trip class_Trip = db.Class_Trip.Find(id);
            db.Class_Trip.Remove(class_Trip);
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
