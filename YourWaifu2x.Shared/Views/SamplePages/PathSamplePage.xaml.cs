using Windows.UI.Xaml.Controls;

namespace YourWaifu2x.Views.Samples {
    [Page(PageCategory.Features, "Path", Description = "Draws a series of connected lines and curves. The line and curve dimensions are declared through the Data property, and can be specified either with Move and draw commands syntax, or with an object model.", DocumentationLink = "https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.shapes.path")]
    public sealed partial class PathSamplePage : Page {
        public PathSamplePage() {
            InitializeComponent();
        }
    }
}
