using Uno.UI.Toolkit;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace YourWaifu2x.Views.Samples {
    [Page(PageCategory.Features, "MessageDialog", Description = "This represents a simple dialog to show to users. Customization is limited to title text, content text and commands.", DocumentationLink = "https://docs.microsoft.com/en-us/uwp/api/windows.ui.popups.messagedialog")]
    public sealed partial class MessageDialogSamplePage : Page {
        public MessageDialogSamplePage() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            MessageDialog messageDialog = new MessageDialog("Hello world!");
            messageDialog.ShowAsync();
        }

        private void Button_Click2(object sender, RoutedEventArgs e) {
            MessageDialog messageDialog = new MessageDialog("This is a very important message.", "Notice");
            messageDialog.ShowAsync();
        }

        private void Button_Click3(object sender, RoutedEventArgs e) {
            MessageDialog messageDialog = new MessageDialog("Are you sure you want to log out?", "Log out") {
                Commands =
                {
                    new UICommand("Cancel"),
                    new UICommand("Log out"),
                }
            };
            messageDialog.ShowAsync();
        }

        private void Button_Click4(object sender, RoutedEventArgs e) {
            UICommand deleteCommand = new UICommand("Delete");
            deleteCommand.SetDestructive(true);

            MessageDialog messageDialog = new MessageDialog("Are you sure you want to delete this item?", "Delete") {

                Commands =
                {
                    new UICommand("Cancel"),
                    deleteCommand,
                }
            };
            messageDialog.ShowAsync();
        }
    }
}
