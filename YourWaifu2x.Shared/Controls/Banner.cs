namespace YourWaifu2x {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Uno.Disposables;
    using Windows.System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public partial class Banner : Control {
        private const string HowItWorkButtonPart = "PART_HowItWorkButton";
        private const string CodeSampleButtonPart = "PART_CodeSampleButton";
        private const string ShowCasesButtonPart = "PART_ShowCasesButton";
        private const string DocsButtonPart = "PART_DocsButton";
        private const string BlogButtonPart = "PART_BlogButton";
        private const string ContactButtonPart = "PART_ContactButton";
        private const string GetStartedButtonPart = "PART_GetStartedButton";

        private static readonly Uri HowItWorkLink = new Uri("https://platform.uno/how-it-works/");
        private static readonly Uri CodeSampleButtonLink = new Uri("https://platform.uno/code-samples/");
        private static readonly Uri ShowCasesButtonLink = new Uri("https://platform.uno/showcases/");
        private static readonly Uri DocsButtonLink = new Uri("https://platform.uno/docs/articles/intro.html");
        private static readonly Uri BlogButtonLink = new Uri("https://platform.uno/blog/");
        private static readonly Uri ContactButtonLink = new Uri("https://platform.uno/contact/#");
        private static readonly Uri GetStartedButtonLink = new Uri("https://platform.uno/docs/articles/getting-started-tutorial-1.html");

        private Button howItWorksButton;
        private Button codeSampleButton;
        private Button showCasesButton;
        private Button docsButton;
        private Button blogButton;
        private Button contactButton;
        private Button getStartedButton;

        private readonly SerialDisposable subscriptions = new SerialDisposable();

        private IReadOnlyCollection<BannerButton> BannerButtons => new List<BannerButton>
        {
            new BannerButton(howItWorksButton, HowItWorkLink),
            new BannerButton(codeSampleButton, CodeSampleButtonLink),
            new BannerButton(showCasesButton, ShowCasesButtonLink),
            new BannerButton(docsButton, DocsButtonLink),
            new BannerButton(blogButton, BlogButtonLink),
            new BannerButton(contactButton, ContactButtonLink),
            new BannerButton(getStartedButton, GetStartedButtonLink),
        };

        protected override void OnApplyTemplate() {
            base.OnApplyTemplate();

            howItWorksButton = (Button)GetTemplateChild(HowItWorkButtonPart);
            codeSampleButton = (Button)GetTemplateChild(CodeSampleButtonPart);
            showCasesButton = (Button)GetTemplateChild(ShowCasesButtonPart);
            docsButton = (Button)GetTemplateChild(DocsButtonPart);
            blogButton = (Button)GetTemplateChild(BlogButtonPart);
            contactButton = (Button)GetTemplateChild(ContactButtonPart);
            getStartedButton = (Button)GetTemplateChild(GetStartedButtonPart);

            var disposables = new CompositeDisposable();
            subscriptions.Disposable = disposables;

            BindOnClick(howItWorksButton);
            BindOnClick(codeSampleButton);
            BindOnClick(showCasesButton);
            BindOnClick(docsButton);
            BindOnClick(blogButton);
            BindOnClick(contactButton);
            BindOnClick(getStartedButton);

            void BindOnClick(Button button) {
                button.Click += OnBannerButtonClicked;
                _ = Disposable
                    .Create(() => button.Click -= OnBannerButtonClicked)
                    .DisposeWith(disposables);
            }
        }

        private void OnBannerButtonClicked(object sender, RoutedEventArgs e) {
            if (sender is Button button && BannerButtons.FirstOrDefault(x => x.Button == button) is BannerButton buttonBanner) {
                _ = Launcher.LaunchUriAsync(buttonBanner.Url);
            }
        }

        private class BannerButton {
            public Button Button { get; set; }

            public Uri Url { get; set; }

            public BannerButton(Button button, Uri url) {
                Button = button;
                Url = url;
            }
        }
    }
}
