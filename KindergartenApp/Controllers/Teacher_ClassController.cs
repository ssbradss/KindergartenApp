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
    public class Teacher_ClassController : Controller
    {
        private Kindergarten_DBEntities db = new Kindergarten_DBEntities();

        // GET: Teacher_Class
        public ActionResult Index()
        {
            var teacher_Class = db.Teacher_Class.Include(t => t.Class).Include(t => t.Teacher);
            return View(teacher_Class.ToList());
        }

        // GET: Teacher_Class/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher_Class teacher_Class = db.Teacher_Class.Find(id);
            if (teacher_Class == null)
            {
                return HttpNotFound();
            }
            return View(teacher_Class);
        }

        // GET: Teacher_Class/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "ClassTitle");
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "FirstName");
            return View();
        }

        // POST: Teacher_Class/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TeacherId,ClassId")] Teacher_Class teacher_Class)
        {
            if (ModelState.IsValid)
            {
                db.Teacher_Class.Add(teacher_Class);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.Classes, "Id", "ClassTitle", teacher_Class.ClassId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "FirstName", teacher_Class.TeacherId);
            return View(teacher_Class);
        }

        // GET: Teacher_Class/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher_Class teacher_Class = db.Teacher_Class.Find(id);
            if (teacher_Class == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "ClassTitle", teacher_Class.ClassId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "FirstName", teacher_Class.TeacherId);
            return View(teacher_Class);
        }

        // POST: Teacher_Class/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TeacherId,ClassId")] Teacher_Class teacher_Class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher_Class).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "ClassTitle", teacher_Class.ClassId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "FirstName", teacher_Class.TeacherId);
            return View(teacher_Class);
        }

        // GET: Teacher_Class/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher_Class teacher_Class = db.Teacher_Class.Find(id);
            if (teacher_Class == null)
            {
                return HttpNotFound();
            }
            return View(teacher_Class);
        }

        // POST: Teacher_Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher_Class teacher_Class = db.Teacher_Class.Find(id);
            db.Teacher_Class.Remove(teacher_Class);
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
