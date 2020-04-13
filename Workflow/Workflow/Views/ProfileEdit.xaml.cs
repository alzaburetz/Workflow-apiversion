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
    public partial class ProfileEdit : ContentPage
    {
        ProfileEditViewModel viewModel { get; set; }
        public ProfileEdit()
        {
            InitializeComponent();
            Datepicker.DateSelected += (s, e) =>
            {
                viewModel.User.NextWorkDay = e.NewDate;
                viewModel.User.FirstWork = new DateTimeOffset(e.NewDate).ToUnixTimeSeconds().ToString();
            };
        }

        public ProfileEdit(ProfileEditViewModel viewmodel) : this()
        {
            BindingContext = viewModel = viewmodel;
        }

        private void EditProfile(object sender, EventArgs args)
        {
            if (this.Schedule.Text.Length > 4)
            {
                var schedule = Schedule.Text.Split('/');
                this.viewModel.User.Workdays = int.Parse(schedule[0].Trim());
                this.viewModel.User.Weekdays = int.Parse(schedule[1].Trim());

                viewModel.UpdateUser.Execute(this.viewModel.User);
            }
            else
            {
                DisplayAlert(null, "Введите правильные данные!", "OK");
            }
        }
    }
}