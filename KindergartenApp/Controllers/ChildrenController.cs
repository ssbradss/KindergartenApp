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
    public class ChildrenController : Controller
    {
        private Kindergarten_DBEntities db = new Kindergarten_DBEntities();

        // GET: Children
        public ActionResult Index()
        {
            var children = db.Children.Include(c => c.Class).Include(c => c.Diet).Include(c => c.TakingCare);
            return View(children.ToList());
        }

        // GET: Children/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = db.Children.Find(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            return View(child);
        }

        // GET: Children/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "ClassTitle");
            ViewBag.DietId = new SelectList(db.Diets, "Id", "DietTitle");
            ViewBag.TakingCareId = new SelectList(db.TakingCares, "Id", "TakingCareTitle");
            return View();
        }

        // POST: Children/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Dob,Gender,ParentName,ParentPhoneNumber,Address,ClassId,DietId,TakingCareId")] Child child)
        {
            if (ModelState.IsValid)
            {
                db.Children.Add(child);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.Classes, "Id", "ClassTitle", child.ClassId);
            ViewBag.DietId = new SelectList(db.Diets, "Id", "DietTitle", child.DietId);
            ViewBag.TakingCareId = new SelectList(db.TakingCares, "Id", "TakingCareTitle", child.TakingCareId);
            return View(child);
        }

        // GET: Children/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = db.Children.Find(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "ClassTitle", child.ClassId);
            ViewBag.DietId = new SelectList(db.Diets, "Id", "DietTitle", child.DietId);
            ViewBag.TakingCareId = new SelectList(db.TakingCares, "Id", "TakingCareTitle", child.TakingCareId);
            return View(child);
        }

        // POST: Children/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Dob,Gender,ParentName,ParentPhoneNumber,Address,ClassId,DietId,TakingCareId")] Child child)
        {
            if (ModelState.IsValid)
            {
                db.Entry(child).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "ClassTitle", child.ClassId);
            ViewBag.DietId = new SelectList(db.Diets, "Id", "DietTitle", child.DietId);
            ViewBag.TakingCareId = new SelectList(db.TakingCares, "Id", "TakingCareTitle", child.TakingCareId);
            return View(child);
        }

        // GET: Children/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = db.Children.Find(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            return View(child);
        }

        // POST: Children/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Child child = db.Children.Find(id);
            db.Children.Remove(child);
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
