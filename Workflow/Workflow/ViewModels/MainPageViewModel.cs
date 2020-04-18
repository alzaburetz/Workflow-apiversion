using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Workflow.Models;
using Workflow.Views;

namespace Workflow.ViewModels
{
    public class MainPageViewModel:BaseViewModel
    {
        UserModel User { get; set; }
        public Command CheckUser { get; set; }
        public MainPageViewModel()
        {
            CheckUser = new Command(async () =>
            {
                var resp = await HttpService.GetRequest<ResponseModel<UserModel>>("user");
                if (resp.Response.Workdays == 0)
                {
                    Application.Current.MainPage = new CreateProfile(new CreateProfileViewModel(resp.Response));
                }
            });
        }
    }
}
