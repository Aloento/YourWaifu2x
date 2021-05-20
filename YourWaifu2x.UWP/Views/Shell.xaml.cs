namespace YourWaifu2x
{
    using System;
    using Windows.UI.Core;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using YourWaifu2x.Helpers;
    using MUXC = Microsoft.UI.Xaml.Controls;

    public sealed partial class Shell : UserControl
    {
        public Shell()
        {
            this.InitializeComponent();

            this.InitializeSafeArea();
            Loaded += this.OnLoaded;

            _ = this.NestedSampleFrame.RegisterPropertyChangedCallback(ContentControl.ContentProperty, this.OnNestedSampleFrameChanged);
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) => e.Handled = this.BackNavigateFromNestedSample();
        }

        public static Shell GetForCurrentView() => (Shell)Window.Current.Content;

        public MUXC.NavigationView NavigationView => this.NavigationViewControl;

        public string CurrentSampleBackdoor
        {
            get => (string)this.GetValue(CurrentSampleBackdoorProperty);
            set => this.SetValue(CurrentSampleBackdoorProperty, value);
        }

        public static readonly DependencyProperty CurrentSampleBackdoorProperty =
            DependencyProperty.Register(nameof(CurrentSampleBackdoor), typeof(string), typeof(Shell), new PropertyMetadata(null));

        private void OnLoaded(object sender, RoutedEventArgs e) => this.SetDarkLightToggleInitialState();
#if __IOS__ || __ANDROID__
            this.Log().Debug("Loaded Shell.");
            YourWaifu2x.Deeplinking.BranchService.Instance.SetIsAppReady();
#endif

        private void SetDarkLightToggleInitialState()
        {
            // Initialize the toggle to the current theme.
            var root = Window.Current.Content as FrameworkElement;

            switch (root.ActualTheme)
            {
                case ElementTheme.Default:
                    this.DarkLightModeToggle.IsChecked = SystemThemeHelper.GetSystemApplicationTheme() == ApplicationTheme.Dark;
                    break;
                case ElementTheme.Light:
                    this.DarkLightModeToggle.IsChecked = false;
                    break;
                case ElementTheme.Dark:
                    this.DarkLightModeToggle.IsChecked = true;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// This method handles the top padding for phones like iPhone X.
        /// </summary>
        private void InitializeSafeArea()
        {
            var full = Window.Current.Bounds;
            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;

            var topPadding = Math.Abs(full.Top - bounds.Top);

            if (topPadding > 0)
            {
                this.TopPaddingRow.Height = new GridLength(topPadding);
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            // Set theme for window root.
            if (Window.Current.Content is FrameworkElement root)
            {
                switch (root.ActualTheme)
                {
                    case ElementTheme.Default:
                        if (SystemThemeHelper.GetSystemApplicationTheme() == ApplicationTheme.Dark)
                        {
                            root.RequestedTheme = ElementTheme.Light;
                        }
                        else
                        {
                            root.RequestedTheme = ElementTheme.Dark;
                        }
                        break;
                    case ElementTheme.Light:
                        root.RequestedTheme = ElementTheme.Dark;
                        break;
                    case ElementTheme.Dark:
                        root.RequestedTheme = ElementTheme.Light;
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnNestedSampleFrameChanged(DependencyObject sender, DependencyProperty dp)
        {
            var isInsideNestedSample = this.NestedSampleFrame.Content != null;

            // prevent empty frame from blocking the content (nav-view) behind it
            this.NestedSampleFrame.Visibility = isInsideNestedSample
                ? Visibility.Visible
                : Visibility.Collapsed;

            // toggle built-in back button for wasm (from browser) and uwp (on title bar)
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = isInsideNestedSample
                ? AppViewBackButtonVisibility.Visible
                : AppViewBackButtonVisibility.Collapsed;
        }

        public void ShowNestedSample<TPage>(bool? clearStack = null) where TPage : Page
        {
            var wasFrameEmpty = this.NestedSampleFrame.Content == null;
            if (this.NestedSampleFrame.Navigate(typeof(TPage)) && (clearStack ?? wasFrameEmpty))
            {
                this.NestedSampleFrame.BackStack.Clear();
            }
        }

        public bool BackNavigateFromNestedSample()
        {
            if (this.NestedSampleFrame.Content == null)
            {
                return false;
            }

            if (this.NestedSampleFrame.CanGoBack)
            {
                this.NestedSampleFrame.GoBack();
            }
            else
            {
                this.NestedSampleFrame.Content = null;

#if __IOS__
                // This will force reset the UINavigationController, to prevent the back button from appearing when the stack is supposely empty.
                // note: Merely setting the Frame.Content to null, doesnt fully reset the stack.
                // When revisiting the page1 again, the previous page1 is still in the UINavigationController stack
                // causing a back button to appear that takes us back to the previous page1
                NestedSampleFrame.BackStack.Add(default);
                NestedSampleFrame.BackStack.Clear();
#endif
            }

            return true;
        }

        private void NavViewToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationViewControl.PaneDisplayMode == MUXC.NavigationViewPaneDisplayMode.LeftMinimal)
            {
                this.NavigationViewControl.IsPaneOpen = !this.NavigationViewControl.IsPaneOpen;
            }
            else if (this.NavigationViewControl.PaneDisplayMode == MUXC.NavigationViewPaneDisplayMode.Left)
            {
                this.NavigationViewControl.IsPaneVisible = !this.NavigationViewControl.IsPaneVisible;
                this.NavigationViewControl.IsPaneOpen = this.NavigationViewControl.IsPaneVisible;
            }
        }

        private void NavigationViewControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // This could be done using VisualState with Adaptive triggers, but an issue prevents this currently - https://github.com/unoplatform/uno/issues/5168
            var desktopWidth = (double)Application.Current.Resources["DesktopAdaptiveThresholdWidth"];
            if (e.NewSize.Width >= desktopWidth && this.NavigationViewControl.PaneDisplayMode != MUXC.NavigationViewPaneDisplayMode.Left)
            {
                this.NavigationViewControl.PaneDisplayMode = MUXC.NavigationViewPaneDisplayMode.Left;
                this.NavigationViewControl.IsPaneOpen = true;
            }
            else if (e.NewSize.Width < desktopWidth && this.NavigationViewControl.PaneDisplayMode != MUXC.NavigationViewPaneDisplayMode.LeftMinimal)
            {
                this.NavigationViewControl.IsPaneVisible = true;
                this.NavigationViewControl.PaneDisplayMode = MUXC.NavigationViewPaneDisplayMode.LeftMinimal;
            }
        }
    }
}
