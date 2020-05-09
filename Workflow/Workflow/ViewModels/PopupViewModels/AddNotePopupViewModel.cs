using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

using Workflow.Models;
using Workflow.ViewModels;
using Xamarin.Forms;

namespace Workflow.ViewModels.PopupViewModels
{
    public class AddNotePopupViewModel:BaseViewModel
    {
        CalendarModel _day;
        public CalendarModel Day
        {
            get => _day;
            set
            {
                _day = value;
                OnPropertyChanged("Day");
            }
        }
        public string PreviousNote { get; set; }

        public Command AddNote { get; set; }
        public Command ClearNote { get; set; }
        readonly INavigation Navigation;
        long _date;
        public long Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }
        public AddNotePopupViewModel(CalendarModel day, INavigation navigation)
        {
            Day = day;
            Date = (new DateTimeOffset(new DateTime(DateTime.Now.Year, day.Month, day.DayOfMonth))).ToUnixTimeSeconds();
            PreviousNote = day.Note;
            Navigation = navigation;
            AddNote = new Command<string>(async (note) =>
            {
                Day.Note = note;
                await Navigation.PopPopupAsync();
            });
            ClearNote = new Command(async () =>
            {
                Day.Note = PreviousNote;
                await Navigation.PopPopupAsync();
            });
        }
    }
}
