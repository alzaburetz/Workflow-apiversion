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

using Plugin.ContactService;
using Plugin.ContactService.Shared;

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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadContactsCommand.Execute(null);
        }

        private void Phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > e.OldTextValue?.Length)
            {
                viewModel.FilterContacts.Execute(Regex.Replace(e.NewTextValue.Replace("+7", "8"), @"\D", ""));
            }
            else
            {
                viewModel.Searching = false;
            }
        }

        private void SelectContats(object sender, SelectionChangedEventArgs args)
        {
            try
            {
                var contact = (sender as CollectionView).SelectedItem as Contact;
                FullName.Text = contact.Name;
                var number_input = Regex.Replace(Phone.Text.Replace("+7", "8"), @"\D", "");
                var number = contact.Numbers.Find(x => x.Contains(number_input));
                StringBuilder builder = new StringBuilder();
                builder.Append("+7 ")
                       .Append($"({number.Substring(1, 3)}) ")
                       .Append($"{number.Substring(4, 3)} ")
                       .Append($"{number.Substring(7, 2)} ")
                       .Append($"{number.Substring(9, 2)}");
                Phone.Text = builder.ToString();
                Email.Text = contact.Email;
                viewModel.Searching = false;
                (sender as CollectionView).SelectedItem = null;
            }
            catch
            {

            }
        }
    }
}