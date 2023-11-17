using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Yannick.Native.OS.Windows.Win32;

namespace Yannick.Native.OS.Windows.UI;

/// <summary>
/// Provides a static class for displaying a message box that can contain text, buttons, and symbols to inform and instruct the user.
/// </summary>
public static class MessageBox
{
    public enum DefaultButton
    {
        BUTTON1 = 0x00000000,
        BUTTON2 = 0x00000100,
        BUTTON3 = 0x00000200,
        BUTTON4 = 0x00000300,
    }

    public enum DialogBoxType
    {
        /// <summary>
        /// The user must respond to the message box before continuing work
        /// <remarks>Depending on the hierarchy of windows in the application,
        /// the user may be able to move to other windows within the thread.
        /// All child windows of the parent of the message box are automatically disabled,
        /// but pop-up windows are not.</remarks>
        /// </summary>
        APPLMODAL = 0x00000000,

        /// <summary>
        /// Same as <see cref="APPLMODAL"/> except that the message box has the WS_EX_TOPMOST style
        /// </summary>
        SYSTEMMODAL = 0x00001000,

        /// <summary>
        /// Same as <see cref="APPLMODAL"/> except that all the top-level windows belonging to the current thread are disabled
        /// </summary>
        TASKMODAL = 0x00002000
    }

    [Flags]
    public enum ExtraOptions
    {
        /// <summary>
        /// Same as desktop of the interactive window station
        /// </summary>
        DEFAULT_DESKTOP_ONLY = 0x00020000,

        /// <summary>
        /// The text is right-justified
        /// </summary>
        RIGHT = 0x00080000,

        /// <summary>
        /// Displays message and caption text using right-to-left reading order on Hebrew and Arabic systems
        /// </summary>
        RTLREADING = 0x00100000,

        /// <summary>
        /// The message box becomes the foreground window.
        /// </summary>
        SETFOREGROUND = 0x00010000,

        /// <summary>
        /// The message box is created with the WS_EX_TOPMOST window style
        /// </summary>
        TOPMOST = 0x00040000,

        /// <summary>
        /// The caller is a service notifying the user of an event
        /// </summary>
        SERVICE_NOTIFICATION = 0x00200000
    }

    public enum Icon
    {
        /// <summary>
        /// An exclamation-point icon appears in the message box
        /// </summary>
        EXCLAMATION = 0x00000030,

        /// <summary>
        /// An exclamation-point icon appears in the message box
        /// </summary>
        WARNING = 0x00000030,

        /// <summary>
        /// An icon consisting of a lowercase letter i in a circle appears in the message box
        /// </summary>
        INFORMATION = 0x00000040,

        /// <summary>
        /// An icon consisting of a lowercase letter i in a circle appears in the message box
        /// </summary>
        ASTERISK = 0x00000040,

        /// <summary>
        /// A question-mark icon appears in the message box
        /// </summary>
        QUESTION = 0x00000020,

        /// <summary>
        /// A stop-sign icon appears in the message box
        /// </summary>
        STOP = 0x00000010,

        /// <summary>
        /// A stop-sign icon appears in the message box
        /// </summary>
        ERROR = 0x00000010,

        /// <summary>
        /// A stop-sign icon appears in the message box
        /// </summary>
        HAND = 0x00000010
    }

    public enum ReturnValue
    {
        ABORD = 3,
        CANCEL = 2,
        CONTINUE = 11,
        IGNORE = 5,
        NO = 7,
        OK = 1,
        RETRY = 4,
        TRY_AGAIN = 10,
        YES = 6
    }

    [Flags]
    public enum Style
    {
        /// <summary>
        /// The message box contains three push buttons: Abort, Retry, and Ignore.
        /// </summary>
        ABORT_RETRY_IGNORE = 0x00000002,

        /// <summary>
        /// The message box contains three push buttons: Cancel, Try Again, Continue. Use this message box type instead of <permission cref="ABORT_RETRY_IGNORE"></permission>.
        /// </summary>
        CANCEL_TRY_CONTINUE = 0x00000006,

        /// <summary>
        /// Adds a Help button to the message box. When the user clicks the Help button or presses F1, the system sends a WM_HELP message to the owner
        /// </summary>
        HELP = 0x00004000,

        /// <summary>
        /// The message box contains one push button: OK. This is the default.
        /// </summary>
        OK = 0x00000000,

        /// <summary>
        /// The message box contains two push buttons: OK and Cancel.
        /// </summary>
        OK_CANCEL = 0x00000001,

        /// <summary>
        /// The message box contains two push buttons: Retry and Cancel.
        /// </summary>
        RETRY_CANCEL = 0x00000005,

        /// <summary>
        /// The message box contains two push buttons: Yes and No.
        /// </summary>
        YES_NO = 0x00000004,

        /// <summary>
        /// The message box contains three push buttons: Yes, No, and Cancel.
        /// </summary>
        YES_NO_CANCEL = 0x00000003
    }

    /// <summary>
    /// Displays a modal dialog box that contains a system icon, a set of buttons, and a brief application-specific message, such as status or error information.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="title">The text to display in the title bar of the message box.</param>
    /// <param name="style">Specifies the set of buttons that are displayed on the message box. Default is <see cref="Style.OK"/>.</param>
    /// <param name="icon">Specifies the icon that is displayed on the message box. Default is <see cref="Icon.INFORMATION"/>.</param>
    /// <param name="defaultButton">Specifies the default button for the message box. Default is <see cref="DefaultButton.BUTTON1"/>.</param>
    /// <param name="dialogBoxType">Specifies the modality of the dialog box. Default is <see cref="DialogBoxType.APPLMODAL"/>.</param>
    /// <param name="options">Specifies extra options for the message box. Default is null.</param>
    /// <returns>Returns an enumeration value that specifies which button the user clicked.</returns>
    /// <exception cref="Win32Exception">Thrown when the message box cannot be displayed.</exception>
    public static ReturnValue Show(string text, string title, Style style = Style.OK, Icon icon = Icon.INFORMATION,
        DefaultButton defaultButton = DefaultButton.BUTTON1,
        DialogBoxType dialogBoxType = DialogBoxType.APPLMODAL, ExtraOptions? options = null)
    {
        var rs = User32.MessageBox(Process.GetCurrentProcess().MainWindowHandle, text, title,
            ((uint)style | (uint)icon) |
            (uint)defaultButton | (uint)dialogBoxType |
            (options == null ? 0u : (uint)options));

        if (rs == 0)
            throw new Win32Exception(Marshal.GetLastWin32Error());

        return (ReturnValue)rs;
    }
}