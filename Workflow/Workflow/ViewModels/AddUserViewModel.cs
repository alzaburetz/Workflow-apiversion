using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Workflow.Models;
using Workflow.Utils;

namespace Workflow.ViewModels
{
    public class AddUserViewModel:BaseViewModel
    {
        public UserModel NewUser { get; set; }
        readonly INavigation Navigation;
        public Command AddNewUserCommand { get; set; }
        DateTime date;
        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        public AddUserViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Date = DateTime.Now;
            AddNewUserCommand = new Command<UserModel>(async (user) =>
            {
                IsBusy = true;
                if (user != null)
                {
                    if (user.Name.Length == 0 || user.Surname.Length == 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Заполните данные", "Введите имя и фамилию для добавления пользователя", "ОК");
                        return;
                    }
                    if (user.Workdays == 0 || user.Weekdays == 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Заполните данные", "Введите корректный график для добавления пользователя", "ОК");
                        return;
                    }

                    if (string.IsNullOrEmpty(user.Email))
                    {
                        user.Email = "temp@email";
                    }
                    if (user.Phone.Length == 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Заполните данные", "Введите телефон для добавления пользователя", "ОК");
                        return;
                    }

                    var resp = await HttpService.PostRequest<ResponseModel<UserModel>, UserModel>("user/add", user, true);
                    if (resp.Code == 200)
                    {
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Произошла ошибка", ListStringifyer.StringifyList(resp.Errors), "ОК");
                        return;
                    }
                }
            });
        }
    }
}
