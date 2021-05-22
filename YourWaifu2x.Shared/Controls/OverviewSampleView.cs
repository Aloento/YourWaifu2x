namespace YourWaifu2x {
    using System;
    using System.Reflection;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    [TemplatePart(Name = ViewButtonPartName, Type = typeof(Button))]
    public partial class OverviewSampleView : ContentControl {
        private const string ViewButtonPartName = "PART_ViewButton";

        public Type SamplePageType {
            get => (Type)GetValue(SamplePageTypeProperty);
            set => SetValue(SamplePageTypeProperty, value);
        }

        public static readonly DependencyProperty SamplePageTypeProperty =
            DependencyProperty.Register("SamplePageType", typeof(Type), typeof(OverviewSampleView), new PropertyMetadata(null, OnSamplePageTypeChanged));

        private static void OnSamplePageTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (!(e.NewValue is Type type))
                return;
            var that = (OverviewSampleView)d;
            that.MyPage = new MyPage(type.GetTypeInfo().GetCustomAttribute<PageAttribute>(), type);
        }

        public MyPage MyPage {
            get => (MyPage)GetValue(MyPageProperty);
            set => SetValue(MyPageProperty, value);
        }

        public static readonly DependencyProperty MyPageProperty =
            DependencyProperty.Register("MyPage", typeof(MyPage), typeof(OverviewSampleView), new PropertyMetadata(null));

        protected override void OnApplyTemplate() {
            base.OnApplyTemplate();

            var viewButton = (Button)GetTemplateChild(ViewButtonPartName);
            viewButton.Click -= OnViewClicked;
            viewButton.Click += OnViewClicked;

            void OnViewClicked(object sender, RoutedEventArgs e) => (Application.Current as App)?.ShellNavigateTo(MyPage);
        }
    }
}
