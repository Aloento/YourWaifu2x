using AppKit;

namespace YourWaifu2x.macOS
{
    internal static class MenuHelper
    {
        private const string QuitTitle = "Quit";
        private const string QuitCharCode = "q";

        internal static NSMenu GetMenu()
        {
            NSMenu menubar = new NSMenu();
            NSMenuItem appMenuItem = new NSMenuItem();
            menubar.AddItem(appMenuItem);

            NSMenu appMenu = new NSMenu();

            NSMenuItem quitMenuItem = new NSMenuItem(QuitTitle, QuitCharCode, delegate
            {
                NSApplication.SharedApplication.Terminate(menubar);
            });

            appMenu.AddItem(quitMenuItem);
            appMenuItem.Submenu = appMenu;

            return menubar;
        }
    }
}