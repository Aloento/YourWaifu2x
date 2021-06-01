namespace YourWaifu2x.Views.GeneralPages {
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Input;
    using Entities.Data;
    using Extensions;
    using Helpers;

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

            var startCommand = new StandardUICommand(StandardUICommandKind.Play);
            startCommand.ExecuteRequested += (command, args) => {
                foreach (var item in WaifuInstance.WaitingList) {
                    WaifuInstance.Waifu2X.Submit(new WaifuConfig(item));
                }
            };
            StartButton.Command = startCommand;
        }
    }
}
