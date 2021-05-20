namespace YourWaifu2x.Skia.Gtk {
    using GLib;
    using System;
    using Uno.UI.Runtime.Skia;

    internal class Program {
        private static void Main(string[] args) {
            ExceptionManager.UnhandledException += delegate (UnhandledExceptionArgs expArgs) {
                Console.WriteLine("GLIB UNHANDLED EXCEPTION" + expArgs.ExceptionObject.ToString());
                expArgs.ExitApplication = true;
            };

            Windows.ApplicationModel.Resources.ResourceLoader.GetStringInternal = s => null;

            var host = new GtkHost(() => new App(), args);

            host.Run();
        }
    }
}
