using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using YourWaifu2x.Views.NestedPages;

namespace YourWaifu2x.Views.SamplePages {
    [Page(PageCategory.Components, nameof(CommandBar),
        Description = "This control provides navigation and related actions for the current page.",
        DocumentationLink = "https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.commandbar")]
    public sealed partial class CommandBarSamplePage : Page {
        public CommandBarSamplePage() {
            InitializeComponent();
        }

        private void LaunchFullScreenSample(object sender, RoutedEventArgs e) {
            Shell.GetForCurrentView().ShowNestedSample<CommandBarSample_NestedMaterialPage1>(clearStack: true);
        }
    }
}
