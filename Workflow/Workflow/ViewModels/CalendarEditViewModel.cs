using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Text;

using Workflow.Models;
using Xamarin.Forms;

namespace Workflow.ViewModels
{
    public class CalendarEditViewModel:BaseViewModel
    {
        public Command InvertDay { get; set; }
        ObservableCollection<CalendarModel> _calendarList;
        string _month;
        public ObservableCollection<CalendarModel> CalendarList
        {
            get => _calendarList;
            set
            {
                _calendarList = value;
                OnPropertyChanged("CalendarList");
            }
        }
        public string Month
        {
            get => _month;
            set
            {
                _month = value;
                OnPropertyChanged("Month");
            }
        }
        public CalendarEditViewModel(List<CalendarModel> calendar, string month)
        {
            this.Month = month;
            this.CalendarList = new ObservableCollection<CalendarModel>();
            foreach (var day in calendar)
            {
                CalendarList.Add(day);
            }
            InvertDay = new Command<CalendarModel>((day) =>
            {
                day.Workday = !day.Workday;
            });
        }
    }
}
