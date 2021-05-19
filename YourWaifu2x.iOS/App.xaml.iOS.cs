using BranchXamarinSDK;
using Foundation;
using UIKit;
using YourWaifu2x.Deeplinking;

namespace YourWaifu2x
{
    public sealed partial class App
    {
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
#if DEBUG
            BranchIOS.Debug = true;
#endif
            BranchIOS.Init(BranchService.BranchKey, launchOptions, BranchService.Instance);

            return base.FinishedLaunching(application, launchOptions);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            return BranchIOS.getInstance().OpenUrl(url);

            //return base.OpenUrl(application, url, sourceApplication, annotation);
        }

        public override bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
        {
            return BranchIOS.getInstance().ContinueUserActivity(userActivity);

            //return base.ContinueUserActivity(application, userActivity, completionHandler);
        }
    }
}
