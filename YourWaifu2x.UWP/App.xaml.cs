namespace YourWaifu2x
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.Logging;
    using ShowMeTheXAML;
    using Uno.Extensions;
    using Uno.Logging;
    using Windows.ApplicationModel;
    using Windows.ApplicationModel.Activation;
    using Windows.Foundation;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Automation;
    using Windows.UI.Xaml.Controls;
    using YourWaifu2x.Helpers;
    using YourWaifu2x.Views.GeneralPages;
    using MUXC = Microsoft.UI.Xaml.Controls;
    using MUXCP = Microsoft.UI.Xaml.Controls.Primitives;

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        private static Sample[] samples;
        private Shell shell;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
#if !WINDOWS_UWP
            Uno.UI.FeatureConfiguration.ApiInformation.NotImplementedLogLevel = LogLevel.Debug; // Raise not implemented usages as Debug messages
#endif

            ConfigureFilters(LogExtensionPoint.AmbientLoggerFactory);
            XamlDisplay.Init(this.GetType().Assembly);
            this.InitializeComponent();

            /// <summary>
            /// Invoked when application execution is being suspended. Application state is saved
            /// without knowing whether the application will be terminated or resumed with the contents
            /// of memory still intact.
            /// </summary>
            /// <param name="sender">The source of the suspend request.</param>
            /// <param name="e">Details about the suspend request.</param>
            Suspending += (object sender, SuspendingEventArgs e) => e.SuspendingOperation.GetDeferral().Complete();

#if __WASM__
            _ = Windows.UI.Xaml.Window.Current.Dispatcher?.RunIdleAsync(_ => AnalyticsService.Initialize());
#endif
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            this.Log().Debug("Launched app.");

            var window = Window.Current;
            var isFirstLaunch = !(window.Content is Shell);

            if (isFirstLaunch)
            {
#if __IOS__ && USE_UITESTS
                // requires Xamarin Test Cloud Agent
                Xamarin.Calabash.Start();
#endif

                this.InitializeThemes();

#if WINDOWS_UWP
                ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(320, 568)); // (size of the iPhone SE)
