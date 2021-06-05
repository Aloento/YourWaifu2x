namespace YourWaifu2x.Views.MyPages {
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;
    using Entities.Data;
    using Extensions;
    using Helpers;
    using Microsoft.UI.Xaml.Controls;

    [Page(PageCategory.None, "Exporting")]
    public sealed partial class Exporting {
        public Exporting() {
            InitializeComponent();

            var outPutter = new TextBoxOutPutter(TestBox);
            Console.SetError(outPutter);
            Console.SetOut(outPutter);
        }

        private void Exporting_OnLoaded(object sender, RoutedEventArgs e) {
            WaitingList.ItemsSource = WaifuInstance.WaitingList;
            FinishedList.ItemsSource = WaifuInstance.FinishedList;
            ErrorList.ItemsSource = WaifuInstance.ErrorList;

            foreach (var item in VisualTreeHelperEx.GetDescendants(
                VisualTreeHelper.GetParent(
                    VisualTreeHelper.GetParent(
                        VisualTreeHelper.GetParent(
                            VisualTreeHelper.GetParent(
                                (ContentPageLayout)sender)))))) {
                if (!(item is InfoBar infoBar))
                    continue;

                var startCommand = new StandardUICommand(StandardUICommandKind.Play);
                var b = new Button {
                    Content = "Start",
                    Command = startCommand,
                };

                startCommand.ExecuteRequested += async (command, args) => {
                    b.IsEnabled = false;
                    b.Content = "Adding Images...";
                    infoBar.Severity = InfoBarSeverity.Warning;

                    await Task.Run(() => {
                        _ = Console.Out.WriteLineAsync("Adding Images...");
                        foreach (var input in WaifuInstance.WaitingList) {
                            WaifuInstance.Waifu2X.Submit(new WaifuConfig(input));
                            _ = Console.Out.WriteLineAsync("Added: " + input.Path);
                        }
                        _ = Console.Out.WriteLineAsync(WaifuInstance.WaitingList.Count + " Images Added To Queue");
                    });

                    b.Content = "Waifu2x Processing...";
                    WaifuInstance.Waifu2X.Start(this);

                    _ = Task.Run(() => {
                        while (WaifuInstance.WaitingList.Count != 0) {
                            Thread.SpinWait(0);
                        }
                        _ = Dispatcher.RunIdleAsync(_ => {
                            if (WaifuInstance.ErrorList.Count == 0) {
                                b.Content = "Successful";
                                infoBar.Severity = InfoBarSeverity.Success;
                                Console.Out.WriteLineAsync("Successful");
                            } else {
                                b.Content = "Finished with Errors";
                                infoBar.Severity = InfoBarSeverity.Error;
                                Console.Error.WriteLineAsync("Finished with Errors");
                            }
                        });
                    });
                };

                infoBar.ActionButton = b;
                break;
            }
        }
    }
}
