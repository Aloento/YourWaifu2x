using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace YourWaifu2x.Converters
{
    public class FromNullToValueConverter : IValueConverter
    {
        public object NullValue { get; set; }

        public object NotNullValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value == null || value == DependencyProperty.UnsetValue)
? NullValue
: NotNullValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException("Only one-way conversion is supported.");
        }
    }
}
