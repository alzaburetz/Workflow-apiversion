using System;
using System.Text;
using Xamarin.Forms;

using Workflow.Models;

namespace Workflow.ViewModels
{
    public class RegisterOneViewModel:BaseViewModel
    {
        public Command RegisterStepOne { get; set; }
        public RegisterOneViewModel()
        {
            RegisterStepOne = new Command<UserAuthModel>(async (auth) =>
            {
                var resp = await HttpService.PostRequest<ResponseModel<UserModel>, UserAuthModel>("user/register", auth);
                if (resp.Code != 200)
                {
                    await Application.Current.MainPage.DisplayAlert($"{resp.Code}", $"{resp.Errors[0]}", "OK");
                }
                else
                {

                }
            });
        }

    }
}
