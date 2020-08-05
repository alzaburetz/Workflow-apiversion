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

using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Workflow.Services;


namespace Workflow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        LoginViewModel viewModel { get; set; }
        public Login()
        {
            InitializeComponent();
            BindingContext = viewModel = new LoginViewModel(this.Navigation);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await CrossPermissions.Current.RequestPermissionsAsync(Permission.Contacts);
        }

        protected async void GoToRegistration(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new RegisterOne(new ViewModels.RegisterOneViewModel()));
        }

        private async void ExecuteLogin(object sender, EventArgs args)
        {
            if (string.IsNullOrEmpty(Number.Text))
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
                Phone = finalPhone,
                Password = Password.Text
            };
            viewModel.Login.Execute(user);
        }

        private async void GoogleLogin(object sender, EventArgs e)
        {
            await DependencyService.Get<IFirebaseAuth>().Authenticate();
        }

        private async void VkLogin(object sender, EventArgs e)
        {
            DependencyService.Get<IVkAuth>().Login();
        }
    }
}