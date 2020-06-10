using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using Workflow.Models;
using Workflow.Services;
using Workflow.Views;
using System.Linq;

using Rg.Plugins.Popup.Extensions;
using Workflow.Views.Popups;
using System.ComponentModel;

namespace Workflow.ViewModels
{
    public class CalendarViewModel : BaseViewModel
    {
        readonly INavigation Navigation;
        public ObservableCollection<CalendarModel> CalendarList { get; set; }
        public Command GetUser { get; set; }
        public Command CalculateCalendar { get; set; }
        public Command Edit { get; set; }
        public Command OpenNote { get; set; }
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
        private bool _canEdit;
        public bool CanEdit
        {
            get => _canEdit;
            set
            {
                _canEdit = value;
                OnPropertyChanged("CanEdit");
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
                MessagingCenter.Subscribe<MainPageViewModel, UserModel>(this, "SetUser", (subscriber, data) =>
                {
                    User = data;
                    this.User.Schedule = User.SortCalendar();
                });
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
                    CanEdit = Application.Current.Properties["email"].ToString() == User.Email;
                    CalendarList.Clear();
                    this.Month = DateTime.Now.ToString("MMMM");
                    var list = new List<CalendarModel>();
                    this.User.Schedule = User.SortCalendar();
                    if (User.Schedule[21].Month == DateTime.Now.Month)
                        list = User.Schedule;
                    else
                    {
                        list = CalculateCalendarMethod();
                        Task.Run(async () =>
                        {
                            this.User.Schedule = list;
                            await HttpService.PutRequest<ResponseModel<UserModel>, UserModel>("user/update", this.User);
                        });
                    }
                    foreach (var day in list)
                    {
                        var d = new CalendarModel();
                        d = day.Clone() as CalendarModel;
                        this.CalendarList.Add(d.Clone() as CalendarModel);
                    }
                    IsBusy = false;
                }
                catch
                {

                }
            });
            Edit = new Command(async () =>
            {
                List<CalendarModel> calendar = new List<CalendarModel>();
                foreach (var day in CalendarList)
                {
                    calendar.Add(day.Clone() as CalendarModel);
                }
                await Navigation.PushAsync(new CalendarEdit(new CalendarEditViewModel(calendar, this.Month, this.Navigation)));
            });

            OpenNote = new Command<CalendarModel>(async (day) =>
            {
                if (!string.IsNullOrEmpty(day.Note))
                {
                    var date = new DateTime(DateTime.Now.Year, day.Month, day.DayOfMonth).ToString("dd.MM.yyyy");
                    await Navigation.PushPopupAsync(new NotePopup(date, day.Note));
                }
                
            });

            MessagingCenter.Subscribe<CalendarEditViewModel, List<CalendarModel>>(this, "UpdateCalendar", (sender, calendar) =>
            {
                this.User.Schedule = calendar;
                Task.Run(async () =>
                {
                    var resp = await HttpService.PutRequest<ResponseModel<UserModel>, UserModel>("user/update", this.User);
                    if (resp.Code == 200)
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        CalculateCalendar.Execute(null);
                        this.User = resp.Response;
                        var now = DateTime.Now;
                        var workstoday = User.Schedule.Find(x => x.Month == now.Month && x.DayOfMonth == now.Day).Workday;
                        Color color = Color.Black;
                        if (workstoday)
                        {
                            color = Color.FromHex("#ff6161");
                        }
                        else
                        {
                            color = Color.FromHex("#237547");
                        }
                        DependencyService.Get<ISetStatusBarColor>().SetStatusBarColor(color);
                        App.Current.Resources["DarkColor"] = color;
                    });
                });
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
                calendar.DayOfWeek = (int)day.DayOfWeek == 0 ? 7 : (int)day.DayOfWeek;
                calendar.IsThisMonth = day.Month == today.Month;
                calendar.Workday = this.User.WorksDayOf(day);
                calendar.NumberOfWeek = i / 7;
                calendar.Month = day.Month;
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
