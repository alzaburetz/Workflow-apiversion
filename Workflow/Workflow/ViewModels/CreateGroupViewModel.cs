using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using Workflow.Models;
using Workflow.Utils;

using Xamarin.Forms;

namespace Workflow.ViewModels
{
    public class CreateGroupViewModel:BaseViewModel
    {
        GroupModel _group;
        public GroupModel Group
        {
            get => _group;
            set
            {
                _group = value;
                OnPropertyChanged("Group");
            }
        }
        string buttonText;
        public string ButtonText
        {
            get => buttonText;
            set
            {
                buttonText = value;
                OnPropertyChanged("ButtonText");
            }
        }
        public Command CreateGroupCommand { get; set; }
        public Command EditGroupCommand { get; set; }
        readonly INavigation Navigation;
        public CreateGroupViewModel(INavigation nav)
        {
            this.Navigation = nav;
            CreateGroupCommand = new Command<GroupModel>((group) =>
            {
                Task.Run(async () =>
                {
                var response = await HttpService.PostRequest<ResponseModel<GroupModel>, GroupModel>("groups/create", group, true);
                if (response.Code == 200)
                {
                    MessagingCenter.Send<CreateGroupViewModel, GroupModel>(this, "AddNewGroup", response.Response);
                    Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
                }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.DisplayAlert("Ошибка", ListStringifyer.StringifyList(response.Errors), "OK"));
                    }
                });
            });

            EditGroupCommand = new Command(() =>
            {
                IsBusy = true;
                Task.Run(async () =>
                {
                    Group.Description = Group.Description.Replace("\n", "\n ");
                    var response = await HttpService.PutRequest<ResponseModel<GroupModel>, GroupModel>($"groups/{Group.ID}/update", Group);
                    IsBusy = false;
                    MessagingCenter.Send<CreateGroupViewModel, GroupModel>(this, "UpdateGroup", Group);
                    Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
                });
            });
        }
    }
}
