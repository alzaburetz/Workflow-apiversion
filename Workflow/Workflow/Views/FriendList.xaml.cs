using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Workflow.ViewModels;

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
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await CrossPermissions.Current.RequestPermissionsAsync(Permission.Contacts).ContinueWith((obj) =>
            {
                viewModel.GetContacts.Execute(null);
            });
        }
    }
}