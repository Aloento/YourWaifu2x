﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace YourWaifu2x.Views.NestedPages {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CommandBarSample_NestedMaterialPage2 : Page {
        public CommandBarSample_NestedMaterialPage2() {
            InitializeComponent();
        }

        private void NavigateBack(object sender, RoutedEventArgs e) {
            Frame.GoBack();
        }
    }
}
