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
        public CalendarEditViewModel(ObservableCollection<CalendarModel> calendar, string month)
        {
            this.Month = month;
            this.CalendarList = calendar;
            InvertDay = new Command<CalendarModel>((day) =>
            {
                day.Workday = !day.Workday;
            });
        }
    }
}
