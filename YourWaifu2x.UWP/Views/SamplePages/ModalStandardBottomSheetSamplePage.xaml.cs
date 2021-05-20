using Windows.UI.Xaml.Controls;

namespace YourWaifu2x.Views.SamplePages {
    [SamplePage(SampleCategory.Components, "ModalStandardBottomSheet", SourceSdk.UnoMaterial, Description = "This represents a draggable, modal bottom sheet. Sheet Content and FullScreenHeader are customizable", DocumentationLink = "https://material.io/components/sheets-bottom#modal-bottom-sheet")]
    public sealed partial class ModalStandardBottomSheetSamplePage : Page {
        public ModalStandardBottomSheetSamplePage() {
            InitializeComponent();
        }
    }
}
