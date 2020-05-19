using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;


using Xamarin.Forms;
using Workflow.Models;

using System.Threading.Tasks;
using Workflow.Utils;

namespace Workflow.ViewModels
{
    public class GroupsViewModel:BaseViewModel
    {
        public Command LoadUserGroups { get; set; }
        public Command LoadAllGroups { get; set; }
        public Command SearchGroups { get; set; }
        public ObservableCollection<GroupModel> AllGroups { get; set; }
        public ObservableCollection<GroupModel> MyGroups { get; set; }
        
        public GroupsViewModel()
        {
            AllGroups = new ObservableCollection<GroupModel>();
            MyGroups = new ObservableCollection<GroupModel>();
            LoadAllGroups = new Command(() =>
            {
                Task.Run(async () =>
                {
                    AllGroups.Clear();
                    var groups = await HttpService.GetRequest<ResponseModel<List<GroupModel>>>("groups");
                    if (groups.Code == 200)
                    {
                        foreach (var group in groups.Response)
                        {
                            Device.BeginInvokeOnMainThread(() => AllGroups.Add(group));
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Application.Current.MainPage.DisplayAlert("Ошибка", ListStringifyer.StringifyList(groups.Errors), "OK");
                        });
                    }
                });
            });

            LoadUserGroups = new Command(() =>
            {
                Task.Run(async () =>
                {
                    IsBusy = true;
                    MyGroups.Clear();
                    var groups = await HttpService.GetRequest<ResponseModel<List<GroupModel>>>("groups/get");
                    if (groups.Code == 200)
                    {
                        if (groups.Response != null)
                        foreach (var group in groups.Response)
                        {
                            Device.BeginInvokeOnMainThread(() => MyGroups.Add(group));
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Application.Current.MainPage.DisplayAlert("Ошибка", ListStringifyer.StringifyList(groups.Errors), "OK");
                        });
                    }
                    IsBusy = false;
                });
            });

            SearchGroups = new Command<string>((query) =>
            {
                if (string.IsNullOrEmpty(query))
                {
                    LoadUserGroups.Execute(null);
                    return;
                }
                IsBusy = true;
                Task.Run(async () =>
                {
                    var search = await HttpService.GetRequest<ResponseModel<List<GroupModel>>>($"groups?search={query}");
                    MyGroups.Clear();
                    if (search.Code == 200)
                    {
                        if (search.Response != null)
                        {
                            foreach (var group in search.Response)
                            {
                                Device.BeginInvokeOnMainThread(() => MyGroups.Add(group));
                            }
                        }
                    }
                    IsBusy = false;
                });
            });
        }
    }
}
