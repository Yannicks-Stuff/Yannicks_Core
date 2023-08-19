using System.Runtime.InteropServices;

namespace Yannick.Native.OS.Windows.Win32;

/// <summary>
/// Provides access to functions from the GDI32.dll library for graphics device interface (GDI) operations.
/// </summary>
public static class Gdi32
{
    /// <summary>
    /// Provides access to functions from the GDI32.dll library for graphics device interface (GDI) operations.
    /// </summary>
    public enum StockObjects
    {
        WHITE_BRUSH = 0,
        LTGRAY_BRUSH = 1,
        GRAY_BRUSH = 2,
        DKGRAY_BRUSH = 3,
        BLACK_BRUSH = 4,
        NULL_BRUSH = 5,
        HOLLOW_BRUSH = NULL_BRUSH,
        WHITE_PEN = 6,
        BLACK_PEN = 7,
        NULL_PEN = 8,
        OEM_FIXED_FONT = 10,
        ANSI_FIXED_FONT = 11,
        ANSI_VAR_FONT = 12,
        SYSTEM_FONT = 13,
        DEVICE_DEFAULT_FONT = 14,
        DEFAULT_PALETTE = 15,
        SYSTEM_FIXED_FONT = 16,
        DEFAULT_GUI_FONT = 17,
        DC_BRUSH = 18,
        DC_PEN = 19,
    }

    public const string Dll = "gdi32.dll";

    /// <summary>
    /// Retrieves a handle to one of the stock pens, brushes, fonts, or palettes.
    /// </summary>
    /// <param name="fnObject">
    /// The type of stock object for which to retrieve a handle.
    /// </param>
    /// <returns>
    /// A handle to the requested logical object, or <see cref="System.IntPtr.Zero"/> if the requested object does not exist.
    /// </returns>
    [DllImport(Dll)]
    public static extern IntPtr GetStockObject(StockObjects fnObject);
}