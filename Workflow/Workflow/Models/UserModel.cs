using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;

using Workflow.ViewModels;

namespace Workflow.Models
{
    public class UserModel:BaseViewModel, ICloneable
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("surname")]
        public string Surname { get; set; }
        [JsonProperty("workdays")]
        public int Workdays { get; set; }
        [JsonProperty("weekdays")]
        public int Weekdays { get; set; }
        [JsonProperty("firstwork")]
        public long FirstWork { get; set; }
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("groups")]
        public List<string> Groups { get; set; }
        string _formatted;
        public string GraphFormatted
        {
            get => _formatted;
            set
            {
                _formatted = value;
                OnPropertyChanged("GraphFormatted");
            }
        }
        [JsonProperty("push_token")]
        public string Push { get; set; }
        [JsonProperty("schedule")]
        public List<CalendarModel> Schedule { get; set; }
        public DateTime NextWorkDay { get; set; }
        private bool workstoday;
        public bool Workstoday
        {
            get => workstoday;
            set
            {
                workstoday = value;
                OnPropertyChanged("Workstoday");
            }
        }

        public bool WorksToday()
        {
            return WorksDayOf(DateTime.Now);
        }

        public bool WorksDayOf(DateTime day)
        {
            var span = (day - NextWorkDay).TotalDays;// >= 0 ? (NextWorkDay - day).TotalDays : (Workdays + Weekdays) + (NextWorkDay - day).TotalDays;
            if (span < 0)
            {
                if (span <= -(Weekdays + Workdays + 1))
                {
                    span = Math.Abs(span) + Workdays - 1;
                }
                else
                {
                    span += (Weekdays + Workdays);
                }
            }
            return (span) % (Workdays + Weekdays) <= Workdays - 1;
        }
        public List<CalendarModel> SortCalendar()
        {
            var month = DateTime.Now.Month;
            List<CalendarModel> result = new List<CalendarModel>();
            for (int i = 0; i < this.Schedule.Count; i++)
            {
                var day = this.Schedule[i];
                day.NumberOfWeek = i / 7;
                day.IsThisMonth = day.Month == month;
                result.Add(day);
            }
            return result;
        }

        public void CalculateCalendar()
        {
            var result = new List<CalendarModel>(42);
            var today = DateTime.Now;
            var firstday = new DateTime(today.Year, today.Month, 1);
            var startofweek = new DateTime(today.Year, 1, 1).AddDays(firstday.DayOfYear - (int)firstday.DayOfWeek);
            int i = 0;
            var day = startofweek;
            while (i < result.Capacity)
            {
                var calendar = new CalendarModel();
                calendar.DayOfMonth = day.Day;
                calendar.DayOfWeek = (int)day.DayOfWeek == 0 ? 7 : (int)day.DayOfWeek;
                calendar.IsThisMonth = day.Month == today.Month;
                calendar.Workday = this.WorksDayOf(day);
                calendar.NumberOfWeek = i / 7;
                calendar.Month = day.Month;
                result.Add(calendar);
                i++;
                day = new DateTime(day.Year, day.Month, day.Day).AddDays(1);
            }
            this.Schedule = result;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
