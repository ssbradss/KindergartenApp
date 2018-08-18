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
    public class DietDetailsController : Controller
    {
        private Kindergarten_DBEntities db = new Kindergarten_DBEntities();

        // GET: DietDetails
        public ActionResult Index()
        {
            var dietDetails = db.DietDetails.Include(d => d.Diet);
            return View(dietDetails.ToList());
        }

        // GET: DietDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DietDetail dietDetail = db.DietDetails.Find(id);
            if (dietDetail == null)
            {
                return HttpNotFound();
            }
            return View(dietDetail);
        }

        // GET: DietDetails/Create
        public ActionResult Create()
        {
            ViewBag.DietId = new SelectList(db.Diets, "Id", "DietTitle");
            return View();
        }

        // POST: DietDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Meal,FoodMenu,Note,Date,DietId")] DietDetail dietDetail)
        {
            if (ModelState.IsValid)
            {
                db.DietDetails.Add(dietDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DietId = new SelectList(db.Diets, "Id", "DietTitle", dietDetail.DietId);
            return View(dietDetail);
        }

        // GET: DietDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DietDetail dietDetail = db.DietDetails.Find(id);
            if (dietDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.DietId = new SelectList(db.Diets, "Id", "DietTitle", dietDetail.DietId);
            return View(dietDetail);
        }

        // POST: DietDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Meal,FoodMenu,Note,Date,DietId")] DietDetail dietDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dietDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DietId = new SelectList(db.Diets, "Id", "DietTitle", dietDetail.DietId);
            return View(dietDetail);
        }

        // GET: DietDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DietDetail dietDetail = db.DietDetails.Find(id);
            if (dietDetail == null)
            {
                return HttpNotFound();
            }
            return View(dietDetail);
        }

        // POST: DietDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DietDetail dietDetail = db.DietDetails.Find(id);
            db.DietDetails.Remove(dietDetail);
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
