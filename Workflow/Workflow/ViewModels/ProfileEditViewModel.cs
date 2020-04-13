using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Workflow.Models;
using Workflow.Utils;

namespace Workflow.ViewModels
{
    public class ProfileEditViewModel:BaseViewModel
    {
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
        INavigation Navigation { get; set; }
        public ProfileEditViewModel(UserModel User, INavigation nav)
        {
            this.Navigation = nav;
            this.User = User;
            UpdateUser = new Command<UserModel>(async (user) =>
            {
                IsBusy = true;
                var resp = await HttpService.PutRequest<ResponseModel<UserModel>, UserModel>("user/update", user);
                IsBusy = false;
                if (resp.Code == 200)
                {
                    await Navigation.PopAsync();
                } 
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Произошла ошибка", Utils.ListStringifyer.StringifyList(resp.Errors), "OK");
                }
            });
        }
    }
}
