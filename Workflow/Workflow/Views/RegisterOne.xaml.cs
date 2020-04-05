using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Workflow.ViewModels;
using Workflow.Models;

using System.Text.RegularExpressions;

namespace Workflow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterOne : ContentPage
    {
        public RegisterOne()
        {
            InitializeComponent();
        }
        public RegisterOne(RegisterOneViewModel viewModel):this()
        {
            viewModel.Navigation = this.Navigation;
            BindingContext = viewModel;
            
        }

        protected async void Register(object sender, EventArgs args)
        {
            if (string.IsNullOrEmpty(Number.Text) || 
                string.IsNullOrEmpty(Email.Text) ||
                string.IsNullOrEmpty(User.Text) ||
                string.IsNullOrEmpty(Password.Text))
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Заполните все поля для продолжения!", "ОК");
                return;
            }
            var phone = Number.Text;
            var finalPhone = Regex.Replace(phone, @"\(", "");
            finalPhone = Regex.Replace(finalPhone, @"\)", "");
            finalPhone = Regex.Replace(finalPhone, @"\s+", "");
            finalPhone = finalPhone.Replace("+7", "8");
            var user = new UserAuthModel()
            {
                Email = Email.Text,
                Name = User.Text,
                Password = Password.Text,
                Phone = finalPhone
            };
            (BindingContext as RegisterOneViewModel).RegisterStepOne.Execute(user);
        }

        private void Number_Completed(object sender, EventArgs e)
        {
            Email.Focus();
        }

        private void Email_Completed(object sender, EventArgs e)
        {
            User.Focus();
        }

        private void User_Completed(object sender, EventArgs e)
        {
            Password.Focus();
        }
    }
}