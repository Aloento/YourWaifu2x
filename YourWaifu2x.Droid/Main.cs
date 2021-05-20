namespace YourWaifu2x.Droid {
    using Android.App;
    using Android.Runtime;
    using BranchXamarinSDK;
    using Com.Nostra13.Universalimageloader.Core;
    using System;
    using Windows.UI.Xaml.Media;
    using YourWaifu2x.Deeplinking;

    [Application(
        Label = "@string/ApplicationName",
        LargeHeap = true,
        HardwareAccelerated = true,
        Theme = "@style/AppTheme"
    )]
    [MetaData("io.branch.sdk.auto_link_disable", Value = "false")]
    [MetaData("io.branch.sdk.TestMode", Value = "false")]
    [MetaData("io.branch.sdk.BranchKey", Value = BranchService.BranchKey)]
    public class Application : Windows.UI.Xaml.NativeApplication {
        public Application(IntPtr javaReference, JniHandleOwnership transfer)
            : base(() => new App(), javaReference, transfer) => ConfigureUniversalImageLoader();

        public override void OnCreate() {
            base.OnCreate();
            BranchAndroid.GetAutoInstance(ApplicationContext);
        }

        private void ConfigureUniversalImageLoader() {
            // Create global configuration and initialize ImageLoader with this config
            var config = new ImageLoaderConfiguration
                .Builder(Context)
                .Build();

            ImageLoader.Instance.Init(config);

            ImageSource.DefaultImageLoader = ImageLoader.Instance.LoadImageAsync;
        }
    }
}
