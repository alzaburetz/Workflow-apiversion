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
        public ProfileViewModel()
        {
            Title = "Профиль";
            GetUserData = new Command(async () =>
            {
                var resp = await HttpService.GetRequest<ResponseModel<UserModel>>("user?search=89157508874");
                User = resp.Response;
                User.GraphFormatted = $"{User.Workdays} / {User.Weekdays}";
            });
        }
    }
}
