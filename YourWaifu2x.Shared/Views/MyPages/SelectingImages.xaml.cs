namespace YourWaifu2x.Views.MyPages {
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Imaging;
    using Entities.Data;
    using Helpers;

    [Page(PageCategory.None, "Selecting Images")]
    public sealed partial class SelectingImages {
        private readonly ObservableCollection<IStorageItem2> imageListData = WaifuInstance.WaitingList;

        private readonly FileOpenPicker filePicker = new FileOpenPicker {
            ViewMode = PickerViewMode.Thumbnail,
            SuggestedStartLocation = PickerLocationId.Downloads
        };

        private readonly FolderPicker folderPicker = new FolderPicker {
            SuggestedStartLocation = PickerLocationId.Downloads
        };

        public SelectingImages() {
            InitializeComponent();
            filePicker.FileTypeFilter.Add(".jpg");
            filePicker.FileTypeFilter.Add(".jpeg");
            filePicker.FileTypeFilter.Add(".png");
            filePicker.FileTypeFilter.Add(".webp");
            folderPicker.FileTypeFilter.Add("*");
        }

        private async void Selecting_OnLoaded(object sender, RoutedEventArgs e) {
            ImagesList.ItemsSource = imageListData;
            imageListData.CollectionChanged += (o, args) => ImagesList_OnSelectionChanged(null, null);

            if (imageListData.Count != 0) {
                await Dispatcher.RunIdleAsync(async _ => {
                    foreach (var item in VisualTreeHelperEx.GetDescendants(ImagesList)) {
                        if (!(item is Image image))
                            continue;
                        var file = from o in imageListData
                                   where o.Path == image.Tag.ToString()
                                   select o;

                        var bit = new BitmapImage();
                        await bit.SetSourceAsync(await ((StorageFile)file.First()).OpenReadAsync());
                        image.Stretch = Stretch.UniformToFill;
                        image.Source = bit;
                    }
                });
            }

            var addCommand = new StandardUICommand(StandardUICommandKind.Open);
            addCommand.ExecuteRequested += async (command, args) => {
                var files = await filePicker.PickMultipleFilesAsync();
                if (files.Count <= 0)
                    return;
                foreach (var file in files) {
                    if (imageListData.Any(image => image.IsEqual(file)))
                        continue;

                    imageListData.Add(file);

                    await Dispatcher.RunIdleAsync(async _ => {
                        foreach (var item in VisualTreeHelperEx.GetDescendants(ImagesList)) {
                            if (!(item is Image image))
                                continue;
                            if ((string)image.Tag != file.Path)
                                continue;

                            var bit = new BitmapImage();
                            await bit.SetSourceAsync(await file.OpenReadAsync());
                            image.Stretch = Stretch.UniformToFill;
                            image.Source = bit;
                        }
                    });
                }
            };
            AddButton.Command = addCommand;

            var folderCommand = new StandardUICommand(StandardUICommandKind.Open);
            folderCommand.ExecuteRequested += async (command, args) => {
                var folder = await folderPicker.PickSingleFolderAsync();
                if (folder == null || imageListData.Any(image => image.IsEqual(folder)))
                    return;

                Windows.Storage.AccessCache.StorageApplicationPermissions.
                    FutureAccessList.AddOrReplace("PickedFolderToken", folder);

                imageListData.Add(folder);
            };
            FolderButton.Command = folderCommand;

            var deleteCommand = new StandardUICommand(StandardUICommandKind.Delete);
            deleteCommand.ExecuteRequested += (command, args) => {
                if (ImagesList.SelectedItems.Count <= 0)
                    return;

                for (var i = 0; i < ImagesList.SelectedItems.Count;) {
                    _ = imageListData.Remove((StorageFile)ImagesList.SelectedItems[i]);
                }
            };
            DeleteButton.Command = deleteCommand;

            var clearCommand = new StandardUICommand(StandardUICommandKind.Delete);
            clearCommand.ExecuteRequested += (command, args) => imageListData.Clear();
            CleanButton.Command = clearCommand;

            var nextCommand = new StandardUICommand(StandardUICommandKind.Forward);
            nextCommand.ExecuteRequested += (command, args) =>
                (Application.Current as App)?.ShellNavigateTo(App.FindMyPage<SettingWaifu2x>());
            NextButton.Command = nextCommand;
        }

        private void ImagesList_OnSelectionChanged(object sender, SelectionChangedEventArgs e) =>
            AllCountLabel.Label = ImagesList.SelectedItems.Count + " / " + imageListData.Count;
    }
}
