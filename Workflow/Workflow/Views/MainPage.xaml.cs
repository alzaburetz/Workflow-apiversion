using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Workflow.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetToolbarPlacement(this, Xamarin.Forms.PlatformConfiguration.AndroidSpecific.ToolbarPlacement.Bottom);
        }
        protected override void OnAppearing()
        {
            var token = Application.Current.Properties["Token"];
            if (token == null)
            {

            }
            base.OnAppearing();
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
        }
    }
}