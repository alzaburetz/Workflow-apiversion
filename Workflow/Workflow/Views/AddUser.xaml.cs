using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Workflow.Models;
using Workflow.ViewModels;
using System.Text.RegularExpressions;

namespace Workflow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddUser : ContentPage
    {
        AddUserViewModel viewModel { get; set; }
        public AddUser()
        {
            InitializeComponent();
            BindingContext = viewModel = new AddUserViewModel(this.Navigation);
        }

        void AddUserButton(object sender, EventArgs args)
        {
            if (FullName.Text.Length == 0 || 
                !FullName.Text.Contains(" ") || 
                Schedule.Text.Length < 5 || 
                Phone.Text.Length < 18)
            {
                DisplayAlert(null, "Для продолжения введите корректные данные!", "ОК");
                return;
            }
            var user = new UserModel();
            var names = FullName.Text.Split(' ');
            user.Name = names[0];
            user.Surname = names[1];
            var schedule = Schedule.Text.Split('/');
            user.Workdays = int.Parse(schedule[0].Trim());
            user.Weekdays = int.Parse(schedule[1].Trim());
            user.Phone = Regex.Replace(Phone.Text.Replace("+7", "8"), @"\D", "");
            user.Email = Email.Text;
            user.FirstWork = (new DateTimeOffset(FirstDay.Date).ToUnixTimeSeconds());
            viewModel.AddNewUserCommand.Execute(user);
        }
    }
}