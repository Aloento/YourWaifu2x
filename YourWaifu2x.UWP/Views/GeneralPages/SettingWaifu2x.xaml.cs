namespace YourWaifu2x.Views.GeneralPages {
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Entities.Data;

    [Page(PageCategory.None, "Setting Waifu2x")]
    public sealed partial class SettingWaifu2x {
        public SettingWaifu2x() => InitializeComponent();

        private void Setting_OnLoaded(object sender, RoutedEventArgs e) {

            var nextCommand = new StandardUICommand(StandardUICommandKind.Forward);
            nextCommand.ExecuteRequested += (command, args) =>
                (Application.Current as App)?.ShellNavigateTo(App.FindMyPage<Exporting>());
            NextButton.Command = nextCommand;

        }

        private void NoiseCBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) =>
            WaifuInstance.Config.Noise = Convert.ToInt32(e.AddedItems[0]);

        private void ScaleCBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) =>
            WaifuInstance.Config.Scale = Convert.ToInt32(e.AddedItems[0]);

        private void FormatCBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) =>
            WaifuInstance.Config.Format = e.AddedItems[0].ToString().ToLower();

        private void ModelRB_OnSelectionChanged(object sender, SelectionChangedEventArgs e) =>
            WaifuInstance.Config.Model = "Native/models/" + e.AddedItems[0];

        private void CpuToggle_OnToggled(object sender, RoutedEventArgs e) {
            if (!(sender is ToggleSwitch toggle))
                return;
            WaifuInstance.Config.Gpu = toggle.IsOn ? new IntVector { -1 } : null;
        }

        private void TtaToggle_OnToggled(object sender, RoutedEventArgs e) {
            if (!(sender is ToggleSwitch toggle))
                return;
            WaifuInstance.Config.TtaMode = toggle.IsOn ? 1 : 0;
        }
    }
}
