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
using Xamarin.Forms;
using Workflow.Droid;
using System.Threading.Tasks;
using Plugin.CurrentActivity;
using Android.Gms.Auth.Api;
using Android.Content.PM;
using Android.Content.Res;

[assembly:Dependency(typeof(FirebaseAuthentication))]
namespace Workflow.Droid
{
    public class FirebaseAuthentication:IFirebaseAuth
    {
        MainActivity main;
        public async Task<string> Authenticate()
        {
            main = (MainActivity)CrossCurrentActivity.Current.Activity;
            main.SignInClick();
            return ";";
        }
    }
}