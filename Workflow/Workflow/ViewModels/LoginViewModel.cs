using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Workflow.Models;
using Workflow.Utils;

namespace Workflow.ViewModels
{
    public class LoginViewModel: BaseViewModel
    {
        public Command Login { get; set; }
        public LoginViewModel()
        {
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
