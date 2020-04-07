using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Xamarin.Forms;

using Plugin.ContactService.Shared;
using Plugin.ContactService;
using System.Linq;

using Workflow.Models;

namespace Workflow.ViewModels
{
    public class FriendListViewModel:BaseViewModel
    {
        public Command GetContacts { get; set; }
        public ObservableCollection<UserModel> FoundUsers { get; set; }
        public FriendListViewModel()
        {
            Title = "Список людей";
            GetContacts = new Command(async () =>
            {
                IsBusy = true;
                var contacts = await CrossContactService.Current.GetContactListAsync();
                var numbers = contacts.Select(x => x.Number).ToList();
                var resp = await HttpService.GetRequestWithBody<ResponseModel<List<UserModel>>, List<string>>("user/find",numbers);
            });
        }
    }
}
