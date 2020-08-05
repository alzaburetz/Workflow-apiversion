using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Workflow.Services;
using Workflow.Droid;
using Xamarin.Forms;
using System.Threading.Tasks;
using VKontakte;
using Plugin.CurrentActivity;

[assembly:Dependency(typeof(VkAuthentication))]
namespace Workflow.Droid
{
    public class VkAuthentication : Java.Lang.Object, IVkAuth
    {
        public void Login()
        {
            VKSdk.Login(CrossCurrentActivity.Current.Activity, new string[] { VKScope.Email });
        }
    }
}