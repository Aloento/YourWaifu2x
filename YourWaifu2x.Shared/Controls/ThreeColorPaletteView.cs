namespace YourWaifu2x {
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;

    /// <summary>
    /// This control is used to display a tuples of colors.
    /// </summary>
    public partial class ThreeColorPaletteView : Control {
        public string Title {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ThreeColorPaletteView), new PropertyMetadata(string.Empty));

        public string Description {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(ThreeColorPaletteView), new PropertyMetadata(string.Empty));

        #region First Color Dependency Properties
        public string FirstColorName {
            get => (string)GetValue(FirstColorNameProperty);
            set => SetValue(FirstColorNameProperty, value);
        }

        public static readonly DependencyProperty FirstColorNameProperty =
            DependencyProperty.Register("FirstColorName", typeof(string), typeof(ThreeColorPaletteView), new PropertyMetadata(string.Empty));

        public Brush FirstColorBrush {
            get => (Brush)GetValue(FirstColorBrushProperty);
            set => SetValue(FirstColorBrushProperty, value);
        }

        public static readonly DependencyProperty FirstColorBrushProperty =
            DependencyProperty.Register("FirstColorBrush", typeof(Brush), typeof(ThreeColorPaletteView), new PropertyMetadata(null));

        public Brush FirstColorForeground {
            get => (Brush)GetValue(FirstColorForegroundProperty);
            set => SetValue(FirstColorForegroundProperty, value);
        }

        public static readonly DependencyProperty FirstColorForegroundProperty =
            DependencyProperty.Register("FirstColorForeground", typeof(Brush), typeof(ThreeColorPaletteView), new PropertyMetadata(null));

        #endregion

        #region Second Color Dependency Properties
        public string SecondColorName {
            get => (string)GetValue(SecondColorNameProperty);
            set => SetValue(SecondColorNameProperty, value);
        }

        public static readonly DependencyProperty SecondColorNameProperty =
            DependencyProperty.Register("SecondColorName", typeof(string), typeof(ThreeColorPaletteView), new PropertyMetadata(string.Empty));

        public Brush SecondColorBrush {
            get => (Brush)GetValue(SecondColorBrushProperty);
            set => SetValue(SecondColorBrushProperty, value);
        }

        public static readonly DependencyProperty SecondColorBrushProperty =
            DependencyProperty.Register("SecondColorBrush", typeof(Brush), typeof(ThreeColorPaletteView), new PropertyMetadata(null));

        public Brush SecondColorForeground {
            get => (Brush)GetValue(SecondColorForegroundProperty);
            set => SetValue(SecondColorForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for DarkTextColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SecondColorForegroundProperty =
            DependencyProperty.Register("SecondColorForeground", typeof(Brush), typeof(ThreeColorPaletteView), new PropertyMetadata(null));

        #endregion

        #region Third Color Dependency Properties
        public string ThirdColorName {
            get => (string)GetValue(ThirdColorNameProperty);
            set => SetValue(ThirdColorNameProperty, value);
        }

        public static readonly DependencyProperty ThirdColorNameProperty =
            DependencyProperty.Register("ThirdColorName", typeof(string), typeof(ThreeColorPaletteView), new PropertyMetadata(string.Empty));

        public Brush ThirdColorBrush {
            get => (Brush)GetValue(ThirdColorBrushProperty);
            set => SetValue(ThirdColorBrushProperty, value);
        }

        public static readonly DependencyProperty ThirdColorBrushProperty =
            DependencyProperty.Register("ThirdColorBrush", typeof(Brush), typeof(ThreeColorPaletteView), new PropertyMetadata(null));

        public Brush ThirdColorForeground {
            get => (Brush)GetValue(ThirdColorForegroundProperty);
            set => SetValue(ThirdColorForegroundProperty, value);
        }

        public static readonly DependencyProperty ThirdColorForegroundProperty =
            DependencyProperty.Register("ThirdColorForeground", typeof(Brush), typeof(ThreeColorPaletteView), new PropertyMetadata(null));

        #endregion
    }
}
