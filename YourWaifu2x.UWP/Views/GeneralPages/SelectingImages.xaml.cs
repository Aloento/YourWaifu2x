namespace YourWaifu2x.Views.GeneralPages {
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;

    [Page(PageCategory.None, "Selecting Images")]
    public sealed partial class SelectingImages {
        public readonly ObservableCollection<StorageFile> ImageListData = new ObservableCollection<StorageFile>();

        private readonly FileOpenPicker filePicker = new FileOpenPicker {
            ViewMode = PickerViewMode.Thumbnail,
            SuggestedStartLocation = PickerLocationId.Downloads
        };

        public SelectingImages() {
            InitializeComponent();
            filePicker.FileTypeFilter.Add(".jpg");
            filePicker.FileTypeFilter.Add(".jpeg");
            filePicker.FileTypeFilter.Add(".png");
            filePicker.FileTypeFilter.Add(".webp");
        }

        private void Selecting_OnLoaded(object sender, RoutedEventArgs e) {
            ImagesList.ItemsSource = ImageListData;

            var addCommand = new StandardUICommand(StandardUICommandKind.Open);
            addCommand.ExecuteRequested += async (command, args) => {
                var files = await filePicker.PickMultipleFilesAsync();
                if (files.Count <= 0)
                    return;
                foreach (var file in files) {
                    if (ImageListData.Any(image => image.IsEqual(file)))
                        return;

                    ImageListData.Add(file);
                }
            };
            AddButton.Command = addCommand;

            var folderCommand = new StandardUICommand(StandardUICommandKind.Open);
            folderCommand.ExecuteRequested += (command, args) => {

            };
            FolderButton.Command = folderCommand;

            var deleteCommand = new StandardUICommand(StandardUICommandKind.Delete);
            deleteCommand.ExecuteRequested += (command, args) => {
                if (ImagesList.SelectedItems.Count <= 0)
                    return;

                for (var i = 0; i < ImagesList.SelectedItems.Count;) {
                    _ = ImageListData.Remove((StorageFile)ImagesList.SelectedItems[i]);
                }
            };
            DeleteButton.Command = deleteCommand;

            var clearCommand = new StandardUICommand(StandardUICommandKind.Delete);
            clearCommand.ExecuteRequested += (command, args) => ImageListData.Clear();
            CleanButton.Command = clearCommand;
        }

        private void ImagesList_OnSelectionChanged(object sender, SelectionChangedEventArgs e) =>
            AllCountLabel.Label = ImagesList.SelectedItems.Count + " / " + ImageListData.Count;
    }
}
