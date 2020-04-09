using System;
using System.Collections.Generic;
using System.Text;

using Workflow.ViewModels;

namespace Workflow.Models
{
    public class CalendarModel:BaseViewModel
    {
        public int DayOfMonth { get; set; }
        public bool Workday { get; set; }
        public int DayOfWeek { get; set; }
        public bool IsThisMonth { get; set; }
    }
}
