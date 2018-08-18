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
    public class TaskEventViewModel: ISchedulerEvent
    {
        public TaskEventViewModel()
        {
        }

        public TaskEventViewModel(TaskEvent taskEvent)
        {
                Id = taskEvent.Id;
                Title = taskEvent.Title;
                Start = DateTime.SpecifyKind(taskEvent.Start, DateTimeKind.Utc);
                End = DateTime.SpecifyKind(taskEvent.End, DateTimeKind.Utc);
                StartTimezone = taskEvent.StartTimeZone;
                EndTimezone = taskEvent.EndTimeZone;
                Description = taskEvent.Description;
                IsAllDay = taskEvent.IsAllDay;
                RecurrenceRule = taskEvent.RecurrenceRule;
                RecurrenceException = taskEvent.RecurrenceException;
                RecurrenceID = taskEvent.RecurrenceId;
                OwnerID = taskEvent.OwnerId ?? 0;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        private DateTime start;
        public DateTime Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value.ToUniversalTime();
            }
        }

        private DateTime end;
        public DateTime End
        {
            get
            {
                return end;
            }
            set
            {
                end = value.ToUniversalTime();
            }
        }

        public string StartTimezone { get; set; }
        public string EndTimezone { get; set; }

        public string RecurrenceRule { get; set; }
        public int? RecurrenceID { get; set; }
        public string RecurrenceException { get; set; }
        public bool IsAllDay { get; set; }
        public int? OwnerID { get; set; }

        public TaskEvent ToEntity()
        {
            return new TaskEvent
            {
                Id = Id,
                Title = Title,
                Start = Start,
                End = End,
                StartTimeZone = StartTimezone,
                EndTimeZone = EndTimezone,
                Description = Description,
                IsAllDay = IsAllDay,
                RecurrenceRule = RecurrenceRule,
                RecurrenceException = RecurrenceException,
                RecurrenceId = RecurrenceID,
                OwnerId = OwnerID ,
            };
        }
    }
}
