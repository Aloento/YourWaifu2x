namespace YourWaifu2x.Views.GeneralPages {
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Entities.Data;

    [Page(PageCategory.None, "Setting Waifu2x")]
    public sealed partial class SettingWaifu2x {
        private static readonly WaifuConfig Config = new WaifuConfig();

        public SettingWaifu2x() => InitializeComponent();

        private void Setting_OnLoaded(object sender, RoutedEventArgs e) {

            var nextCommand = new StandardUICommand(StandardUICommandKind.Forward);
            nextCommand.ExecuteRequested += (command, args) =>
                (Application.Current as App)?.ShellNavigateTo(App.FindMyPage<Exporting>());
            NextButton.Command = nextCommand;

        }

        private void NoiseCBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            throw new System.NotImplementedException();
        }

        private void ScaleCBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            throw new System.NotImplementedException();
        }

        private void FormatCBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            throw new System.NotImplementedException();
        }

        private void ModelRB_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            throw new System.NotImplementedException();
        }

        private void CpuToggle_OnToggled(object sender, RoutedEventArgs e) {
            throw new System.NotImplementedException();
        }

        private void TtaToggle_OnToggled(object sender, RoutedEventArgs e) {
            throw new System.NotImplementedException();
        }
    }
}
