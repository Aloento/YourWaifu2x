using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace YourWaifu2x.Views.GeneralPages {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [Page(PageCategory.Theme, "Cupertino Palette", SortOrder = 3, Description = Description)]
    public sealed partial class CupertinoPalettePage : Page {
        private const string Description = "View the Uno palette applied to Cupertino's styles.";

        public CupertinoPalettePage() {
            InitializeComponent();
        }
    }
}
