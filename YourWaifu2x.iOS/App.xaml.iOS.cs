namespace YourWaifu2x {
    using BranchXamarinSDK;
    using Foundation;
    using UIKit;
    using YourWaifu2x.Deeplinking;

    public sealed partial class App {
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions) {
#if DEBUG
            Branch.Debug = true;
#endif
            BranchIOS.Init(BranchService.BranchKey, launchOptions, BranchService.Instance);

            return base.FinishedLaunching(application, launchOptions);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation) =>
            BranchIOS.getInstance().OpenUrl(url);//return base.OpenUrl(application, url, sourceApplication, annotation);

        public override bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler) =>
            BranchIOS.getInstance().ContinueUserActivity(userActivity);//return base.ContinueUserActivity(application, userActivity, completionHandler);
    }
}
