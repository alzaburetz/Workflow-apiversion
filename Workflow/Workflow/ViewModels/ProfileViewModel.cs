using System;
using System.Collections.Generic;
using System.Text;

using Workflow.Models;
using Xamarin.Forms;

namespace Workflow.ViewModels
{
    public class ProfileViewModel:BaseViewModel
    {
        public Command GetUserData { get; set; }
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
        public bool GoogleLogin
        {
            get
            {
                try
                {
                    var ggl = Application.Current.Properties["GoogleLogin"];
                    return Convert.ToBoolean(ggl);
                } 
                catch 
                {
                    return false;
                }
            }
                
        }
        public ProfileViewModel()
        {
            Title = "Профиль";
            GetUserData = new Command(async () =>
            {
                IsBusy = true;
                var resp = await HttpService.GetRequest<ResponseModel<UserModel>>("user");
                User = resp.Response;
                User.GraphFormatted = $"{User.Workdays} / {User.Weekdays}";
                User.NextWorkDay = DateTimeOffset.FromUnixTimeSeconds(User.FirstWork).DateTime;
                IsBusy = false;
            });
        }
    }
}
