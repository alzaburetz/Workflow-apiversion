using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Workflow.Models;
using Xamarin.Forms;

namespace Workflow.ViewModels
{
    public class CalendarEditViewModel:BaseViewModel
    {
        public Command InvertDay { get; set; }
        public Command LoadCalendar { get; set; }
        public Command UpdateCalendar { get; set; }
        readonly INavigation Navigation;
        string _month;
        public ObservableCollection<CalendarModel> CalendarListEdit { get; set; }
        UserModel _user;
        public string Month
        {
            get => _month;
            set
            {
                _month = value;
                OnPropertyChanged("Month");
            }
        }
        public CalendarEditViewModel(List<CalendarModel> calendar, string month, INavigation navigation)
        {
            this.Month = month;
            this.Navigation = navigation;
            this.CalendarListEdit = new ObservableCollection<CalendarModel>();
            //this._user = user;

            LoadCalendar = new Command(() =>
            {
                foreach (var day in calendar)
                {
                    CalendarListEdit.Add(day);
                }
            });
            
            InvertDay = new Command<CalendarModel>((day) =>
            {
                day.Workday = !day.Workday;
            });

            UpdateCalendar = new Command(async () =>
            {
                var calendr = new List<CalendarModel>();
                foreach (var day in CalendarListEdit)
                {
                    calendr.Add(day.Clone() as CalendarModel);
                }
                MessagingCenter.Send<CalendarEditViewModel, List<CalendarModel>>(this, "UpdateCalendar", calendr);
                await Navigation.PopAsync();
            });
        }
    }
}
