using Windows.UI.Xaml.Controls;

namespace YourWaifu2x.Views.Samples {
    [Page(PageCategory.Components, "PersonPicture", Description = "The person picture control displays the avatar image for a person, if one is available; if not, it displays the person's initials or a generic glyph.", DocumentationLink = "https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/person-picture")]
    public sealed partial class PersonPictureSamplePage : Page {
        public PersonPictureSamplePage() {
            InitializeComponent();
        }
    }
}