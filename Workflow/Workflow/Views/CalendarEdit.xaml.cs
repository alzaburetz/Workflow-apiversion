using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Workflow.ViewModels;
using Workflow.Models;

namespace Workflow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarEdit : ContentPage
    {
        CalendarEditViewModel viewModel { get; set; }
        public CalendarEdit(CalendarEditViewModel vm)
        {
            InitializeComponent();
            BindingContext = viewModel = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadCalendar.Execute(null);
        }

        protected async void UpdateCalendar(object sender, EventArgs args)
        {
            MessagingCenter.Send<CalendarEdit, List<CalendarModel>>(this, "UpdateCalendar", viewModel.CalendarListEdit.ToList());
            await Navigation.PopAsync();
        }
    }
}