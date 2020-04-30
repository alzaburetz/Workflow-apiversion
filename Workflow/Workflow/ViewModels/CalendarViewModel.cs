using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using Workflow.Models;
using Workflow.Views;

namespace Workflow.ViewModels
{
    public class CalendarViewModel : BaseViewModel
    {
        readonly INavigation Navigation;
        public ObservableCollection<CalendarModel> CalendarList { get; set; }
        public Command GetUser { get; set; }
        public Command CalculateCalendar { get; set; }
        public Command Edit { get; set; }
        public UserModel User { get; set; }
        private string month;
        public string Month
        {
            get => month;
            set
            {
                month = value;
                OnPropertyChanged("Month");
            }
        }
        public CalendarViewModel(INavigation navigation, UserModel user = null)
        {
            this.User = user;
            this.Navigation = navigation;
            CalendarList = new ObservableCollection<CalendarModel>();
            if (user == null)
            {
                MessagingCenter.Subscribe<ProfileEditViewModel, UserModel>(this, "UpdateUser", (subscriber, data) => User = data);
                MessagingCenter.Subscribe<MainPageViewModel, UserModel>(this, "SetUser", (subscriber, data) => User = data);
            } 
            else
            {
                Title = $"{user.Name} {user.Surname}";
            }
            CalculateCalendar = new Command(async () =>
            {
                try
                {
                    User.NextWorkDay = DateTimeOffset.FromUnixTimeSeconds(User.FirstWork).DateTime;
                    IsBusy = true;
                    CalendarList.Clear();
                    var list = CalculateCalendarMethod();
                    foreach (var day in list)
                    {
                        this.CalendarList.Add(day);
                    }
                    IsBusy = false;
                }
                catch
                {

                }
            });
            Edit = new Command(async () =>
            {
                await Navigation.PushAsync(new CalendarEdit(new CalendarEditViewModel(this.CalendarList, this.Month)));
            });
        }

        private List<CalendarModel> CalculateCalendarMethod()
        {
            var result = new List<CalendarModel>(42);
            var today = DateTime.Now;
            this.Month = today.ToString("MMMM");
            var firstday = new DateTime(today.Year, today.Month, 1);
            var startofweek = new DateTime(today.Year, 1, 1).AddDays(firstday.DayOfYear - (int)firstday.DayOfWeek);
            int i = 0;
            var day = startofweek;
            while (i < result.Capacity)
            {
                var calendar = new CalendarModel();
                calendar.DayOfMonth = day.Day;
                calendar.DayOfWeek = (int)day.DayOfWeek == 0 ? 6 : (int)day.DayOfWeek - 1;
                calendar.IsThisMonth = day.Month == today.Month;
                calendar.Workday = this.User.WorksDayOf(day);
                calendar.NumberOfWeek = i / 7;
                result.Add(calendar);
                i++;
                day = new DateTime(day.Year, day.Month, day.Day).AddDays(1);
            }
            return result;
        }

        private void CalculateCalendarMethodStat()
        {
            var today = DateTime.Now;
            var firstday = new DateTime(today.Year, today.Month, 1);
            var startofweek = new DateTime(today.Year, 1, 1).AddDays(firstday.DayOfYear - (int)firstday.DayOfWeek);
            int i = 0;
            var day = startofweek;
            while (i < 42)
            {
                var calendar = new CalendarModel();
                calendar.DayOfMonth = day.Day;
                calendar.DayOfWeek = (int)day.DayOfWeek;
                calendar.IsThisMonth = day.Month == today.Month;
                calendar.Workday = this.User.WorksDayOf(day);
                calendar.NumberOfWeek = i / 7;
                CalendarList.Add((CalendarModel)calendar as CalendarModel);
                i++;
                day = new DateTime(day.Year, day.Month, day.Day).AddDays(1);
            }
        }
    }
}
