using Windows.UI.Xaml.Controls;

namespace YourWaifu2x.Views.Samples {
    // There is no WebView implementation in WebAssembly
#if !__WASM__ && !__SKIA__
    [Page(PageCategory.Features, "WebView", Description = "This control hosts a web page or HTML content within an application.", DocumentationLink = "https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.webview")]
#endif
    public sealed partial class WebViewSamplePage : Page {
        public WebViewSamplePage() {
            InitializeComponent();
        }
    }
}
