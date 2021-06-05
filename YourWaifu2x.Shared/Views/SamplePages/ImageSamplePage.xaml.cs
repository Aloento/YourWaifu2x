using Windows.UI.Xaml.Controls;

namespace YourWaifu2x.Views.Samples {
    [Page(PageCategory.Features, "Image", Description = "Represents a control that displays an image. The image source is specified by referring to an image file, using several supported formats. The image source can also be set with a stream.", DocumentationLink = "https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/images-imagebrushes")]
    public sealed partial class ImageSamplePage : Page {
        public ImageSamplePage() {
            InitializeComponent();
        }
    }
}
