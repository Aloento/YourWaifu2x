namespace YourWaifu2x {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.Logging;
    using Uno.Extensions;
    using Uno.Logging;
    using Windows.ApplicationModel.Activation;
    using Windows.Foundation;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Automation;
    using Windows.UI.Xaml.Controls;
    using Entities.Data;
    using Helpers;
    using Views.GeneralPages;
    using MUXC = Microsoft.UI.Xaml.Controls;
    using MUXCP = Microsoft.UI.Xaml.Controls.Primitives;

    public sealed partial class App {
        private static MyPage[] myPages;
        private Shell shell;

        public App() {
#if !WINDOWS_UWP
            Uno.UI.FeatureConfiguration.ApiInformation.NotImplementedLogLevel = LogLevel.Debug; // Raise not implemented usages as Debug messages
#endif

            InitializeComponent();
            Suspending += (s, e) => e.SuspendingOperation.GetDeferral().Complete();

#if __WASM__
            _ = Windows.UI.Xaml.Window.Current.Dispatcher?.RunIdleAsync(_ => AnalyticsService.Initialize());
#endif
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e) {
            this.Log().Debug("Launched app.");

            var window = Window.Current;
            var isFirstLaunch = !(window.Content is Shell);

            if (isFirstLaunch) {
#if __IOS__ && USE_UITESTS
                // requires Xamarin Test Cloud Agent
                Xamarin.Calabash.Start();
#endif

                Uno.Material.Resources.Init(this, colorPaletteOverride: new ResourceDictionary() { Source = new Uri("ms-appx:///Views/Colors.xaml") });
                Uno.Cupertino.Resources.Init(this, null);
                Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("ms-appx:///Views/Styles/TextBlock.xaml") });

#if WINDOWS_UWP
                ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(320, 568)); // (size of the iPhone SE)
#endif

                if (!(window.Content is Shell))
                    window.Content = shell = BuildShell();

                WaifuInstance.Init();
            }

            window.Activate();
        }

        protected override void OnActivated(IActivatedEventArgs args) {
            this.Log().Debug("Activated app.");
            base.OnActivated(args);
        }

        public static MyPage FindMyPage<TPage>() where TPage : Page {
            var pageType = typeof(TPage);
            var attribute = pageType.GetCustomAttribute<PageAttribute>()
                            ?? throw new NotSupportedException($"{pageType} isn't tagged with [{nameof(PageAttribute)}].");

            return myPages.FirstOrDefault(page => page.Title.Equals(attribute.Title));
        }

        public void ShellNavigateTo(MyPage myPage) => ShellNavigateTo(myPage, true);

        //private void ShellNavigateTo<TPage>(bool trySynchronizeCurrentItem = true) where TPage : Page {
        //    var pageType = typeof(TPage);
        //    var attribute = pageType.GetCustomAttribute<PageAttribute>()
        //        ?? throw new NotSupportedException($"{pageType} isn't tagged with [{nameof(PageAttribute)}].");

        //    ShellNavigateTo(new MyPage(attribute, pageType), trySynchronizeCurrentItem);
        //}

        private void ShellNavigateTo(MyPage myPage, bool trySynchronizeCurrentItem) {
            var nv = shell.NavigationView;
            if (nv.Content?.GetType() == myPage.ViewType)
                return;

            var selected = trySynchronizeCurrentItem
                ? nv.MenuItems
                    .OfType<MUXC.NavigationViewItem>()
                    .FirstOrDefault(x => (x.DataContext as MyPage)?.ViewType == myPage.ViewType)
                : default;

            if (selected != null)
                nv.SelectedItem = selected;

            var page = (Page)Activator.CreateInstance(myPage.ViewType);
            page.DataContext = myPage;

#if __WASM__
                _ = Windows.UI.Xaml.Window.Current.Dispatcher.RunIdleAsync(_ => AnalyticsService.TrackView(myPage?.Title ?? page.GetType().Name));
#endif

            shell.NavigationView.Content = page;
        }

        private Shell BuildShell() {
            shell = new Shell();
            AutomationProperties.SetAutomationId(shell, "AppShell");
            _ = shell.RegisterPropertyChangedCallback(Shell.CurrentSampleBackdoorProperty, OnCurrentSampleBackdoorChanged);
            var nv = shell.NavigationView;
            AddNavigationItems(nv);

            ShellNavigateTo(FindMyPage<OverviewPage>());
            nv.ItemInvoked += (s, e) => {
                if (e.InvokedItemContainer.DataContext is MyPage sample)
                    ShellNavigateTo(sample, false);
            };

            return shell;
        }

        private void OnCurrentSampleBackdoorChanged(DependencyObject sender, DependencyProperty dp) {
            var sample = GetPages()
                .FirstOrDefault(x => string.Equals(x.Title, shell.CurrentSampleBackdoor, StringComparison.OrdinalIgnoreCase));

            if (sample == null) {
                this.Log().LogWarning($"No SampleAttribute found with a Title that matches: {shell.CurrentSampleBackdoor}");
                return;
            }

            ShellNavigateTo(sample);
        }

        private void AddNavigationItems(MUXC.NavigationView nv) {
            var categories = GetPages()
                .OrderByDescending(x => x.SortOrder.HasValue)
                .ThenBy(x => x.SortOrder)
                .ThenBy(x => x.Title)
                .GroupBy(x => x.Category);

            foreach (var category in categories.OrderBy(x => x.Key)) {
                foreach (var sample in category) {
                    var item = new MUXC.NavigationViewItem {
                        Content = sample.Title,
                        DataContext = sample,
                        Style = (Style)Resources["T1NavigationViewItemStyle"]
                    }.Apply(NavViewItemVisualStateFix);
                    AutomationProperties.SetAutomationId(item, "Section_" + item.Content);

                    nv.MenuItems.Add(item);
                }
            }

            void NavViewItemVisualStateFix(MUXC.NavigationViewItem nvi) =>
                nvi.RegisterPropertyChangedCallback(MUXC.NavigationViewItemBase.IsSelectedProperty, (s, e) => {
                    if (!nvi.IsSelected) {
                        var nvip = VisualTreeHelperEx.GetFirstDescendant<MUXCP.NavigationViewItemPresenter>(nvi, x => x.Name == "NavigationViewItemPresenter");
                        _ = VisualStateManager.GoToState((Control)nvip ?? nvi, "Normal", true);
                    }
                });
        }

        public static IEnumerable<MyPage> GetPages() => myPages = myPages ?? Assembly.GetExecutingAssembly()
                           .DefinedTypes
                           .Where(x => x.Namespace?.StartsWith("YourWaifu2x") == true)
                           .Select(x => (TypeInfo: x, SamplePageAttribute: x.GetCustomAttribute<PageAttribute>()))
                           .Where(x => x.SamplePageAttribute != null)
                           .Select(x => new MyPage(x.SamplePageAttribute, x.TypeInfo.AsType()))
                           .ToArray();
    }
}
