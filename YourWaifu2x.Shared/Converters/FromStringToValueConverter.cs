using System;
using Windows.UI.Xaml.Data;

namespace YourWaifu2x.Converters
{
    public class FromStringToValueConverter : IValueConverter
    {
        public enum CheckMethod { IsNullOrEmpty, IsNullOrWhitespace }


        public CheckMethod Check { get; set; }

        public object TrueValue { get; set; }

        public object FalseValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return !(value is string text) || (Check == CheckMethod.IsNullOrEmpty ? string.IsNullOrEmpty(text) : string.IsNullOrWhiteSpace(text))
? TrueValue
: FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException("Only one-way conversion is supported.");
        }
    }
}
