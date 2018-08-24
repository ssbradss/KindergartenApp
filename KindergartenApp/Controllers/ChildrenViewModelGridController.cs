using System;
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
    public class ChildrenViewModelGridController : Controller
    {
        private Kindergarten_DBEntities db = new Kindergarten_DBEntities();

        public ActionResult Index()
        {
            PopulateGender();
            PopulateClasses();
            PopulateDiets();
            PopulateTakingCares();
            GetGenderRatio();
            GetYearBornRatio();
            return View();
        }

        public ActionResult ChildrenViewModel_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Child> children = db.Children.Include(c => c.Class).Include(c => c.Diet).Include(c => c.TakingCare);
            //int? defaultClassId = db.Classes.FirstOrDefault().Id;
            //int? defaultDietId = db.Diets.FirstOrDefault().Id;
            //int? defaultTakingCareId = db.TakingCares.FirstOrDefault().Id;
            DataSourceResult result = children.ToDataSourceResult(request, c => new ChildrenViewModel
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

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ChildrenViewModel_Create([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ChildrenViewModel> children)
        {
            var entities = new List<ChildrenViewModel>();
            if (children != null && ModelState.IsValid)
            {
                foreach (var child in children)
                {
                    //child.Id == 0
                    var entity = new Child
                    {
                        FirstName = child.FirstName,
                        LastName = child.LastName,
                        Dob = child.Dob,
                        Gender = child.GenderVm.Gender == "Male" ? true : false,
                        ParentName = child.ParentName,
                        ParentPhoneNumber = child.ParentPhoneNumber,
                        Address = child.Address,
                        ClassId = child.ClassVm.Id,
                        DietId = child.DietVm.Id,
                        TakingCareId = child.TakingCareVm.Id,
                    };
                    db.Children.Add(entity);
                    //save changes to generate a new Id
                    if (db.SaveChanges() > 0)
                    {
                        //add the auto-incremented id to child.Id
                        //child.Id = entity.Id;
                        var item = db.Children.ToList().Last();
                        child.Id = item.Id;
                        entities.Add(child);
                    };
                };
            }

            return Json(entities.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ChildrenViewModel_Update([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ChildrenViewModel> children)
        {
            var entities = new List<ChildrenViewModel>();
            if (children != null && ModelState.IsValid)
            {
                foreach (var child in children)
                {
                    var entity = new Child
                    {
                        Id = child.Id,
                        FirstName = child.FirstName,
                        LastName = child.LastName,
                        Dob = child.Dob,
                        Gender = child.GenderVm.Gender == "Male" ? true : false,
                        ParentName = child.ParentName,
                        ParentPhoneNumber = child.ParentPhoneNumber,
                        Address = child.Address,
                        ClassId = child.ClassVm.Id,
                        DietId = child.DietVm.Id,
                        TakingCareId = child.TakingCareVm.Id,
                    };
                    entities.Add(child);
                    db.Children.Attach(entity);
                    db.Entry(entity).State = EntityState.Modified;
                }
                db.SaveChanges();
            }

            return Json(entities.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ChildrenViewModel_Destroy([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ChildrenViewModel> children)
        {
            var entities = new List<Child>();
            if (ModelState.IsValid)
            {
                foreach (var child in children)
                {
                    var entity = new Child
                    {
                        Id = child.Id,
                        FirstName = child.FirstName,
                        LastName = child.LastName,
                        Dob = child.Dob,
                        Gender = child.GenderVm.Gender == "Male" ? true : false,
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

        private void PopulateGender()
        {
            var genders = db.Children
                        .Select(c => new GenderViewModel
                        {
                            Gender = c.Gender ? "Male" : "Female",
                        })
                        .Distinct();
            ViewData["genders"] = genders;
            ViewData["defaultGender"] = genders.First();
        }
        public void PopulateClasses()
        {
            var classes = db.Classes
                        .Select(c => new ClassesViewModel
                        {
                            Id = c.Id,
                            ClassTitle = c.ClassTitle
                        })
                        .OrderBy(e => e.ClassTitle);

            ViewData["classes"] = classes;
            ViewData["defaultClass"] = classes.First();
        }

        private void PopulateDiets()
        {
            var diets = db.Diets
                        .Select(c => new DietsViewModel
                        {
                            Id = c.Id,
                            DietTitle = c.DietTitle
                        })
                        .OrderBy(e => e.DietTitle);

            ViewData["diets"] = diets;
            ViewData["defaultDiet"] = diets.First();
        }

        private void PopulateTakingCares()
        {
            var takingcares = db.TakingCares
                        .Select(c => new TakingCaresViewModel
                        {
                            Id = c.Id,
                            TakingCareTitle = c.TakingCareTitle,
                        })
                        .OrderBy(e => e.TakingCareTitle);

            ViewData["takingcares"] = takingcares;
            ViewData["defaultTakingCare"] = takingcares.First();
        }

        private void GetGenderRatio()
        {
            int allChild = db.Children.Count();
            double maleRatio = Math.Round((db.Children.Where(c => c.Gender == true).Count() / Convert.ToDouble(allChild) * 100), 2);
            double femaleRatio = 100 - maleRatio;
            dynamic abc = new dynamic[]
            {
                new {category="Male",value=maleRatio,color="#9de219"},
                new {category="Female",value=femaleRatio,color="#90cc38"},
            };
            ViewData["chart_gender"] = abc;
        }

        private void GetYearBornRatio()
        {
            var years = db.Children.Select(c => c.Dob.Year).Distinct().ToList();
            List<int> maleSeries = new List<int>();
            List<int> femaleSeries = new List<int>();
            var yearsToString = new List<string>();
            foreach (int year in years)
            {
                int males = db.Children.Where(c => c.Dob.Year == year && c.Gender == true).Count();
                maleSeries.Add(males);
                int females = db.Children.Where(c => c.Dob.Year == year && c.Gender == false).Count();
                femaleSeries.Add(females);
                yearsToString.Add(year.ToString());
            }
            ViewData["chart_yearBorn"] = yearsToString;
            ViewData["chart_yearBornMales"] = maleSeries;
            ViewData["chart_yearBornFemales"] = femaleSeries;
        }

        public ActionResult ChartChildrenInGeneral_Read()
        {
            var classes = db.Classes.OrderBy(c => c.Id).ToList();
            var result = new List<ChartChildrenInGeneral>();
            foreach(var item in classes)
            {
                result.Add(new ChartChildrenInGeneral {
                    Classname = db.Classes.Where(c => c.Id == item.Id).Select(c => c.ClassTitle).FirstOrDefault(),
                    ChildrenNumber = db.Children.Where(ch => ch.ClassId == item.Id).Count(),
                    MaleNumber = db.Children.Where(ch => ch.ClassId == item.Id && ch.Gender == true).Count(),
                    FemaleNumber = db.Children.Where(ch => ch.ClassId == item.Id && ch.Gender == false).Count()
                });
            }
            return Json(result);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}
