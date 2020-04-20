using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xamarin.Forms;

namespace Workflow.Converters
{
    public class StatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val = (bool)value;
            if (val)
            {
                Color pink = Color.FromHex(Application.Current.Resources["PinkColor"].ToString());
                return pink;
            }
            else
            {
                Color green = Color.FromHex(Application.Current.Resources["MainColor"].ToString());
                return green;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
