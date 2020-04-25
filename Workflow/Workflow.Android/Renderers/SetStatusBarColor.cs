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

using Xamarin.Forms;
using Workflow.Droid.Renderers;
using Workflow.Services;

using Plugin.CurrentActivity;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Android;
using Xamarin.Essentials;
using Xamarin.Forms.Platform.Android;

[assembly:Dependency(typeof(SetStatusBarColor))]
namespace Workflow.Droid.Renderers
{
    public class SetStatusBarColor : ISetStatusBarColor
    {
        void ISetStatusBarColor.SetStatusBarColor(Color color)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                var androidColor = color.ToAndroid();
                //Just use the plugin
                CrossCurrentActivity.Current.Activity.Window.SetStatusBarColor(androidColor);
            }
            else
            {
                // Here you will just have to set your 
                // color in styles.xml file as shown below.
            }
        }
    }
}