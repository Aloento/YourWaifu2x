namespace YourWaifu2x.Views.GeneralPages {
    using System.Collections.ObjectModel;
    using System.IO;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Input;

    [Page(PageCategory.None, "Selecting Images")]
    public sealed partial class SelectingImages {
        public SelectingImages() => InitializeComponent();
        public readonly ObservableCollection<FileStream> ImageListData = new ObservableCollection<FileStream>();

        private void Selecting_OnLoaded(object sender, RoutedEventArgs e) {
            ImagesList.ItemsSource = ImageListData;
            ImageListData.Add(File.OpenRead("YourWaifu2x.exe"));

            ImagesList.SelectionChanged += (o, args) =>
                AllCountLabel.Label = ImagesList.SelectedItems.Count + "/" + ImageListData.Count;

            var deleteCommand = new StandardUICommand(StandardUICommandKind.Delete);
            deleteCommand.ExecuteRequested += (command, args) => {
                if (ImagesList.SelectedItems.Count == 0)
                    return;
                foreach (var i in ImagesList.SelectedItems) {
                    _ = ImageListData.Remove((FileStream)i);
                }
            };
            DeleteButton.Command = deleteCommand;
        }
    }
}
