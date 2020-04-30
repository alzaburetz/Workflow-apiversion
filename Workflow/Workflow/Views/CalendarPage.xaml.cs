using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Workflow.ViewModels;

namespace Workflow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {
        CalendarViewModel viewModel { get; set; }
        public CalendarPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new CalendarViewModel(this.Navigation);
            MessagingCenter.Subscribe<MainPage>(this, "LoadCalendar", (sender) => viewModel.CalculateCalendar.Execute(null));
        }

        public CalendarPage(Workflow.Models.UserModel user)
        {
            InitializeComponent();
            BindingContext = viewModel = new CalendarViewModel(this.Navigation, user);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.CalculateCalendar.Execute(null);
        }
    }
}