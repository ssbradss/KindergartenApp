﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KindergartenApp.Models;

namespace KindergartenApp.Controllers
{
    public class ChildrenGridController : Controller
    {
        private Kindergarten_DBEntities db = new Kindergarten_DBEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Children_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Child> children = db.Children;
            DataSourceResult result = children.ToDataSourceResult(request, child => new {
                Id = child.Id,
                FirstName = child.FirstName,
                LastName = child.LastName,
                Dob = child.Dob,
                Gender = child.Gender,
                ParentName = child.ParentName,
                ParentPhoneNumber = child.ParentPhoneNumber,
                Address = child.Address,
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Children_Create([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Child> children)
        {
            var entities = new List<Child>();
            if (children != null && ModelState.IsValid)
            {
                foreach(var child in children)
                {
                    var entity = new Child
                    {
                            FirstName = child.FirstName,
                            LastName = child.LastName,
                            Dob = child.Dob,
                            Gender = child.Gender,
                            ParentName = child.ParentName,
                            ParentPhoneNumber = child.ParentPhoneNumber,
                            Address = child.Address,
                    };

                    db.Children.Add(entity);
                    entities.Add(entity);
                }
                db.SaveChanges();
            }

            return Json(entities.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Children_Update([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Child> children)
        {
            var entities = new List<Child>();
            if (children != null && ModelState.IsValid)
            {
                foreach(var child in children)
                {
                    var entity = new Child
                    {
                        Id = child.Id,
                        FirstName = child.FirstName,
                        LastName = child.LastName,
                        Dob = child.Dob,
                        Gender = child.Gender,
                        ParentName = child.ParentName,
                        ParentPhoneNumber = child.ParentPhoneNumber,
                        Address = child.Address,
                    };

                    entities.Add(entity);
                    db.Children.Attach(entity);
                    db.Entry(entity).State = EntityState.Modified;                        
                }
                db.SaveChanges();
            }

            return Json(entities.ToDataSourceResult(request, ModelState));
        } 

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Children_Destroy([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Child> children)
        {
            var entities = new List<Child>();
            if (ModelState.IsValid)
            {
                foreach(var child in children)
                {
                    var entity = new Child
                    {
                        Id = child.Id,
                        FirstName = child.FirstName,
                        LastName = child.LastName,
                        Dob = child.Dob,
                        Gender = child.Gender,
                        ParentName = child.ParentName,
                        ParentPhoneNumber = child.ParentPhoneNumber,
                        Address = child.Address,
                    };

                    entities.Add(entity);
                    db.Children.Attach(entity);
                    db.Children.Remove(entity);
                }
                db.SaveChanges();
            }

            return Json(entities.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
