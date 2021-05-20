using Windows.UI.Xaml.Controls;

namespace YourWaifu2x.Views.SamplePages {
    [SamplePage(SampleCategory.Components, "ExpandingBottomSheet", SourceSdk.UnoMaterial, Description = "This control allows users to toggle optional page content.", DocumentationLink = "https://material.io/components/sheets-bottom#expanding-bottom-sheet")]

    public sealed partial class ExpandingBottomSheetSamplePage : Page {
        public ExpandingBottomSheetSamplePage() {
            InitializeComponent();
        }
    }
}
