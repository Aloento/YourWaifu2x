using Windows.UI.Xaml.Controls;

namespace YourWaifu2x.Views.Samples {
    [Page(PageCategory.Components, "ElevatedView", Description = Description, DocumentationLink = "https://platform.uno/docs/articles/features/ElevatedView.html")]
    public sealed partial class ElevatedViewSamplePage : Page {
        private const string Description = "ElevatedView component allow to highlight the different levels of layout";

        public ElevatedViewSamplePage() {
            InitializeComponent();
        }
    }
}
