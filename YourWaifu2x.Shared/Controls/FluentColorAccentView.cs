namespace YourWaifu2x {
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Markup;

    [ContentProperty(Name = nameof(DescriptiveContent))]
    public partial class FluentColorAccentView : Control {
        public FluentColorAccentView() => DefaultStyleKey = typeof(FluentColorAccentView);

        public string ColorName {
            get => (string)GetValue(ColorNameProperty);
            set => SetValue(ColorNameProperty, value);
        }

        public static readonly DependencyProperty ColorNameProperty =
            DependencyProperty.Register("ColorName", typeof(string), typeof(FluentColorAccentView), new PropertyMetadata(string.Empty));

        public DataTemplate DescriptiveContent {
            get => (DataTemplate)GetValue(DescriptiveContentProperty);
            set => SetValue(DescriptiveContentProperty, value);
        }

        public static readonly DependencyProperty DescriptiveContentProperty =
            DependencyProperty.Register("DescriptiveContent", typeof(DataTemplate), typeof(FluentColorAccentView), new PropertyMetadata(null));
    }
}
