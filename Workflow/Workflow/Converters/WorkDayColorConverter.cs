using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xamarin.Forms;

namespace Workflow.Converters
{
    public class WorkDayColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isWorkingDay = (bool)value;
            if (isWorkingDay)
            {
                return Color.FromHex("#fa5252");
            }
            return Color.FromHex("#20a830");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
