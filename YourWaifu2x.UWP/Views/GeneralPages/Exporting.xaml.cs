namespace YourWaifu2x.Views.GeneralPages {
    using System;
    using Windows.UI.Xaml;
    using Extensions;

    [Page(PageCategory.None, "Exporting")]
    public sealed partial class Exporting {
        public Exporting() {
            InitializeComponent();

            var outPutter = new TextBoxOutPutter(TestBox);
            Console.SetOut(outPutter);
        }

        private void Exporting_OnLoaded(object sender, RoutedEventArgs e) {

        }
    }
}
