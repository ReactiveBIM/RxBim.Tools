namespace RxBim.Tools.Autocad
{
    using System;
    using System.Windows;
    using JetBrains.Annotations;
    using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

    /// <summary>
    /// Extensions for WPF windows
    /// </summary>
    [PublicAPI]
    public static class WindowExtensions
    {
        /// <summary>
        /// Displays the window as modal and centered relative to the main AutoCAD window.
        /// Returns the result of the dialog after it is closed.
        /// </summary>
        /// <param name="window">Window</param>
        public static bool? ShowAsModalCentered(this Window window)
        {
            FixStartupLocation(window);
            AddActivatedEvent(window);
            return Application.ShowModalWindow(null, window, false);
        }

        /// <summary>
        /// Displays the window as a modeless dialog centered on the main AutoCAD window.
        /// </summary>
        /// <param name="window">Window</param>
        public static void ShowAsModelessCentered(this Window window)
        {
            FixStartupLocation(window);
            AddActivatedEvent(window);
            Application.ShowModelessWindow(null, window, false);
        }

        private static void FixStartupLocation(Window window)
        {
            if (window.WindowStartupLocation != WindowStartupLocation.CenterOwner)
            {
                window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
        }

        private static void AddActivatedEvent(Window window)
        {
            window.Activated -= WindowActivated;
            window.Activated += WindowActivated;
        }

        private static void WindowActivated(object? sender, EventArgs e)
        {
            var window = (Window)sender;
            window.Activated -= WindowActivated;
            var loc = Application.MainWindow.DeviceIndependentLocation;
            var size = Application.MainWindow.DeviceIndependentSize;
            window.Top = loc.Y + size.Height / 2 - window.ActualHeight / 2;
            window.Left = loc.X + size.Width / 2 - window.ActualWidth / 2;
        }
    }
}