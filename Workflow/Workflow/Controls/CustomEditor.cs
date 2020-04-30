using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace Workflow.Controls
{
    public class CustomEditor:Editor
    {
        public int CornerRadius { get; set; }
        public int Padding { get; set; }
        public Color Background { get; set; }
        public CustomEditor()
        {
            if (Background == null)
            {
                Background = Color.White;
            }
        }
    }
}
