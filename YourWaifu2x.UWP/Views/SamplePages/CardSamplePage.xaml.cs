using Windows.UI.Xaml.Controls;

namespace YourWaifu2x.Views.SamplePages
{
    [SamplePage(SampleCategory.Components, "Card", Description = "This control is used to display content and actions about a single item.", DocumentationLink = "https://material.io/components/cards")]
    public sealed partial class CardSamplePage : Page
    {
        public CardSamplePage()
        {
            InitializeComponent();
        }
    }
}
