using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Workflow.Models;
using Workflow.Utils;
using Workflow.Views;

namespace Workflow.ViewModels
{
    public class LoginViewModel: BaseViewModel
    {
        public Command Login { get; set; }
        public Command SocialLogin { get; set; }
        readonly INavigation Navigation;
        public LoginViewModel(INavigation nav)
        {
            this.Navigation = nav;
            MessagingCenter.Subscribe<Object, UserAuthModel>(this, "GoogleLogin", (sender, user) =>
            {
                SocialLogin.Execute(user);
            });
            SocialLogin = new Command<UserAuthModel>(async (usr) =>
            {
                IsBusy = true;
                string phone = await Application.Current.MainPage.DisplayPromptAsync(null, "Для продолжения введите номер телефона", "OK", null, "Номер телефона", 12, Keyboard.Telephone);
                usr.Phone = phone;
                var response = await HttpService.PostRequest<ResponseModel<SocialToken>, UserAuthModel>("user/login/social", usr);
                if (response.Code == 200)
                {
                    if (!Application.Current.Properties.ContainsKey("Token"))
                        Application.Current.Properties.Add("Token", response.Response.Token);
                    else
                        Application.Current.Properties["Token"] = response.Response.Token;
                    await Application.Current.SavePropertiesAsync();
                    HttpService.Token = response.Response.Token;
                    if (response.Response.Logged)
                    {
                        Application.Current.MainPage = new MainPage();
                    }
                    else
                    {
                        UserModel User = new UserModel()
                        {
                            Name = usr.Name,
                            Surname = usr.Surname,
                            Email = usr.Email,
                            Phone = usr.Phone,
                        };
                        await Navigation.PushModalAsync(new CreateProfile(new CreateProfileViewModel(User)));
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Произошла ошибка", ListStringifyer.StringifyList(response.Errors), "OK");
                }
                IsBusy = false;
            });
            Login = new Command<UserAuthModel>(async (user) =>
            {
                IsBusy = true;
                var response = await HttpService.PostRequest<ResponseModel<string>, UserAuthModel>("user/login", user);
                if (response.Code == 200)
                {
                    if (!Application.Current.Properties.ContainsKey("Token"))
                        Application.Current.Properties.Add("Token", response.Response);
                    else
                        Application.Current.Properties["Token"] = response.Response;
                    await Application.Current.SavePropertiesAsync();
                    HttpService.Token = response.Response;
                    Application.Current.MainPage = new Workflow.Views.MainPage();
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.DisplayAlert("Ошибка", ListStringifyer.StringifyList(response.Errors), "OK"));
                }
                IsBusy = false;
            });
        }
    }
}
