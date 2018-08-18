using KindergartenApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KindergartenApp.Controllers
{
    public class AutoCompleteController : Controller
    {
        private Kindergarten_DBEntities db = new Kindergarten_DBEntities();
        // GET: AutoComplete
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AutoComplete_GetChildrenViewModel()
        {
            var children = db.Children.Select(c => new ChildrenViewModel
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Dob = c.Dob,
                //Gender = c.Gender == true ? "Male" : "Female",
                GenderVm = new GenderViewModel
                {
                    Gender = c.Gender ? "Male" : "Female",
                },
                ParentName = c.ParentName,
                ClassVm = new ClassesViewModel
                {
                    Id = c.ClassId ?? 0,
                    ClassTitle = c.ClassId == null ? "Not Set" : c.Class.ClassTitle,
                },
                DietVm = new DietsViewModel
                {
                    Id = c.DietId ?? 0,
                    DietTitle = c.DietId == null ? "Not Set" : c.Diet.DietTitle,
                },
                TakingCareVm = new TakingCaresViewModel
                {
                    Id = c.TakingCareId ?? 0,
                    TakingCareTitle = c.TakingCareId == null ? "Not Set" : c.TakingCare.TakingCareTitle,
                },
                ParentPhoneNumber = c.ParentPhoneNumber,
                Address = c.Address,
            });

            return Json(children, JsonRequestBehavior.AllowGet);
        }
    }
}