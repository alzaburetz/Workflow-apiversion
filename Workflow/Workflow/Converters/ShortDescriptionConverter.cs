using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xamarin.Forms;

namespace Workflow.Converters
{
    public class ShortDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var words = value.ToString().Split(' ');
            var shortentext = string.Empty;
            int i = 0;
            do
            {
                shortentext += words[i] + " ";
                i++;    
            } while (shortentext.Length < 100 && i < words.Length);

            var conv = (new GroupDescriptionConverter()).Convert(shortentext, typeof(FormattedString), new object());
            return conv;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
