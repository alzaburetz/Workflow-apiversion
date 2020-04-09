using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using System.Globalization;

namespace Workflow.Converters
{
    public class TimeStampToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long timestamp = 0;
            long.TryParse(value.ToString(), out timestamp);
            if (timestamp > 0)
            {
                return DateTimeOffset.FromUnixTimeSeconds(timestamp).ToString("dd.MM.yyyy");
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
