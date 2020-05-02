using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Workflow.Models;
using Workflow.Views;
using Workflow.Services;

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
                var token = DependencyService.Get<IFirebaseTokenObtainer>().GetFirebaseToken();
                var resp = await HttpService.GetRequest<ResponseModel<UserModel>>("user");
                resp.Response.NextWorkDay = DateTimeOffset.FromUnixTimeSeconds(resp.Response.FirstWork).UtcDateTime;
                if (resp.Response.Workdays == 0)
                {
                    Application.Current.MainPage = new CreateProfile(new CreateProfileViewModel(resp.Response));
                }
                else
                {
                    User = resp.Response;
                    var workstoday = User.WorksToday();
                    Color color = Color.Black;
                    if (workstoday)
                    {
                        color = Color.FromHex("#ff6161");
                    }
                    else
                    {
                        color = Color.FromHex("#237547");
                    }
                    DependencyService.Get<ISetStatusBarColor>().SetStatusBarColor(color);
                    App.Current.Resources["DarkColor"] = color;
                    MessagingCenter.Send<MainPageViewModel, UserModel>(this, "SetUser", resp.Response);
                    if (User.Push != token)
                    {
                        User.Push = token;
                        System.Threading.Tasks.Task.Run(async () =>
                        {
                            await HttpService.PutRequest<ResponseModel<UserModel>, UserModel>("user/update", this.User);
                        });
                    }
                }
            });
        }
    }
}
