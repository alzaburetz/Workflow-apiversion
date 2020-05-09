using System;
using System.Collections.Generic;
using System.Text;

using Workflow.ViewModels;

using Newtonsoft.Json;

namespace Workflow.Models
{
    public class CalendarModel:BaseViewModel, ICloneable
    {
        [JsonProperty("day")]
        public int DayOfMonth { get; set; }
        bool _workday;
        [JsonProperty("works")]
        public bool Workday
        {
            get => _workday;
            set
            {
                _workday = value;
                OnPropertyChanged("Workday");
            }
        }
        [JsonProperty("day_of_week")]
        public int DayOfWeek { get; set; }
        [JsonProperty("month")]
        public int Month { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        public bool IsThisMonth { get; set; }
        public int NumberOfWeek { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
