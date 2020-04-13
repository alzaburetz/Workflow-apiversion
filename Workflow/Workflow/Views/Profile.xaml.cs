using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Workflow.ViewModels;

namespace Workflow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        ProfileViewModel viewModel { get; set; }
        public Profile()
        {
            InitializeComponent();
            BindingContext = viewModel = new ProfileViewModel();
            MessagingCenter.Subscribe<MainPage>(this, "LoadProfile", (obj) =>
            {
                viewModel.GetUserData.Execute(null);
            });
        }

        protected async void EditProfile(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ProfileEdit(new ProfileEditViewModel(viewModel.User, this.Navigation)));
        }
    }
}