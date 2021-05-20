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
            // Gets the requested sample and design, then navigates to it.

            this.Log().Debug("Branch initialization completed.");

            var pageName = string.Empty;
            if (blp?.controlParams?.TryGetValue("$deeplink_path", out pageName) ?? false) {
                var sample = App.GetSamples().FirstOrDefault(s => s.ViewType.Name.ToLowerInvariant() == pageName.ToLowerInvariant());
                if (sample != null) {
                    if (blp.controlParams.TryGetValue("$design", out var designName) && Enum.TryParse(designName, out Design design)) {
                        SamplePageLayout.SetPreferredDesign(design);
                    }

                    _ = await isAppReadyTcs.Task;

                    this.Log().Debug($"Navigating to {sample.ViewType.Name} from deeplink.");

                    (Application.Current as App)?.ShellNavigateTo(sample);

                    this.Log().Info($"Navigated to {sample.ViewType.Name} from deeplink.");
                }
            }
        }

        public void SessionRequestError(BranchError error) => this.Log().Error("Branch error: " + error.ErrorCode + '\n' + error.ErrorMessage);

        /// <summary>
        /// Creates a branch.io link to the specified sample and design, then shares it.
        /// </summary>
        public async Task ShareSample(Sample sample, Design design) {
            var pageName = sample.ViewType.Name.ToLowerInvariant();
            var buo = new BranchUniversalObject() {
                title = $"{design} {sample.Title}",
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
