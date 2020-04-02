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
using Workflow.Controls;
using Xamarin.Forms.Platform.Android;

using Android.Graphics.Drawables;

[assembly:ExportRenderer(typeof(GradientBox), typeof(GradientBoxRenderer))]
namespace Workflow.Droid.Renderers
{
    public class GradientBoxRenderer: BoxRenderer
    {
        Color StartColor { get; set; }
        Color EndColor { get; set; }
        public GradientBoxRenderer(Context context):base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                this.StartColor = (e.NewElement as GradientBox).StartColor;
                this.EndColor = (e.NewElement as GradientBox).EndColor;

            }


        }

        protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
        {
            #region for Vertical Gradient
            //var gradient = new Android.Graphics.LinearGradient(0, 0, 0, Height,
            #endregion

            #region for Horizontal Gradient
            var gradient = new Android.Graphics.LinearGradient(Width / 2, 0, Width / 2, Height,
            #endregion

                   this.StartColor.ToAndroid(),
                   this.EndColor.ToAndroid(),
                   Android.Graphics.Shader.TileMode.Mirror);

            var paint = new Android.Graphics.Paint()
            {
                Dither = true,
            };
            paint.SetShader(gradient);
            canvas.DrawPaint(paint);
            base.DispatchDraw(canvas);
        }
    }
}