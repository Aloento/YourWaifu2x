namespace YourWaifu2x.Views.MyPages {
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;

    [Page(PageCategory.None, "Overview")]
    public sealed partial class OverviewPage {
        public OverviewPage() => InitializeComponent();

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
            var nextCommand = new StandardUICommand(StandardUICommandKind.Forward);
            nextCommand.ExecuteRequested += (command, args) =>
                (Application.Current as App)?.ShellNavigateTo(App.FindMyPage<SelectingImages>());
            ((Button)sender).Command = nextCommand;
        }
    }
}
