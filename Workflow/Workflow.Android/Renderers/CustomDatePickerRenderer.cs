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

using Workflow.Controls;
using Workflow.Droid.Renderers;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

[assembly:ExportRenderer(typeof(CustomDatePicker),typeof(CustomDatePickerRenderer))]
namespace Workflow.Droid.Renderers
{
    public class CustomDatePickerRenderer:DatePickerRenderer
    {
        public CustomDatePickerRenderer(Context context):base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                this.Control.SetBackgroundColor(Xamarin.Forms.Color.Transparent.ToAndroid());
                this.Control.SetHintTextColor(Xamarin.Forms.Color.White.ToAndroid());
                this.Control.SetTextColor(Xamarin.Forms.Color.White.ToAndroid());
            }
        }
    }
}