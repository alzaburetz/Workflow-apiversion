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
using System.ComponentModel;

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
                this.Control.SetBackgroundColor(Xamarin.Forms.Color.Transparent.ToAndroid());
                this.Control.SetTextColor(Xamarin.Forms.Color.White.ToAndroid());
            }
        }
    }
}