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

using Firebase.Iid;

[assembly:Dependency(typeof(FirebaseTokenObtainer))]
namespace Workflow.Droid
{
    public class FirebaseTokenObtainer : IFirebaseTokenObtainer
    {
        public string GetFirebaseToken()
        {
            return Firebase.Iid.FirebaseInstanceId.Instance.Token;
        }
    }
}