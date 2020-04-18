using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Workflow.ViewModels;
using Plugin.Media.Abstractions;
using Plugin.Media;
using Plugin.Permissions;

namespace Workflow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateProfile : ContentPage
    {
        CreateProfileViewModel viewModel { get; set; }
        public CreateProfile()
        {
            InitializeComponent();
        }

        public CreateProfile(CreateProfileViewModel ViewModel) : this()
        {
            this.viewModel = ViewModel;
            BindingContext = this.viewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected async void TakePhoto(object sender, EventArgs args)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
            {
                AllowCropping = true,
                PhotoSize = PhotoSize.Medium
            });
            if (BindingContext == null)
            {
                BindingContext = viewModel = new CreateProfileViewModel();
            }

            if (photo != null)
            {
                viewModel.Avatar = photo;
                Avatar.Source = ImageSource.FromStream(() =>
                {
                    var stream = photo.GetStream();
                    return stream;
                });
            }
        }
        private async void CreateProfilePressed(object sender, EventArgs args)
        {
            if (string.IsNullOrEmpty(Work.Text) || Work.Text.Length < 5)
            {
                await DisplayAlert(null, "Введите данные в правильном формате!", "ОК");
                return;
            }
            var days = Work.Text.Split('/');
            var dayone = days[0].TrimEnd();
            var daytwo = days[1].TrimStart();
            viewModel.User.Workdays = int.Parse(dayone);
            viewModel.User.Weekdays = int.Parse(daytwo);
            viewModel.User.FirstWork = (new DateTimeOffset(FirstDay.Date).ToUnixTimeSeconds());
            viewModel.User.Surname = Surname.Text;

            viewModel.UpdateUser.Execute(null);
        }
    }
}