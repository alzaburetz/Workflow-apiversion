using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xamarin.Forms;
using System.Text.RegularExpressions;

namespace Workflow.Converters
{
    public class GroupDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter = null, CultureInfo culture = null)
        {
            var words = ((string)value).Split(' ');
            FormattedString result = new FormattedString();
            string textblock = string.Empty;
            Regex regex = new Regex(@"\(\w+\)\[http://\S+]|\(\w+\)\[https://\S+]");
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
                    if (regex.IsMatch(words[i]))
                    {
                        var a = words[i];
                        string linkname = a.Substring(1, a.IndexOf(')') - 1);
                        string link = a.Substring(a.IndexOf('[') + 1, a.IndexOf(']') - a.IndexOf('[') - 1);
                        var span1 = new Span()
                        {
                            Text = linkname,
                            TextColor = parameter == null ? Color.Blue : Color.Default,
                            TextDecorations = parameter == null ? TextDecorations.Underline : TextDecorations.None
                        };
                        if (parameter == null)
                        {
                            TapGestureRecognizer openUri1 = new TapGestureRecognizer()
                            {
                                Command = new Command<string>((url) => Device.OpenUri(new Uri(url.Trim()))),
                                CommandParameter = link
                            };
                            span1.GestureRecognizers.Add(openUri1);
                        }
                        
                        result.Spans.Add(span1);
                        continue;
                    }
                    var span = new Span()
                    {
                        Text = words[i],
                        TextColor = parameter == null ? Color.Blue : Color.Default,
                        TextDecorations = parameter == null ? TextDecorations.Underline : TextDecorations.None
                    };
                    if (parameter == null)
                    {
                        TapGestureRecognizer openUri = new TapGestureRecognizer()
                        {
                            Command = new Command<string>((url) => Device.OpenUri(new Uri(url.Trim()))),
                            CommandParameter = span.Text
                        };
                        span.GestureRecognizers.Add(openUri);
                    }
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
