using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace Workflow.Controls
{
    public class GradientButton:Button
    {
        public BindableProperty StartColor { get; set; }
        public BindableProperty EndColor { get; set; }
        public Color Start { get; set; }
        public Color End { get; set; }
        public GradientButton() : base()
        {
            StartColor = BindableProperty.Create("StartColor", typeof(Color), typeof(Color), Color.FromHex("#2fa362"));
            EndColor = BindableProperty.Create("EndColor", typeof(Color), typeof(Color), Color.FromHex("#237547"));
            Start = (Color)GetValue(StartColor);
            End = (Color)GetValue(EndColor);
        }
    }
}
