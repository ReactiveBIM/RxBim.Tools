namespace RxBim.Tools.TableBuilder;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// Extensions for Excel.
/// </summary>
public static class ExcelExtensions
{
    /// <summary>
    /// Standard DPI.
    /// </summary>
    private const int StandardDpi = 96;

    /// <summary>
    /// Is DPI calculated.
    /// </summary>
    private static bool _isDpiCalculated;

    /// <summary>
    /// DPI by X.
    /// </summary>
    private static uint _dpiX;

    /// <summary>
    /// DPI by Y.
    /// </summary>
    private static uint _dpiY;

    /// <summary>
    /// Returns the width in pixels.
    /// </summary>
    /// <param name="width">Width in Excel points.</param>
    public static double ExcelWidthToPixels(this double width)
    {
        // DPI (screen resolution) is used to determine the width and height of Excel in pixels.
        CalculateDpi();

        // Magic constants for determining column width and row height in pixels (from Google).
        // https://github.com/ClosedXML/ClosedXML/issues/846
        return (width * 7 + 12) / ((double)StandardDpi / _dpiX);
    }

    /// <summary>
    /// Returns the height in pixels.
    /// </summary>
    /// <param name="height">Height in Excel points.</param>
    public static double ExcelHeightToPixels(this double height)
    {
        CalculateDpi();

        // https://learn.microsoft.com/en-us/answers/questions/257675/excel-row-height-logic-calculation
        return height / 0.75 / ((double)StandardDpi / _dpiY);
    }

    [DllImport("shcore.dll")]
    private static extern int GetDpiForMonitor(IntPtr hmonitor, int dpiType, out uint dpiX, out uint dpiY);

    [DllImport("user32.dll")]
    private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

    [DllImport("user32.dll")]
    private static extern IntPtr GetDesktopWindow();

    private static void CalculateDpi()
    {
        if (_isDpiCalculated)
            return;

        // Determining dpi via Graphics does not work (does not find the System.Drawing.Common assembly).
        var hwnd = GetDesktopWindow();
        var hMonitor = MonitorFromWindow(hwnd, 0);
        GetDpiForMonitor(hMonitor, 0, out _dpiX, out _dpiY);

        if (_dpiX == 0)
            _dpiX = StandardDpi;

        if (_dpiY == 0)
            _dpiY = StandardDpi;

        _isDpiCalculated = true;
    }
}