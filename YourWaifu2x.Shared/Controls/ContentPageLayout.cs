namespace YourWaifu2x {
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// This control is used as a template for each non-myPage page (like the palette pages).
    /// </summary>
    public partial class ContentPageLayout : ContentControl {
        public string Title {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ContentPageLayout), new PropertyMetadata(null));

        public string Description {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(ContentPageLayout), new PropertyMetadata(null));
    }
}
