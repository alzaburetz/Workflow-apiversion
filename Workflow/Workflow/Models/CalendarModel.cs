using System;
using System.Collections.Generic;
using System.Text;

using Workflow.ViewModels;

namespace Workflow.Models
{
    public class CalendarModel:BaseViewModel
    {
        public int DayOfMonth { get; set; }
        bool _workday;
        public bool Workday
        {
            get => _workday;
            set
            {
                _workday = value;
                OnPropertyChanged("Workday");
            }
        }
        public int DayOfWeek { get; set; }
        public bool IsThisMonth { get; set; }
        public int NumberOfWeek { get; set; }
    }
}
