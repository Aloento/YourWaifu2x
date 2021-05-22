﻿using Windows.UI.Xaml.Controls;

namespace YourWaifu2x {
    [Page(PageCategory.Theme, "Fluent Palette", SortOrder = 2, Description = Description)]
    public sealed partial class FluentPalettePage : Page {
        private const string Description = "View the Uno palette applied to Fluent's styles.";

        public FluentPalettePage() {
            InitializeComponent();
        }
    }
}
