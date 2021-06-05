using Windows.UI.Xaml.Controls;

namespace YourWaifu2x {
    [Page(PageCategory.Theme, "Material Palette", SortOrder = 1, Description = Description)]
    public sealed partial class MaterialPalettePage : Page {
        private const string Description = "View the Uno palette adapted to Material's styles.";

        public MaterialPalettePage() {
            InitializeComponent();
        }
    }
}
