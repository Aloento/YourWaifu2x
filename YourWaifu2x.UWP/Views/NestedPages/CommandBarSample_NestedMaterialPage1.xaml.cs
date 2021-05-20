using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace YourWaifu2x.Views.NestedPages {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CommandBarSample_NestedMaterialPage1 : Page {
        public CommandBarSample_NestedMaterialPage1() {
            InitializeComponent();
        }

        private void NavigateToNextPage(object sender, RoutedEventArgs e) {
            Frame.Navigate(typeof(CommandBarSample_NestedMaterialPage2));
        }

        private void NavigateBack(object sender, RoutedEventArgs e) {
            Shell.GetForCurrentView().BackNavigateFromNestedSample();
        }
    }
}