#endif

                if (!(window.Content is Shell))
                {
                    window.Content = this.shell = this.BuildShell();
                }
            }

            // Ensure the current window is active
            window.Activate();
        }

        /// <summary>
        /// This method is used as the entry point when opening the app from an url.
        /// </summary>
        protected override void OnActivated(IActivatedEventArgs args)
        {
            this.Log().Debug("Activated app.");
            base.OnActivated(args);
        }

        public void ShellNavigateTo(Sample sample) => this.ShellNavigateTo(sample, trySynchronizeCurrentItem: true);

        private void ShellNavigateTo<TPage>(bool trySynchronizeCurrentItem = true) where TPage : Page
        {
            var pageType = typeof(TPage);
            var attribute = pageType.GetCustomAttribute<SamplePageAttribute>()
                ?? throw new NotSupportedException($"{pageType} isn't tagged with [{nameof(SamplePageAttribute)}].");
            var sample = new Sample(attribute, pageType);

            this.ShellNavigateTo(sample, trySynchronizeCurrentItem);
        }

        private void ShellNavigateTo(Sample sample, bool trySynchronizeCurrentItem)
        {
            var nv = this.shell.NavigationView;
            if (nv.Content?.GetType() != sample.ViewType)
            {
                var selected = trySynchronizeCurrentItem
                    ? nv.MenuItems
                        .OfType<MUXC.NavigationViewItem>()
                        .FirstOrDefault(x => (x.DataContext as Sample)?.ViewType == sample.ViewType)
                    : default;
                if (selected != null)
                {
                    nv.SelectedItem = selected;
                }

                var page = (Page)Activator.CreateInstance(sample.ViewType);
                page.DataContext = sample;

#if __WASM__
                _ = Windows.UI.Xaml.Window.Current.Dispatcher.RunIdleAsync(_ => AnalyticsService.TrackView(sample?.Title ?? page.GetType().Name));
#endif

                this.shell.NavigationView.Content = page;
            }
        }

        private Shell BuildShell()
        {
            this.shell = new Shell();
            AutomationProperties.SetAutomationId(this.shell, "AppShell");
            this.shell.RegisterPropertyChangedCallback(Shell.CurrentSampleBackdoorProperty, this.OnCurrentSampleBackdoorChanged);
            var nv = this.shell.NavigationView;
            this.AddNavigationItems(nv);

            // landing navigation
            this.ShellNavigateTo<OverviewPage>(
#if !WINDOWS_UWP
                // workaround for uno#5069: setting NavView.SelectedItem at launch bricks it
                trySynchronizeCurrentItem: false
#endif
            );

            // navigation + setting handler
            nv.ItemInvoked += this.OnNavigationItemInvoked;

            return this.shell;
        }

        private void OnCurrentSampleBackdoorChanged(DependencyObject sender, DependencyProperty dp)
        {
            var sample = GetSamples()
                .FirstOrDefault(x => string.Equals(x.Title, this.shell.CurrentSampleBackdoor, StringComparison.OrdinalIgnoreCase));

            if (sample == null)
            {
                this.Log().LogWarning($"No SampleAttribute found with a Title that matches: {this.shell.CurrentSampleBackdoor}");
                return;
            }

            this.ShellNavigateTo(sample);
        }


        private void OnNavigationItemInvoked(MUXC.NavigationView sender, MUXC.NavigationViewItemInvokedEventArgs e)
        {
            if (e.InvokedItemContainer.DataContext is Sample sample)
            {
                this.ShellNavigateTo(sample, trySynchronizeCurrentItem: false);
            }
        }

        private void AddNavigationItems(MUXC.NavigationView nv)
        {
            var categories = GetSamples()
                .OrderByDescending(x => x.SortOrder.HasValue)
                .ThenBy(x => x.SortOrder)
                .ThenBy(x => x.Title)
                .GroupBy(x => x.Category);

            foreach (var category in categories.OrderBy(x => x.Key))
            {
                var tier = 1;

                var parentItem = default(MUXC.NavigationViewItem);
                if (category.Key != SampleCategory.None)
                {
                    parentItem = new MUXC.NavigationViewItem
                    {
                        Content = category.Key.GetDescription() ?? category.Key.ToString(),
                        SelectsOnInvoked = false,
                        Style = (Style)this.Resources[$"T{tier++}NavigationViewItemStyle"]
                    }.Apply(NavViewItemVisualStateFix);
                    AutomationProperties.SetAutomationId(parentItem, "Section_" + parentItem.Content);

                    nv.MenuItems.Add(parentItem);
                }

                foreach (var sample in category)
                {
                    var item = new MUXC.NavigationViewItem
                    {
                        Content = sample.Title,
                        DataContext = sample,
                        Style = (Style)this.Resources[$"T{tier}NavigationViewItemStyle"]
                    }.Apply(NavViewItemVisualStateFix);
                    AutomationProperties.SetAutomationId(item, "Section_" + item.Content);

                    (parentItem?.MenuItems ?? nv.MenuItems).Add(item);
                }
            }

            void NavViewItemVisualStateFix(MUXC.NavigationViewItem nvi) =>
                // gallery#107: on uwp and uno, deselecting a NVI by selecting another NVI will leave the former in the "Selected" state
                // to workaround this, we force reset the visual state when IsSelected becomes false
                nvi.RegisterPropertyChangedCallback(MUXC.NavigationViewItemBase.IsSelectedProperty, (s, e) =>
                {
                    if (!nvi.IsSelected)
                    {
                        // depending on the DisplayMode, a NVIP may or may not be used.
                        var nvip = VisualTreeHelperEx.GetFirstDescendant<MUXCP.NavigationViewItemPresenter>(nvi, x => x.Name == "NavigationViewItemPresenter");
                        _ = VisualStateManager.GoToState((Control)nvip ?? nvi, "Normal", true);
                    }
                });
        }

        /// <summary>
        /// Configures global logging
        /// </summary>
        /// <param name="factory"></param>
        private static void ConfigureFilters(ILoggerFactory factory) => factory
                .WithFilter(new FilterLoggerSettings
                    {
                        { "Uno", LogLevel.Warning },
                        { "Windows", LogLevel.Warning },
                        { "YourWaifu2x", LogLevel.Debug },
#if !DEBUG
						{ "Windows.UI.Xaml", LogLevel.None },
						{ "Windows.ApplicationModel.Core.CoreApplicationViewTitleBar", LogLevel.None },
						{ "Uno.UI.DataBinding.BindingPropertyHelper", LogLevel.None },
#endif

						// Debug JS interop
						// { "Uno.Foundation.WebAssemblyRuntime", LogLevel.Debug },

						// Generic Xaml events
						// { "Windows.UI.Xaml", LogLevel.Debug },
						// { "Windows.UI.Xaml.VisualStateGroup", LogLevel.Debug },
						// { "Windows.UI.Xaml.StateTriggerBase", LogLevel.Debug },
						// { "Windows.UI.Xaml.UIElement", LogLevel.Debug },

						// Layouter specific messages
						// { "Windows.UI.Xaml.Controls", LogLevel.Debug },
						// { "Windows.UI.Xaml.Controls.Layouter", LogLevel.Debug },
						// { "Windows.UI.Xaml.Controls.Panel", LogLevel.Debug },
						// { "Windows.Storage", LogLevel.Debug },

						// Binding related messages
						// { "Windows.UI.Xaml.Data", LogLevel.Debug },

						// DependencyObject memory references tracking
						// { "ReferenceHolder", LogLevel.Debug },

						// ListView-related messages
						// { "Windows.UI.Xaml.Controls.ListViewBase", LogLevel.Debug },
						// { "Windows.UI.Xaml.Controls.ListView", LogLevel.Debug },
						// { "Windows.UI.Xaml.Controls.GridView", LogLevel.Debug },
						// { "Windows.UI.Xaml.Controls.VirtualizingPanelLayout", LogLevel.Debug },
						// { "Windows.UI.Xaml.Controls.NativeListViewBase", LogLevel.Debug },
						// { "Windows.UI.Xaml.Controls.ListViewBaseSource", LogLevel.Debug }, //iOS
						// { "Windows.UI.Xaml.Controls.ListViewBaseInternalContainer", LogLevel.Debug }, //iOS
						// { "Windows.UI.Xaml.Controls.NativeListViewBaseAdapter", LogLevel.Debug }, //Android
						// { "Windows.UI.Xaml.Controls.BufferViewCache", LogLevel.Debug }, //Android
						// { "Windows.UI.Xaml.Controls.VirtualizingPanelGenerator", LogLevel.Debug }, //WASM
					}
                )
#if DEBUG
                .AddConsole(LogLevel.Debug);
#else
				.AddConsole(LogLevel.Information);
#endif

        public static IEnumerable<Sample> GetSamples() => samples = samples ?? Assembly.GetExecutingAssembly()
                           .DefinedTypes
                           .Where(x => x.Namespace?.StartsWith("YourWaifu2x") == true)
                           .Select(x => new { TypeInfo = x, SamplePageAttribute = x.GetCustomAttribute<SamplePageAttribute>() })
                           .Where(x => x.SamplePageAttribute != null)
                           .Select(x => new Sample(x.SamplePageAttribute, x.TypeInfo.AsType()))
                           .ToArray();

        private void InitializeThemes()
        {
            Uno.Material.Resources.Init(this, colorPaletteOverride: new ResourceDictionary() { Source = new Uri("ms-appx:///Views/Colors.xaml") });
            Uno.Cupertino.Resources.Init(this, null);
            this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("ms-appx:///Views/Styles/TextBlock.xaml") });
        }
    }
}
