using Windows.UI.Xaml.Controls;

namespace YourWaifu2x.Views.SamplePages {
    [Page(PageCategory.Components, "StandardBottomSheet", SourceSdk.UnoMaterial, Description = "This represents a draggable bottom sheet. Sheet Header, Content and FullScreenHeader are customizable", DocumentationLink = "https://material.io/components/sheets-bottom#standard-bottom-sheet")]
    public sealed partial class StandardBottomSheetSamplePage : Page {
        public StandardBottomSheetSamplePage() {
            InitializeComponent();
        }
    }
}
