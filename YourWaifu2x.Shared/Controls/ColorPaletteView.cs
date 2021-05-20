namespace YourWaifu2x {
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;
    using YourWaifu2x.Converters;

    /// <summary>
    /// This controls is used to display a color.
    /// </summary>
    public partial class ColorPaletteView : Control {
        public ColorPaletteView() {
            DefaultStyleKey = typeof(ColorPaletteView);

            ActualThemeChanged += OnThemeChanged;
        }

        private void OnThemeChanged(FrameworkElement sender, object args) {
            ColorHex = FromColorBrushToHexConverter.GetHexName(ColorBrush);
            OnColorHex = FromColorBrushToHexConverter.GetHexName(OnColorBrush);
        }

        public string Title {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ColorPaletteView), new PropertyMetadata(string.Empty));

        public string Description {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(ColorPaletteView), new PropertyMetadata(string.Empty));

        public string ColorName {
            get => (string)GetValue(ColorNameProperty);
            set => SetValue(ColorNameProperty, value);
        }

        public static readonly DependencyProperty ColorNameProperty =
            DependencyProperty.Register("ColorName", typeof(string), typeof(ColorPaletteView), new PropertyMetadata(string.Empty));

        public Brush ColorBrush {
            get => (Brush)GetValue(ColorBrushProperty);
            set => SetValue(ColorBrushProperty, value);
        }

        public static readonly DependencyProperty ColorBrushProperty =
            DependencyProperty.Register("ColorBrush", typeof(Brush), typeof(ColorPaletteView), new PropertyMetadata(null, OnColorBrushChanged));

        private static void OnColorBrushChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args) {
            var that = (ColorPaletteView)dependencyObject;
            that.ColorHex = FromColorBrushToHexConverter.GetHexName(args.NewValue);
        }

        public Brush OnColorBrush {
            get => (Brush)GetValue(OnColorBrushProperty);
            set => SetValue(OnColorBrushProperty, value);
        }

        public static readonly DependencyProperty OnColorBrushProperty =
            DependencyProperty.Register("OnColorBrush", typeof(Brush), typeof(ColorPaletteView), new PropertyMetadata(null, OnOnColorBrushChanged));

        private static void OnOnColorBrushChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args) {
            var that = (ColorPaletteView)dependencyObject;
            that.OnColorHex = FromColorBrushToHexConverter.GetHexName(args.NewValue);
        }

        public double ColorHeight {
            get => (double)GetValue(ColorHeightProperty);
            set => SetValue(ColorHeightProperty, value);
        }

        public static readonly DependencyProperty ColorHeightProperty =
            DependencyProperty.Register("ColorHeight", typeof(double), typeof(ColorPaletteView), new PropertyMetadata(0f));

        public string ColorHex {
            get => (string)GetValue(ColorHexProperty);
            set => SetValue(ColorHexProperty, value);
        }

        public static readonly DependencyProperty ColorHexProperty =
            DependencyProperty.Register("ColorHex", typeof(string), typeof(ColorPaletteView), new PropertyMetadata(null));

        public string OnColorHex {
            get => (string)GetValue(OnColorHexProperty);
            set => SetValue(OnColorHexProperty, value);
        }

        public static readonly DependencyProperty OnColorHexProperty =
            DependencyProperty.Register("OnColorHex", typeof(string), typeof(ColorPaletteView), new PropertyMetadata(null));
    }
}
