using Windows.UI.Xaml.Controls;

namespace YourWaifu2x.Views.Samples {
#if !__WASM__ && !__MACOS__
    [SamplePage(SampleCategory.Features, "FlipView", Description = "This control is used to show a collection of items, one item at a time. You have to \"flip\" to through the items.", DocumentationLink = "https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.flipview")]
#endif
    public sealed partial class FlipViewSamplePage : Page {
        public FlipViewSamplePage() {
            InitializeComponent();
        }
    }
}
