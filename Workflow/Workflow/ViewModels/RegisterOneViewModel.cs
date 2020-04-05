using System;
using System.Text;
using Xamarin.Forms;

using Workflow.Models;
using Workflow.Views;

namespace Workflow.ViewModels
{
    public class RegisterOneViewModel:BaseViewModel
    {
        public INavigation Navigation;
        public Command RegisterStepOne { get; set; }
        public Command FirstLogin { get; set; }
        UserModel User { get; set; }
        public RegisterOneViewModel()
        {
            RegisterStepOne = new Command<UserAuthModel>(async (auth) =>
            {
                IsBusy = true;
                var resp = await HttpService.PostRequest<ResponseModel<UserModel>, UserAuthModel>("user/register", auth);
                User = resp.Response;
                if (resp.Code != 200)
                {
                    await Application.Current.MainPage.DisplayAlert($"{resp.Code}", $"{resp.Errors[0]}", "OK");
                    IsBusy = false;
                }
                else
                {
                    FirstLogin.Execute(auth);
                }
            });
            FirstLogin = new Command<UserAuthModel>(async (userauth) =>
            {
                var resp = await HttpService.PostRequest<ResponseModel<string>, UserAuthModel>("user/login", userauth);
                HttpService.Token = resp.Response;
                IsBusy = false;
                await Navigation.PushModalAsync(new CreateProfile(new CreateProfileViewModel(User)));
            });
        }

    }
}
