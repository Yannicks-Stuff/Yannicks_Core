using System.Runtime.InteropServices;

namespace Yannick.Native.OS.Windows.Win32;

public static class User32
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
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern uint MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);
}