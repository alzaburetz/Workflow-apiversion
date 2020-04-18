using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Workflow.ViewModels;

using Plugin.Media;
using Plugin.Media.Abstractions;

namespace Workflow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileEdit : ContentPage
    {
        ProfileEditViewModel viewModel { get; set; }
        public ProfileEdit()
        {
            InitializeComponent();
            Datepicker.DateSelected += (s, e) =>
            {
                viewModel.User.NextWorkDay = e.NewDate;
                viewModel.User.FirstWork = new DateTimeOffset(e.NewDate).ToUnixTimeSeconds();
            };
        }

        public ProfileEdit(ProfileEditViewModel viewmodel) : this()
        {
            BindingContext = viewModel = viewmodel;
        }

        private void EditProfile(object sender, EventArgs args)
        {
            if (this.Schedule.Text.Length > 4)
            {
                var schedule = Schedule.Text.Split('/');
                this.viewModel.User.Workdays = int.Parse(schedule[0].Trim());
                this.viewModel.User.Weekdays = int.Parse(schedule[1].Trim());

                viewModel.UpdateUser.Execute(this.viewModel.User);
            }
            else
            {
                DisplayAlert(null, "Введите правильные данные!", "OK");
            }
        }

        private async void TakePhoto(object sender, EventArgs args)
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
    }
}