using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Workflow.Models;

using Plugin.Media.Abstractions;
using Plugin.Media;
using Workflow.Utils;

namespace Workflow.ViewModels
{
    public class CreateProfileViewModel:BaseViewModel
    {
        public UserModel User { get; set; }
        public DateTime Date { get; set; }
        public Command UpdateUser { get; set; }
        public Command UploadPhoto { get; set; }
        public MediaFile Avatar { get; set; }
        public CreateProfileViewModel(UserModel user):this()
        {
            User = user;
        }

        public CreateProfileViewModel()
        {
            Date = DateTime.Now;
            UpdateUser = new Command(async () =>
            {
                IsBusy = true;
                var response = await HttpService.PutRequest<ResponseModel<UserModel>, UserModel>("user/update", User);
                if (response.Code != 200)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Application.Current.MainPage.DisplayAlert(response.Code.ToString(), ListStringifyer.StringifyList(response.Errors), "OK");
                    });
                    IsBusy = false;
                    return;
                }

                if (Avatar != null)
                {
                    UploadPhoto.Execute(null);
                }
                else
                {
                    IsBusy = false;
                    Navigate();
                }
            });

            UploadPhoto = new Command(async () =>
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
                Navigate();
            });
        }

        private void Navigate()
        {
            Application.Current.MainPage = new Workflow.Views.MainPage();
        }
    }
}
