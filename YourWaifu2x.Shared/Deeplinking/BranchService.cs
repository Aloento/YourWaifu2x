#if __IOS__ || __ANDROID__
namespace YourWaifu2x.Deeplinking {
    using BranchXamarinSDK;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Uno.Extensions;
    using Uno.Logging;
    using Windows.UI.Xaml;
    using Xamarin.Essentials;

    public class BranchService : IBranchBUOSessionInterface, IBranchUrlInterface {
        public const string BranchKey = "key_live_eg8jUSrrIb5TEDaviMJkXbccCDi0y7wn";

        public static BranchService Instance { get; } = new BranchService();

        private TaskCompletionSource<string> uriTcs;
        private readonly TaskCompletionSource<bool> isAppReadyTcs = new TaskCompletionSource<bool>();

        public async void InitSessionComplete(BranchUniversalObject buo, BranchLinkProperties blp) {
            // Gets the requested myPage and design, then navigates to it.

            this.Log().Debug("Branch initialization completed.");

            var pageName = string.Empty;
            if (blp?.controlParams?.TryGetValue("$deeplink_path", out pageName) ?? false) {
                var myPage = App.GetPages().FirstOrDefault(s => s.ViewType.Name.ToLowerInvariant() == pageName.ToLowerInvariant());
                if (myPage != null) {
                    if (blp.controlParams.TryGetValue("$design", out var designName) && Enum.TryParse(designName, out Design design)) {
                        SamplePageLayout.SetPreferredDesign(design);
                    }

                    _ = await isAppReadyTcs.Task;

                    this.Log().Debug($"Navigating to {myPage.ViewType.Name} from deeplink.");

                    (Application.Current as App)?.ShellNavigateTo(myPage);

                    this.Log().Info($"Navigated to {myPage.ViewType.Name} from deeplink.");
                }
            }
        }

        public void SessionRequestError(BranchError error) => this.Log().Error("Branch error: " + error.ErrorCode + '\n' + error.ErrorMessage);

        /// <summary>
        /// Creates a branch.io link to the specified myPage and design, then shares it.
        /// </summary>
        public async Task ShareSample(MyPage myPage, Design design) {
            var pageName = myPage.ViewType.Name.ToLowerInvariant();
            var buo = new BranchUniversalObject() {
                title = $"{design} {myPage.Title}",
                canonicalIdentifier = Guid.NewGuid().ToString(),
            };

            var blp = new BranchLinkProperties {
                feature = "sharing"
            };
            blp.controlParams["$deeplink_path"] = pageName;
            blp.controlParams["$design"] = design.ToString();

            uriTcs = new TaskCompletionSource<string>();
            Branch.GetInstance().GetShortURL(this, buo, blp);
            var uri = await uriTcs.Task;
            await Share.RequestAsync(new ShareTextRequest {
                Title = "Share Link",
                Text = "Check out this Uno Gallery page!",
                Uri = uri,
            });
            uriTcs = null;
        }

        public void SetIsAppReady() => isAppReadyTcs.TrySetResult(true);

        void IBranchUrlInterface.ReceivedUrl(string uri) => uriTcs?.TrySetResult(uri);

        void IBranchUrlInterface.UrlRequestError(BranchError error) {
        }
    }
}
#endif
