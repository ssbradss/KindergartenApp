using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KindergartenApp.Models
{
    public class ChildrenViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public System.DateTime Dob { get; set; }
        [Required]
        public string ParentName { get; set; }
        [Required]
        public int ParentPhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [UIHint("ClassesViewModel")]
        public ClassesViewModel ClassVm { get; set; }
        [UIHint("GenderViewModel")]
        public GenderViewModel GenderVm { get; set; }
        [UIHint("DietsViewModel")]
        public DietsViewModel DietVm { get; set; }
        [UIHint("TakingCaresViewModel")]
        public TakingCaresViewModel TakingCareVm { get; set; }
    }

    public class GenderViewModel
    {
        public string Gender { get; set; }
    }

    public class DietsViewModel
    {
        public int Id { get; set; }
        public string DietTitle { get; set; }
    }
    public class TakingCaresViewModel
    {
        public int Id { get; set; }
        public string TakingCareTitle { get; set; }
    }

    public class ClassesViewModel
    {
        public int Id { get; set; }
        public string ClassTitle { get; set; }
        public string ClassColor { get; set; }
    }

    public class ChartChildrenInGeneralByClass
    {
        public List<int> ChildrenNumber;
        public List<string> Classname;
        public List<int> Male;
        public List<int> Female;
    }

    public class ChartChildrenInGeneral
    {
        public int ChildrenNumber;
        public string Classname;
        public int MaleNumber;
        public int FemaleNumber;
    }
}