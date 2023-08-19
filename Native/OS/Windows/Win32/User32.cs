using System.Runtime.InteropServices;

namespace Yannick.Native.OS.Windows.Win32;

public static partial class User32
{
    private const string Dll = "user32.dll";


    /// <summary>
    /// Displays a modal dialog box that contains an application-defined message.
    /// </summary>
    /// <param name="hWnd">A handle to the owner window of the MessageBox. Can be IntPtr.Zero.</param>
    /// <param name="lpText">The message to be displayed.</param>
    /// <param name="lpCaption">The text to be displayed in the title bar of the MessageBox.</param>
    /// <param name="uType">Specifies the contents and behavior of the dialog box.</param>
    /// <returns>
    /// If the function succeeds, the return value is the menu-item:
    /// </returns>
    /// <remarks>
    /// For a wrapper version view <see cref="Yannick.Native.OS.Windows.UI.MessageBox"/>
    /// </remarks>
    [DllImport(Dll, CharSet = CharSet.Unicode)]
    public static extern uint MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);

    /// <summary>
    /// Indicates to the system that a thread has made a request to terminate (quit).
    /// </summary>
    /// <param name="nExitCode">The exit code for the thread. Use the GetExitCodeThread function to retrieve a thread's exit value.</param>
    [DllImport(Dll)]
    public static extern void PostQuitMessage(int nExitCode);

    /// <summary>
    /// Calls the default window procedure to provide default processing for any window messages that an application does not process.
    /// </summary>
    /// <param name="hWnd">A handle to the window procedure that received the message.</param>
    /// <param name="uMsg">The message.</param>
    /// <param name="wParam">Additional message-specific information.</param>
    /// <param name="lParam">Additional message-specific information.</param>
    /// <returns>The return value is the result of the message processing and depends on the message.</returns>
    [DllImport(Dll)]
    public static extern IntPtr DefWindowProc(IntPtr hWnd, WM uMsg, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// Retrieves a handle to the display monitor that has the largest area of intersection with the bounding rectangle of a specified window.
    /// </summary>
    /// <param name="hwnd">A handle to the window of interest.</param>
    /// <param name="dwFlags">Determines the function's return value if the window does not intersect any display monitor.</param>
    /// <returns>Handle to the display monitor of interest.</returns>
    [DllImport(Dll, ExactSpelling = true)]
    public static extern IntPtr MonitorFromWindow(IntPtr hwnd, MonitorFlag dwFlags);

    /// <summary>
    /// Retrieves information about a display monitor.
    /// </summary>
    /// <param name="hMonitor">A handle to the display monitor of interest.</param>
    /// <param name="lpmi">A reference to a MonitorInfo structure that receives information about the specified display monitor.</param>
    /// <returns>If the function succeeds, the return value is true. If the function fails, the return value is false.</returns>
    [DllImport(Dll, CharSet = CharSet.Unicode)]
    public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MonitorInfo lpmi);
}