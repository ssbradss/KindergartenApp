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
    public class TaskEventsSchedulerController : Controller
    {
        private Kindergarten_DBEntities db = new Kindergarten_DBEntities();

        public ActionResult Index()
        {
            //var classes = db.Classes
            //            .Select(c => new ClassesViewModel
            //            {
            //                Id = c.Id,
            //                ClassTitle = c.ClassTitle
            //            })
            //            .OrderBy(e => e.ClassTitle);
            //List<ClassesViewModel> classesWithColor = new List<ClassesViewModel>();
            //foreach (var item in classes)
            //{
            //    item.ClassColor = GenerateColor(item.ClassTitle, item.Id);
            //    classesWithColor.Add(item);
            //}
            //MappingColor(classes);
            //ViewData["resource"] = classesWithColor;

            var classes = db.Classes
                        .Select(c => new ClassesViewModel
                        {
                            Id = c.Id,
                            ClassTitle = c.ClassTitle
                        })
                        .OrderBy(e => e.ClassTitle).ToList();
            MappingColor(classes);
            ViewData["resource"] = classes;
            return View();
        }

        public ActionResult TaskEvents_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = db.TaskEvents.ToList()
                        .Select(taskEvent => new TaskEventViewModel(taskEvent))
                        .AsQueryable();

            return Json(data.ToDataSourceResult(request));
        }

        public virtual JsonResult TaskEvents_Create([DataSourceRequest] DataSourceRequest request, TaskEventViewModel taskEvent)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty( taskEvent.Title))
                {
                     taskEvent.Title = "";
                }

                var entity = taskEvent.ToEntity();
                db.TaskEvents.Add(entity);
                db.SaveChanges();
                taskEvent.Id = entity.Id;
            }

            return Json(new[] { taskEvent }.ToDataSourceResult(request, ModelState));
        }
        public virtual JsonResult TaskEvents_Update([DataSourceRequest] DataSourceRequest request, TaskEventViewModel taskEvent)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty( taskEvent.Title))
                {
                     taskEvent.Title = "";
                }

                var entity = taskEvent.ToEntity();
                db.TaskEvents.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { taskEvent }.ToDataSourceResult(request, ModelState));
        }
        public virtual JsonResult TaskEvents_Destroy([DataSourceRequest] DataSourceRequest request, TaskEventViewModel taskEvent)
        {
            if (ModelState.IsValid)
            {
                var entity = taskEvent.ToEntity();
                db.TaskEvents.Attach(entity);
                db.TaskEvents.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { taskEvent }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private string GenerateColor(string title, int id)
        {
            char[] values = title.ToCharArray();
            double lastValue = id;
            foreach (char letter in values)
            {
                int value = Convert.ToInt32(letter);
                lastValue *= value;
            }
            while (lastValue > 999999)
            {
                lastValue = Math.Round(lastValue / 10);
            };
            string hexOutput = String.Format("#{0:X6}", (int)lastValue);

            return hexOutput;
        }

        private void MappingColor(List<ClassesViewModel> Classes)
        {
            List<string> color = new List<string>(
                new string[] { "#FF0000", "#FFFF00", "#00FF00", "#00FFFF", "#0000FF",
                    "#FF00FF", "#808080", "#800000", "#808000", "#008000", "#008080",
                    "#000080", "#800080"}
                );
            int i = 0;
            foreach (var item in Classes)
            {
                if (i > color.Count() - 1)
                {
                    i = i % color.Count();
                };
                item.ClassColor = color[i];
                i++;
            };
        }
    }
}