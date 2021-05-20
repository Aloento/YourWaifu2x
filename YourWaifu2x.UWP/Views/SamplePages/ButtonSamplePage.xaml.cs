using Windows.UI.Xaml.Controls;

namespace YourWaifu2x.Views.Samples {
    [SamplePage(SampleCategory.Components, "Button", Description = Description, DocumentationLink = "https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/buttons")]
    public sealed partial class ButtonSamplePage : Page {
        private const string Description = "A button is used to interpret a user click or tap interaction. They're often bound to commands.";

        public ButtonSamplePage() {
            InitializeComponent();
        }
    }
}
