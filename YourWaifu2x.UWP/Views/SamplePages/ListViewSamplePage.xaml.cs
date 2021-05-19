using Windows.UI.Xaml.Controls;
using YourWaifu2x.Entities.Data;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace YourWaifu2x.Views.Samples
{

    [SamplePage(SampleCategory.Features, "ListView",
        Description = "Represents a control that displays data items in a vertical stack.",
        DocumentationLink = "https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.listview",
        DataType = typeof(RecordCollection)
    )]
    public sealed partial class ListViewSamplePage : Page
    {
        public ListViewSamplePage()
        {
            InitializeComponent();
        }
    }
}