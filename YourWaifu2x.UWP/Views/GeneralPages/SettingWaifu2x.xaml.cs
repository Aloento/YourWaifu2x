namespace YourWaifu2x.Views.GeneralPages {
    using Windows.UI.Xaml;
    using Entities.Data;

    [Page(PageCategory.None, "Setting Waifu2x")]
    public sealed partial class SettingWaifu2x {
        private static readonly WaifuConfig Config = new WaifuConfig();

        public SettingWaifu2x() => InitializeComponent();

        private void Setting_OnLoaded(object sender, RoutedEventArgs e) {

        }
    }
}
