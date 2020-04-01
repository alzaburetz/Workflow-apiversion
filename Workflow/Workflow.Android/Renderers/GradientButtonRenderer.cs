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
using Android.Graphics.Drawables;
using Workflow.Controls;
using Workflow.Droid.Renderers;

using Android.Support.V7.Widget;

using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(GradientButton), typeof(GradientButtonRenderer))]
namespace Workflow.Droid.Renderers
{
    public class GradientButtonRenderer: Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {
        private Color StartColor { get; set; }
        private Color EndColor { get; set; }
        public GradientButtonRenderer(Context context):base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            var newEl = e.NewElement as GradientButton;
            if (newEl != null)
            {
                var gradient = new GradientDrawable(GradientDrawable.Orientation.TopBottom, new int[]
                {
                    newEl.Start.ToAndroid(),
                    newEl.End.ToAndroid()
                });
                var height = Control.MinHeight;
                gradient.SetCornerRadius((float)height / 2);
                Control.Background = gradient;
            }
        }

        protected override AppCompatButton CreateNativeControl()
        {
            var button = base.CreateNativeControl();
            button.SetAllCaps(false);
            button.SetTextColor(Android.Graphics.Color.White);
            button.SetValue(Xamarin.Forms.Button.CornerRadiusProperty, 20);
            return button;
        }
    }
}