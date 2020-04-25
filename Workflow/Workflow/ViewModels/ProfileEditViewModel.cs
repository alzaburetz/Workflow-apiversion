using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Workflow.Models;
using Workflow.Utils;
using Workflow.Views;

using Plugin.Media.Abstractions;
using Plugin.Media;

using Workflow.Services;

namespace Workflow.ViewModels
{
    public class ProfileEditViewModel:BaseViewModel
    {
        public MediaFile Avatar;
        private UserModel user_;
        public UserModel User
        {
            get => user_;
            set
            {
                user_ = value;
                OnPropertyChanged("User");
            }
        }
        public Command UpdateUser { get; set; }
        public Command UploadFile { get; set; }
        INavigation Navigation { get; set; }
        public ProfileEditViewModel(UserModel User, INavigation nav)
        {
            this.Navigation = nav;
            this.User = User;
            UpdateUser = new Command<UserModel>(async (user) =>
            {
                IsBusy = true;
                var resp = await HttpService.PutRequest<ResponseModel<UserModel>, UserModel>("user/update", user);
                if (resp.Code == 200)
                {
                    resp.Response.NextWorkDay = DateTimeOffset.FromUnixTimeSeconds(resp.Response.FirstWork).DateTime;
                    var workstoday = resp.Response.WorksToday();
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
                    user.NextWorkDay = DateTimeOffset.FromUnixTimeSeconds(user.FirstWork).DateTime;
                    user.GraphFormatted = $"{user.Workdays} / {user.Weekdays}";
                    MessagingCenter.Send<ProfileEditViewModel, UserModel>(this, "UpdateUser", resp.Response);
                    MessagingCenter.Send<ProfileEditViewModel, UserModel>(this, "Update", resp.Response);
                    if (Avatar != null)
                    {
                        UploadFile.Execute(null);
                    }
                    else
                    {
                        await Navigation.PopAsync();
                    }
                } 
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Произошла ошибка", Utils.ListStringifyer.StringifyList(resp.Errors), "OK");
                }
                IsBusy = false;
            });

            UploadFile = new Command(async () =>
            {
                var file = System.IO.File.ReadAllBytes(Avatar.AlbumPath);
                var response = await HttpService.UploadFile<ResponseModel<string>>("user/avatar", file);
                if (response.Code != 200)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Application.Current.MainPage.DisplayAlert(response.Code.ToString(), ListStringifyer.StringifyList(response.Errors), "OK");
                        IsBusy = false;
                        return;
                    });
                }
                else
                {
                    await Navigation.PopAsync();
                }
                IsBusy = false;
            });
        }
    }
}
