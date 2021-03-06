//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KindergartenApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TaskEvent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public System.DateTime Start { get; set; }
        public System.DateTime End { get; set; }
        public string StartTimeZone { get; set; }
        public string EndTimeZone { get; set; }
        public string Description { get; set; }
        public bool IsAllDay { get; set; }
        public string RecurrenceException { get; set; }
        public string RecurrenceRule { get; set; }
        public Nullable<int> RecurrenceId { get; set; }
        public Nullable<int> OwnerId { get; set; }
    
        public virtual Class Class { get; set; }
    }
}
