using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Workflow.Services;
using Workflow.Views;

namespace Workflow
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DependencyService.Register<HttpService>();
            DependencyService.Register<ISetStatusBarColor>();
            DependencyService.Register<IFirebaseTokenObtainer>();
            try
            {
                var token = Application.Current.Properties["Token"];
                MainPage = new MainPage();
            }
            catch
            {
                MainPage = new Login();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
