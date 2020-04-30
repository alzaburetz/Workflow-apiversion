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
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Android.Graphics.Drawables;
using Xamarin.Forms;

[assembly:ExportRenderer(typeof(CustomEditor),typeof(CustomEditorRenderer))]
namespace Workflow.Droid.Renderers
{
    public class CustomEditorRenderer:EditorRenderer
    {
        public CustomEditorRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var padding = (e.NewElement as CustomEditor).Padding;
                var corner = (e.NewElement as CustomEditor).CornerRadius;
                var background = (e.NewElement as CustomEditor).Background;

                var bg = new GradientDrawable(GradientDrawable.Orientation.BottomTop, new int[]
                {
                    background.ToAndroid(),
                    background.ToAndroid()
                });
                bg.SetCornerRadius(corner);

                Control.SetBackground(bg);
                Control.SetPadding(padding, padding, padding, padding);
                Control.SetTextColor(e.NewElement.TextColor.ToAndroid());
            }
        }
    }
}