﻿namespace YourWaifu2x
{
    public static class AnalyticsService
    {
        public static void Initialize()
        {
#if !DEBUG && __WASM__
			Uno.Foundation.WebAssemblyRuntime.InvokeJS("Uno.UI.Demo.Analytics.reportPageView('main');");
#endif
        }

        public static void TrackView(string viewName)
        {
#if !DEBUG && __WASM__
			Uno.Foundation.WebAssemblyRuntime.InvokeJS($"Uno.UI.Demo.Analytics.reportPageView('{viewName}');");
#endif
        }
    }
}
