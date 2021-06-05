using System.ComponentModel;
using System.Globalization;
using Windows.UI.Xaml.Controls;

namespace YourWaifu2x.Views.Samples {
    [Page(PageCategory.Features, "Binding", Description = "Bindings allow you to pass data between your UI and business logic.", DocumentationLink = "https://docs.microsoft.com/en-us/windows/uwp/data-binding/")]
    public sealed partial class BindingSamplePage : Page {
        public BindingSamplePage() {
            InitializeComponent();
        }
    }

    public class BindingSamplePageViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _text;
        public string Text {
            get => _text;
            set {
                _text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextLength)));
            }
        }

        public string TextLength => Text?.Length.ToString(CultureInfo.InvariantCulture) ?? "[empty]";
    }
}