using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xamarin.Forms;

namespace Workflow.Converters
{
    public class GroupDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var words = ((string)value).Split(' ');
            FormattedString result = new FormattedString();
            string textblock = string.Empty;
            for (int i = 0; i < words.Length; i++)
            {
                if (!words[i].Contains("https://"))
                {
                    textblock += words[i] + " ";

                }
                else
                {
                    result.Spans.Add(new Span()
                    {
                        Text = textblock
                    });
                    textblock = string.Empty;
                    var span = new Span()
                    {
                        Text = words[i],
                        TextColor = Color.Blue,
                        TextDecorations = TextDecorations.Underline
                    };
                    TapGestureRecognizer openUri = new TapGestureRecognizer()
                    {
                        Command = new Command<string>((url) => Device.OpenUri(new Uri(url.Trim()))),
                        CommandParameter = span.Text
                    };
                    span.GestureRecognizers.Add(openUri);
                    result.Spans.Add(span);
                }
            }
            if (!string.IsNullOrEmpty(textblock))
            {
                result.Spans.Add(new Span() { Text = textblock });
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
