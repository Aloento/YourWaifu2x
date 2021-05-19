using System;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace YourWaifu2x
{
    [TemplatePart(Name = ViewButtonPartName, Type = typeof(Button))]
    public partial class OverviewSampleView : ContentControl
    {
        private const string ViewButtonPartName = "PART_ViewButton";

        public Type SamplePageType
        {
            get => (Type)GetValue(SamplePageTypeProperty);
            set => SetValue(SamplePageTypeProperty, value);
        }

        public static readonly DependencyProperty SamplePageTypeProperty =
            DependencyProperty.Register("SamplePageType", typeof(Type), typeof(OverviewSampleView), new PropertyMetadata(null, OnSamplePageTypeChanged));

        private static void OnSamplePageTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Type type)
            {
                OverviewSampleView that = (OverviewSampleView)d;
                that.Sample = new Sample(type.GetTypeInfo().GetCustomAttribute<SamplePageAttribute>(), type);
            }
        }

        public Sample Sample
        {
            get => (Sample)GetValue(SampleProperty);
            set => SetValue(SampleProperty, value);
        }

        public static readonly DependencyProperty SampleProperty =
            DependencyProperty.Register("Sample", typeof(Sample), typeof(OverviewSampleView), new PropertyMetadata(null));

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Button viewButton = (Button)GetTemplateChild(ViewButtonPartName);
            viewButton.Click -= OnViewClicked;
            viewButton.Click += OnViewClicked;

            void OnViewClicked(object sender, RoutedEventArgs e)
            {
                (Application.Current as App)?.ShellNavigateTo(Sample);
            }
        }
    }
}
