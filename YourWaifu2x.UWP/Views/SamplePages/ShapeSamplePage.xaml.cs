using Windows.UI.Xaml.Controls;

namespace YourWaifu2x.Views.Samples {
    [SamplePage(SampleCategory.Features, "Shape", Description = "Shape is the base class of shape elements such as Ellipse, Rectangle or Path. Shapes are not templatable as they are drawn directly.", DocumentationLink = "https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.shapes.shape")]
    public sealed partial class ShapeSamplePage : Page {
        public ShapeSamplePage() {
            InitializeComponent();
        }
    }
}
