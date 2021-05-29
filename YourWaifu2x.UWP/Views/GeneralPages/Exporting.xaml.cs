namespace YourWaifu2x.Views.GeneralPages {
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Entities.Data;
    using Extensions;

    [Page(PageCategory.None, "Exporting")]
    public sealed partial class Exporting {
        public Exporting() {
            InitializeComponent();

            var outPutter = new TextBoxOutPutter(TestBox);
            Console.SetError(outPutter);
            Console.SetOut(outPutter);
        }

        private void Exporting_OnLoaded(object sender, RoutedEventArgs e) {

        }

        private void WaitingList_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            throw new NotImplementedException();
        }

        private void FinishedList_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            throw new NotImplementedException();
        }

        private void ErrorList_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            throw new NotImplementedException();
        }
    }
}
