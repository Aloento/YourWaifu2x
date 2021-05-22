namespace YourWaifu2x {
    using System.Collections.Generic;
    using System.Linq;
    using Uno.Disposables;
    using Windows.Foundation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Documents;
    using YourWaifu2x.Helpers;

    /// <summary>
    /// This control is used as a template for each myPage page.
    /// </summary>
    public partial class SamplePageLayout : ContentControl {
        private const string VisualStateMaterial = nameof(Design.Material);
        private const string VisualStateFluent = nameof(Design.Fluent);
        private const string VisualStateCupertino = nameof(Design.Cupertino);
        private const string VisualStateNative = nameof(Design.Native);

        private const string MaterialRadioButtonPartName = "PART_MaterialRadioButton";
        private const string FluentRadioButtonPartName = "PART_FluentRadioButton";
        private const string CupertinoRadioButtonPartName = "PART_CupertinoRadioButton";
        private const string NativeRadioButtonPartName = "PART_NativeRadioButton";
        private const string StickyMaterialRadioButtonPartName = "PART_StickyMaterialRadioButton";
        private const string StickyFluentRadioButtonPartName = "PART_StickyFluentRadioButton";
        private const string StickyCupertinoRadioButtonPartName = "PART_StickyCupertinoRadioButton";
        private const string StickyNativeRadioButtonPartName = "PART_StickyNativeRadioButton";
        private const string ScrollingTabsPartName = "PART_ScrollingTabs";
        private const string StickyTabsPartName = "PART_StickyTabs";
        private const string ScrollViewerPartName = "PART_ScrollViewer";
        private const string TopPartName = "PART_MobileTopBar";
        private const string ShareHyperlinkPartName = "PART_ShareHyperlink";

        private static Design design = Design.Material;

        private IReadOnlyCollection<LayoutModeMapping> LayoutModeMappings => new List<LayoutModeMapping>
        {
            new LayoutModeMapping(Design.Material, materialRadioButton, stickyMaterialRadioButton, VisualStateMaterial, MaterialTemplate),
            new LayoutModeMapping(Design.Fluent, fluentRadioButton, stickyFluentRadioButton, VisualStateFluent, FluentTemplate),
            new LayoutModeMapping(Design.Cupertino, cupertinoRadioButton, stickyCupertinoRadioButton, VisualStateCupertino, CupertinoTemplate),
#if __IOS__ || __MACOS__ || __ANDROID__
			// native tab is only shown when applicable
			new LayoutModeMapping(Design.Native, nativeRadioButton, stickyNativeRadioButton, VisualStateNative, NativeTemplate),
#else
			// undefined template are not selectable and wont be selected by default
			new LayoutModeMapping(Design.Native, nativeRadioButton, stickyNativeRadioButton, VisualStateNative, default),
#endif
		};

        private RadioButton materialRadioButton;
        private RadioButton fluentRadioButton;
        private RadioButton cupertinoRadioButton;
        private RadioButton nativeRadioButton;
        private RadioButton stickyMaterialRadioButton;
        private RadioButton stickyFluentRadioButton;
        private RadioButton stickyCupertinoRadioButton;
        private RadioButton stickyNativeRadioButton;
        private FrameworkElement scrollingTabs;
        private FrameworkElement stickyTabs;
        private FrameworkElement top;
        private ScrollViewer scrollViewer;

        private readonly SerialDisposable subscriptions = new SerialDisposable();

        public SamplePageLayout() {
            DataContextChanged += OnDataContextChanged;

            void OnDataContextChanged(object sender, DataContextChangedEventArgs args) {
                if (args.NewValue is MyPage sample) {
                    Title = sample.Title;
                    Description = sample.Description;
                    DocumentationLink = sample.DocumentationLink;
                    Source = sample.Source;

#if __IOS__ || __ANDROID__
                    IsFooterVisible = true;
                    IsShareVisible = true;
#else
                    IsFooterVisible = !string.IsNullOrWhiteSpace(sample.DocumentationLink);
                    IsShareVisible = false;
#endif
                }
            }
        }

        protected override void OnApplyTemplate() {
            base.OnApplyTemplate();

            materialRadioButton = (RadioButton)GetTemplateChild(MaterialRadioButtonPartName);
            fluentRadioButton = (RadioButton)GetTemplateChild(FluentRadioButtonPartName);
            cupertinoRadioButton = (RadioButton)GetTemplateChild(CupertinoRadioButtonPartName);
            nativeRadioButton = (RadioButton)GetTemplateChild(NativeRadioButtonPartName);
            stickyMaterialRadioButton = (RadioButton)GetTemplateChild(StickyMaterialRadioButtonPartName);
            stickyFluentRadioButton = (RadioButton)GetTemplateChild(StickyFluentRadioButtonPartName);
            stickyCupertinoRadioButton = (RadioButton)GetTemplateChild(StickyCupertinoRadioButtonPartName);
            stickyNativeRadioButton = (RadioButton)GetTemplateChild(StickyNativeRadioButtonPartName);
            scrollingTabs = (FrameworkElement)GetTemplateChild(ScrollingTabsPartName);
            stickyTabs = (FrameworkElement)GetTemplateChild(StickyTabsPartName);
            scrollViewer = (ScrollViewer)GetTemplateChild(ScrollViewerPartName);
            top = (FrameworkElement)GetTemplateChild(TopPartName);
            var shareHyperlink = (Hyperlink)GetTemplateChild(ShareHyperlinkPartName);

            // ensure previous subscriptions is removed before adding new ones, in case OnApplyTemplate is called multiple times
            var disposables = new CompositeDisposable();
            subscriptions.Disposable = disposables;

            scrollViewer.ViewChanged += OnScrolled;
            _ = Disposable
                .Create(() => scrollViewer.ViewChanged -= OnScrolled)
                .DisposeWith(disposables);

            if (shareHyperlink != null) // This feature is not available on all platforms.
            {
                shareHyperlink.Click += OnShareClicked;
                _ = Disposable
                    .Create(() => shareHyperlink.Click -= OnShareClicked)
                    .DisposeWith(disposables);
            }

            BindOnClick(materialRadioButton);
            BindOnClick(fluentRadioButton);
            BindOnClick(cupertinoRadioButton);
            BindOnClick(nativeRadioButton);
            BindOnClick(stickyMaterialRadioButton);
            BindOnClick(stickyFluentRadioButton);
            BindOnClick(stickyCupertinoRadioButton);
            BindOnClick(stickyNativeRadioButton);

            UpdateLayoutRadioButtons();

            void BindOnClick(RadioButton radio) {
                radio.Click += OnLayoutRadioButtonChecked;
                _ = Disposable
                    .Create(() => radio.Click -= OnLayoutRadioButtonChecked)
                    .DisposeWith(disposables);
            }

            void OnScrolled(object sender, ScrollViewerViewChangedEventArgs e) {
                var relativeOffset = GetRelativeOffset();
                if (relativeOffset < 0) {
                    stickyTabs.Visibility = Visibility.Visible;
                } else {
                    stickyTabs.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void OnShareClicked(Hyperlink sender, HyperlinkClickEventArgs args) {
#if __IOS__ || __ANDROID__
            var myPage = DataContext as MyPage;
            _ = Deeplinking.BranchService.Instance.ShareSample(myPage, design);
#endif
        }

        /// <summary>
        /// Changes the preferred design.
        /// This doesn't change the current UI. It only affects the next created myPage.
        /// </summary>
        /// <param name="design">The desired design.</param>
        public static void SetPreferredDesign(Design design) => SamplePageLayout.design = design;

        private void RegisterEvent(RoutedEventHandler click) => click += OnLayoutRadioButtonChecked;

        private void UpdateLayoutRadioButtons() {
            var mappings = LayoutModeMappings;
            var previouslySelected = default(LayoutModeMapping);

            foreach (var mapping in mappings) {
                var visibility = mapping.Template != null ? Visibility.Visible : Visibility.Collapsed;
                mapping.RadioButton.Visibility = visibility;
                mapping.StickyRadioButton.Visibility = visibility;
                if (mapping.Template != null && mapping.Design == design) {
                    previouslySelected = mapping;
                }
            }

            // selected mode is based on previous selection and availability (whether the template is defined)
            var selected = previouslySelected ?? mappings.FirstOrDefault(x => x.Template != null);
            if (selected != null) {
                UpdateLayoutMode(transitionTo: selected.Design);
            }
        }

        private void OnLayoutRadioButtonChecked(object sender, RoutedEventArgs e) {
            if (sender is RadioButton radio && LayoutModeMappings.FirstOrDefault(x => x.RadioButton == radio || x.StickyRadioButton == radio) is LayoutModeMapping mapping) {
                design = mapping.Design;
                UpdateLayoutMode();
            }
        }

        private void UpdateLayoutMode(Design? transitionTo = null) {
            var design = transitionTo ?? SamplePageLayout.design;

            var current = LayoutModeMappings.FirstOrDefault(x => x.Design == design);
            if (current != null) {
                current.RadioButton.IsChecked = true;
                current.StickyRadioButton.IsChecked = true;

                _ = VisualStateManager.GoToState(this, current.VisualStateName, useTransitions: true);
            }
        }

        private double GetRelativeOffset() {
#if NETFX_CORE
            // On UWP we can count on finding a ScrollContentPresenter. 
            var scp = VisualTreeHelperEx.GetFirstDescendant<ScrollContentPresenter>(scrollViewer);
            var content = scp?.Content as FrameworkElement;
            var transform = scrollingTabs.TransformToVisual(content);
            return transform.TransformPoint(new Point(0, 0)).Y - scrollViewer.VerticalOffset;
#elif __IOS__
            GeneralTransform transform = scrollingTabs.TransformToVisual(scrollViewer);
            return transform.TransformPoint(new Point(0, 0)).Y;
#else
            var transform = scrollingTabs.TransformToVisual(this);
            return transform.TransformPoint(new Point(0, 0)).Y - top.ActualHeight;
#endif
        }

        /// <summary>
        /// Get control inside the specified layout template.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mode">The layout mode in which the control is defined</param>
        /// <param name="name">The 'x:Name' of the control</param>
        /// <returns></returns>
        /// <remarks>The caller must ensure the control is loaded. This is best done from <see cref="FrameworkElement.Loaded"/> event.</remarks>
        public T GetSampleChild<T>(Design mode, string name)
            where T : FrameworkElement {
            var presenter = (ContentPresenter)GetTemplateChild($"{mode}ContentPresenter");

            return VisualTreeHelperEx.GetFirstDescendant<T>(presenter, x => x.Name == name);
        }

        private class LayoutModeMapping {
            public Design Design { get; set; }
            public RadioButton RadioButton { get; set; }
            public RadioButton StickyRadioButton { get; set; }
            public string VisualStateName { get; set; }
            public DataTemplate Template { get; set; }

            public LayoutModeMapping(Design design, RadioButton radioButton, RadioButton stickyRadioButton, string visualStateName, DataTemplate template) {
                Design = design;
                RadioButton = radioButton;
                StickyRadioButton = stickyRadioButton;
                VisualStateName = visualStateName;
                Template = template;
            }
        }
    }
}
