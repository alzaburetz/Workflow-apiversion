using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace Workflow.Controls
{
    public class GradientBox:BoxView
    {
        public Color StartColor { get; set; }
        public Color EndColor { get; set; }
        public GradientBox()
        {
            if (StartColor == null)
            {
                StartColor = Color.FromHex("#d6d6d6");
            }

            if (EndColor == null)
            {
                EndColor = Color.White;
            }
        }
    }
}
