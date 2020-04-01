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
using Xamarin.Forms.Platform.Android;
using Workflow.Droid.Renderers;

using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Text;

[assembly:ExportRenderer(typeof(Entry), typeof(WorkFlowEntryRenderer))]
namespace Workflow.Droid.Renderers
{
    public class WorkFlowEntryRenderer:EntryRenderer
    {
        public WorkFlowEntryRenderer(Context context) : base(context) { }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var gradient = new GradientDrawable(GradientDrawable.Orientation.TopBottom, new int[]
                {
                    Xamarin.Forms.Color.FromHex("#d6d6d6").ToAndroid(),
                    Xamarin.Forms.Color.FromHex("#e6e6e6").ToAndroid()
                });
                gradient.SetCornerRadius(40);
                this.Control.SetBackgroundDrawable(gradient);
                this.Control.SetHintTextColor(Xamarin.Forms.Color.FromHex("#237547").ToAndroid());
                Rect rect = new Rect();
                this.Control.GetHitRect(rect);
                rect.Left -= 40;
            }
        }
    }
}