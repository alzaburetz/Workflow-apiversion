using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using Workflow.Models;

namespace Workflow.ViewModels
{
    public class CalendarViewModel : BaseViewModel
    {
        public ObservableCollection<CalendarModel> CalendarList { get; set; }
        public Command GetUser { get; set; }
        public Command CalculateCalendar { get; set; }
        public UserModel User { get; set; }
        public CalendarViewModel(UserModel user = null)
        {
            User = user;
            CalendarList = new ObservableCollection<CalendarModel>();
            CalculateCalendar = new Command(async () =>
            {
                CalendarList.Clear();
                if (this.User == null)
                {
                    var resp = await HttpService.GetRequest<ResponseModel<UserModel>>("user?search=89157508874");
                    if (resp.Code == 200)
                    {
                        this.User = resp.Response;
                        this.User.NextWorkDay = DateTimeOffset.FromUnixTimeSeconds(long.Parse(this.User.FirstWork)).UtcDateTime;
                    }
                    else
                    {
                        return;
                    }
                    CalculateCalendarMethodStat();
                    return;
                    var list = CalculateCalendarMethod();
                    foreach (CalendarModel day in list)
                    {
                        CalendarList.Add(day);
                    }
                }
                else
                {
                    var list = CalculateCalendarMethod();
                    foreach (var day in list)
                    {
                        this.CalendarList.Add(day);
                    }
                }
            });
        }

        private List<CalendarModel> CalculateCalendarMethod()
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
                calendar.DayOfWeek = (int)day.DayOfWeek;
                calendar.IsThisMonth = day.Month == today.Month;
                calendar.Workday = this.User.WorksDayOf(day.DayOfYear);
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
                calendar.Workday = this.User.WorksDayOf(day.DayOfYear);
                CalendarList.Add((CalendarModel)calendar as CalendarModel);
                i++;
                day = new DateTime(day.Year, day.Month, day.Day).AddDays(1);
            }
        }
    }
}
