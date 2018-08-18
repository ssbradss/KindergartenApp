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
    public class TakingCareDetailsController : Controller
    {
        private Kindergarten_DBEntities db = new Kindergarten_DBEntities();

        // GET: TakingCareDetails
        public ActionResult Index()
        {
            var takingCareDetails = db.TakingCareDetails.Include(t => t.TakingCare);
            return View(takingCareDetails.ToList());
        }

        // GET: TakingCareDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TakingCareDetail takingCareDetail = db.TakingCareDetails.Find(id);
            if (takingCareDetail == null)
            {
                return HttpNotFound();
            }
            return View(takingCareDetail);
        }

        // GET: TakingCareDetails/Create
        public ActionResult Create()
        {
            ViewBag.TakingCareId = new SelectList(db.TakingCares, "Id", "TakingCareTitle");
            return View();
        }

        // POST: TakingCareDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Meal,FoodMenu,Activity,Note,TakingCareId")] TakingCareDetail takingCareDetail)
        {
            if (ModelState.IsValid)
            {
                db.TakingCareDetails.Add(takingCareDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TakingCareId = new SelectList(db.TakingCares, "Id", "TakingCareTitle", takingCareDetail.TakingCareId);
            return View(takingCareDetail);
        }

        // GET: TakingCareDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TakingCareDetail takingCareDetail = db.TakingCareDetails.Find(id);
            if (takingCareDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.TakingCareId = new SelectList(db.TakingCares, "Id", "TakingCareTitle", takingCareDetail.TakingCareId);
            return View(takingCareDetail);
        }

        // POST: TakingCareDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Meal,FoodMenu,Activity,Note,TakingCareId")] TakingCareDetail takingCareDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(takingCareDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TakingCareId = new SelectList(db.TakingCares, "Id", "TakingCareTitle", takingCareDetail.TakingCareId);
            return View(takingCareDetail);
        }

        // GET: TakingCareDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TakingCareDetail takingCareDetail = db.TakingCareDetails.Find(id);
            if (takingCareDetail == null)
            {
                return HttpNotFound();
            }
            return View(takingCareDetail);
        }

        // POST: TakingCareDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TakingCareDetail takingCareDetail = db.TakingCareDetails.Find(id);
            db.TakingCareDetails.Remove(takingCareDetail);
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
