namespace YourWaifu2x.macOS {
    using AppKit;

    internal static class MainClass {
        private static void Main(string[] args) {
            NSApplication.Init();
            NSApplication.SharedApplication.MainMenu = MenuHelper.GetMenu();
            NSApplication.SharedApplication.Delegate = new App();
            NSApplication.Main(args);
        }
    }
}

