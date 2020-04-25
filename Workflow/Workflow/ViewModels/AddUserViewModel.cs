using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Xamarin.Forms;
using Workflow.Models;
using Workflow.Utils;

using Plugin.ContactService;
using Plugin.ContactService.Shared;

using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Linq;

namespace Workflow.ViewModels
{
    public class AddUserViewModel : BaseViewModel
    {
        IList<Contact> Contacts { get; set; }
        public UserModel NewUser { get; set; }
        readonly INavigation Navigation;
        Entry phone { get; set; }
        public Command AddNewUserCommand { get; set; }
        public Command LoadContactsCommand { get; set; }
        public Command FilterContacts { get; set; }
        public ObservableCollection<Contact> FoundContacts { get; set; }
        bool searching;
        public bool Searching
        {
            get => searching;
            set
            {
                searching = value;
                OnPropertyChanged("Searching");
            }
        }
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

        public AddUserViewModel(INavigation navigation, Entry phon)
        {
            this.phone = phon;
            Navigation = navigation;
            Date = DateTime.Now;
            FoundContacts = new ObservableCollection<Contact>();
            FilterContacts = new Command<string>((phone) =>
            {
                FoundContacts.Clear();
                Searching = true;
                var ctcts = Contacts.Where(x => x.Numbers.Find(y => y.Contains(phone)) != null).GetEnumerator();
                while (ctcts.MoveNext())
                {
                    ctcts.Current.PhotoUri = string.Empty;
                    ctcts.Current.PhotoUriThumbnail = string.Empty;
                    FoundContacts.Add(ctcts.Current);
                }

            });
            LoadContactsCommand = new Command(() =>
            {
                Task.Run(async () =>
                {
                    Contacts = await CrossContactService.Current.GetContactListAsync();
                    foreach (var contact in Contacts)
                    {
                        for (int i = 0; i < contact.Numbers.Count; i++)
                        {
                            contact.Numbers[i] = Regex.Replace(contact.Numbers[i].Replace("+7", "8"), @"\D", "");
                        }
                    }
                }).ContinueWith((res) => Device.BeginInvokeOnMainThread(() => phone.Text = "+7"));
            });
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
