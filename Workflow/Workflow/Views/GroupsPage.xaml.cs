using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Workflow.ViewModels;
using Workflow.Models;

namespace Workflow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupsPage : ContentPage
    {
        GroupsViewModel viewModel { get; set; }
        UserModel User { get; set; }
        public GroupsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new GroupsViewModel();
            MessagingCenter.Subscribe<MainPage>(this, "LoadGroups", (sender) =>
            {
                viewModel.LoadUserGroups.Execute(null);
            });
            MessagingCenter.Subscribe<MainPageViewModel, UserModel>(this, "SetUser", (sender, user) =>
            {
                User = user;
            });
            MessagingCenter.Subscribe<Object, string>(this, "RemoveGroupID", (sender, id) => User.Groups.Remove(id));
            MessagingCenter.Subscribe<Object, string>(this, "AddGroupID", (sender, id) =>
            {
                User.Groups.Add(id);
                try
                {
                    viewModel.MyGroups.Where(x => x.ID == id).SingleOrDefault().UserCount++;
                }
                catch { }
            });
        }

        async void CreateGroup(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new CreateGroupPage(this.Navigation));
        }

        async void OpenGroup(object sender, SelectionChangedEventArgs args)
        {
            try
            {
                await Navigation.PushAsync(new GroupPage(new GroupPageViewModel(args.CurrentSelection[0] as GroupModel, User), true));
                ((CollectionView)sender).SelectedItem = null;
                MessagingCenter.Send<GroupsPage>(this, "LoadPosts");
            }
            catch
            {

            }
        }
    }
}