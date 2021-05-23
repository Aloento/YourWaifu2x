namespace YourWaifu2x.Converters {
    using System;
    using System.IO;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Use this converter to debug data bindings in your xaml.
    /// </summary>
    public class PathToNameConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) =>
            Path.GetFileName(value as string);

        public object ConvertBack(object value, Type targetType, object parameter, string language) =>
        // Put a breakpoint here to inspect values from the View to the ViewModel.
        value;
    }
}
