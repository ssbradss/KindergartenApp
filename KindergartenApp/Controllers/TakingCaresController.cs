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
    public class TakingCaresController : Controller
    {
        private Kindergarten_DBEntities db = new Kindergarten_DBEntities();

        // GET: TakingCares
        public ActionResult Index()
        {
            return View(db.TakingCares.ToList());
        }

        // GET: TakingCares/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TakingCare takingCare = db.TakingCares.Find(id);
            if (takingCare == null)
            {
                return HttpNotFound();
            }
            return View(takingCare);
        }

        // GET: TakingCares/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TakingCares/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TakingCareTitle")] TakingCare takingCare)
        {
            if (ModelState.IsValid)
            {
                db.TakingCares.Add(takingCare);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(takingCare);
        }

        // GET: TakingCares/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TakingCare takingCare = db.TakingCares.Find(id);
            if (takingCare == null)
            {
                return HttpNotFound();
            }
            return View(takingCare);
        }

        // POST: TakingCares/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TakingCareTitle")] TakingCare takingCare)
        {
            if (ModelState.IsValid)
            {
                db.Entry(takingCare).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(takingCare);
        }

        // GET: TakingCares/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TakingCare takingCare = db.TakingCares.Find(id);
            if (takingCare == null)
            {
                return HttpNotFound();
            }
            return View(takingCare);
        }

        // POST: TakingCares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TakingCare takingCare = db.TakingCares.Find(id);
            db.TakingCares.Remove(takingCare);
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
