using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Xamarin.Forms;

using Plugin.ContactService.Shared;
using Plugin.ContactService;
using System.Linq;

using Workflow.Models;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Workflow.ViewModels
{
    public class FriendListViewModel:BaseViewModel
    {
        bool _refreshing;
        public bool Refreshing
        {
            get => _refreshing;
            set
            {
                _refreshing = value;
                OnPropertyChanged("Refreshing");
            }
        }
        public Command GetContacts { get; set; }
        public Command Update { get; set; }
        public ObservableCollection<UserModel> FoundUsers { get; set; }
        public FriendListViewModel()
        {
            Title = "Список людей";
            FoundUsers = new ObservableCollection<UserModel>();
            Update = new Command(() =>
            {
                Refreshing = true;
                GetContacts.Execute(null);
            });
            GetContacts = new Command(async () =>
            {
                await Task.Run(async () =>
                {
                    IsBusy = true;
                    FoundUsers.Clear();
                    var contacts = await CrossContactService.Current.GetContactListAsync();
                    var numbers = contacts.SelectMany(x => x.Numbers).ToList();
                    for (int i = 0; i < numbers.Count; i++)
                    {
                        var number = numbers[i];
                        numbers[i] = Regex.Replace(number.Replace("+7", "8"), @"\D", "");
                    }
                    var resp = await HttpService.PostRequest<ResponseModel<List<UserModel>>, List<string>>("user/find", numbers,true);
                    if (resp.Response != null)
                        foreach (var user in resp.Response)
                        {
                            user.NextWorkDay = DateTimeOffset.FromUnixTimeSeconds(user.FirstWork).DateTime;
                            user.Workstoday = user.WorksToday();
                            Device.BeginInvokeOnMainThread(() => FoundUsers.Add(user));
                        }
                    IsBusy = false;
                    Refreshing = false;
                });
                
            });
        }
    }
}
