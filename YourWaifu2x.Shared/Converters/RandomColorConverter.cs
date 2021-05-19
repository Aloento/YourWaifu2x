using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace YourWaifu2x.Converters
{
    public class RandomColorConverter : IValueConverter
    {
        public enum RngProvider { HashCode, Random }
        public RngProvider Provider { get; set; } = RngProvider.Random;

        public static readonly Random _random = new Random(Guid.NewGuid().GetHashCode());

        private static readonly Color[] _knownColors = new Color[]
        {
            Color.FromArgb(0xFF, 0x6C, 0xE5, 0xAE),
            Color.FromArgb(0xFF, 0x22, 0x9D, 0xFC),
            Color.FromArgb(0xFF, 0x7A, 0x69, 0xF5),
            Color.FromArgb(0xFF, 0xF6, 0x56, 0x78)
        };

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int rng = Provider == RngProvider.HashCode
                ? value?.GetHashCode() ?? 0
                : _random.Next();
            Color color = _knownColors[rng % _knownColors.Length];

            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException("Only one-way conversion is supported.");
        }
    }
}
