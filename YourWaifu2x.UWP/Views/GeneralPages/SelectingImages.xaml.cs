namespace YourWaifu2x.Views.GeneralPages {
    using System.Collections.ObjectModel;
    using System.IO;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;

    public class Images {
        public FileStream FileStream { get; set; }
        public string FileName => Path.GetFileName(FileStream.Name);
    }

    [Page(PageCategory.None, "Selecting Images")]
    public sealed partial class SelectingImages : Page {
        public SelectingImages() => InitializeComponent();
        public ObservableCollection<Images> ImageListData = new ObservableCollection<Images>();

        private void Selecting_OnLoaded(object sender, RoutedEventArgs e) {
            ImagesList.ItemsSource = ImageListData;

            ImageListData.Add(new Images() {
                FileStream = File.OpenRead("YourWaifu2x.exe")
            });

            var deleteCommand = new StandardUICommand(StandardUICommandKind.Delete);
            deleteCommand.ExecuteRequested += (command, args) => {
                if (ImagesList.SelectedItems.Count == 0)
                    return;
                foreach (var i in ImagesList.SelectedItems) {
                    _ = ImageListData.Remove((Images)i);
                }
            };
            DeleteButton.Command = deleteCommand;
        }
    }
}
