using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Workflow.ViewModels;
using Workflow.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace Workflow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendList : ContentPage
    {
        FriendListViewModel viewModel { get; set; }
        public FriendList()
        {
            InitializeComponent();
            BindingContext = viewModel = new FriendListViewModel();
            MessagingCenter.Subscribe<MainPage>(this, "LoadFriends", (c) => viewModel.GetContacts.Execute(null));
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //
        }

        async void AddUserPush(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AddUser());
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var user = e.CurrentSelection[0] as UserModel;
                await Navigation.PushAsync(new CalendarPage(user));

                if ((sender as CollectionView).SelectedItem != null)
                    (sender as CollectionView).SelectedItem = null;
            }
            catch
            {

            }
        }
    }
}