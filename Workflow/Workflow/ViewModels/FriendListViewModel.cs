using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

using Plugin.ContactService.Shared;
using Plugin.ContactService;

namespace Workflow.ViewModels
{
    public class FriendListViewModel:BaseViewModel
    {
        public Command GetContacts { get; set; }
        public FriendListViewModel()
        {
            Title = "Список людей";
            GetContacts = new Command(async () =>
            {
                var contacts = await CrossContactService.Current.GetContactListAsync();
            });
        }
    }
}
