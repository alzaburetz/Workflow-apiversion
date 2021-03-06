﻿using System;
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
                if (resp.Response.Workdays == 0 && !string.IsNullOrEmpty(resp.Response.Email))
                {
                    Application.Current.MainPage = new CreateProfile(new CreateProfileViewModel(resp.Response));
                }
                else if (string.IsNullOrEmpty(resp.Response.Email))
                {
                    Application.Current.MainPage = new Login();
                }
                else
                {
                    User = resp.Response;
                    var now = DateTime.Now;
                    try
                    {
                        var workstoday = User.Schedule.Find(x => x.Month == now.Month && x.DayOfMonth == now.Day).Workday;
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
                    }
                    catch { }
                    MessagingCenter.Send<MainPageViewModel, UserModel>(this, "SetUser", resp.Response);
                    if (Application.Current.Properties.ContainsKey("email"))
                    {
                        Application.Current.Properties["email"] = User.Email;
                    }
                    else
                    {
                        Application.Current.Properties.Add("email", User.Email);
                    }
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
