using Windows.UI.Xaml.Controls;

namespace YourWaifu2x.Views.Samples {
#if !__WASM__ && !__MACOS__
    [Page(PageCategory.Components, "TimePicker", Description = "This control allows users to pick a time value.", DocumentationLink = "https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.timepicker")]
#endif
    public sealed partial class TimePickerSamplePage : Page {
        public TimePickerSamplePage() {
            InitializeComponent();
        }
    }
}
