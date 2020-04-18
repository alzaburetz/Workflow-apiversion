using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

using Workflow.ViewModels;

namespace Workflow.Models
{
    public class UserModel:BaseViewModel
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
        public DateTime NextWorkDay { get; set; }

        public bool WorksToday()
        {
            var span = TimeSpan.FromSeconds(FirstWork).TotalDays;
            return (span) % (Workdays + Weekdays) <= Workdays - 1;
        }

        public bool WorksDayOf(int day)
        {
            var span = TimeSpan.FromSeconds(FirstWork).TotalDays + day;
            return (span) % (Workdays + Weekdays) <= Workdays;
        }
    }
}
