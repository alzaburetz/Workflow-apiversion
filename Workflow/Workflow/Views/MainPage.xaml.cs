using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Workflow.ViewModels;

namespace Workflow.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        MainPageViewModel viewModel { get; set; }
        public MainPage()
        {
            BindingContext = viewModel = new MainPageViewModel();
            InitializeComponent();
            Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetToolbarPlacement(this, Xamarin.Forms.PlatformConfiguration.AndroidSpecific.ToolbarPlacement.Bottom);
        }
        protected override void OnAppearing()
        {
            viewModel.CheckUser.Execute(null);
            base.OnAppearing();
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            var i = this.Children.IndexOf(this.CurrentPage);
            switch (i)
            {
                case 0: break;
                case 1: MessagingCenter.Send<MainPage>(this, "LoadProfile");
                    break;
            }
        }
    }
}