using GLib;
using System;
using Uno.UI.Runtime.Skia;

namespace YourWaifu2x.Skia.Gtk
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ExceptionManager.UnhandledException += delegate (UnhandledExceptionArgs expArgs)
            {
                Console.WriteLine("GLIB UNHANDLED EXCEPTION" + expArgs.ExceptionObject.ToString());
                expArgs.ExitApplication = true;
            };

            Windows.ApplicationModel.Resources.ResourceLoader.GetStringInternal = s => null;

            GtkHost host = new GtkHost(() => new App(), args);

            host.Run();
        }
    }
}
