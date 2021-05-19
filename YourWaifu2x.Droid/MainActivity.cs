using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using BranchXamarinSDK;
using YourWaifu2x.Deeplinking;

namespace YourWaifu2x.Droid
{
    [Activity(
            MainLauncher = true,
            ConfigurationChanges = Uno.UI.ActivityHelper.AllConfigChanges,
            WindowSoftInputMode = SoftInput.AdjustPan | SoftInput.StateHidden
        )]
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
        Categories = new[] { Android.Content.Intent.CategoryBrowsable, Android.Content.Intent.CategoryDefault },
        DataScheme = "https",
        DataHost = "unogallery.app.link",
        AutoVerify = true)]
    public class MainActivity : Windows.UI.Xaml.ApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            BranchAndroid.Init(this, BranchService.BranchKey, BranchService.Instance);
        }

        // Ensure we get the updated link identifier when the app becomes active
        // due to a Branch link click after having been in the background
        protected override void OnNewIntent(Intent intent)
        {
            Intent = intent;
        }
    }
}

