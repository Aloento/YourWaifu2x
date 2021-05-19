using Windows.UI.Xaml.Controls;
using YourWaifu2x.Entities.Data;

namespace YourWaifu2x.Views.Samples
{
    [SamplePage(
        category: SampleCategory.Components,
        title: "Divider",
        Description = "This control is a thin line than can be used to divide layouts or groups content inside of lists.",
        DocumentationLink = "https://material.io/components/dividers",
        DataType = typeof(DividerItems)
    )]
    public sealed partial class DividerSamplePage : Page
    {
        public DividerSamplePage()
        {
            InitializeComponent();
        }
    }
}
