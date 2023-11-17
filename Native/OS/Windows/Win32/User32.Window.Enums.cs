namespace Yannick.Native.OS.Windows.Win32;

public static partial class User32
{
    [Flags]
    public enum ClassStyles : uint
    {
        /// <summary>Aligns the window's client area on a byte boundary (in the x direction). This style affects the width of the window and its horizontal placement on the display.</summary>  
        ByteAlignClient = 0x1000,

        /// <summary>Aligns the window on a byte boundary (in the x direction). This style affects the width of the window and its horizontal placement on the display.</summary>  
        ByteAlignWindow = 0x2000,

        /// <summary>  
        /// Allocates one device context to be shared by all windows in the class.  
        /// Because window classes are process specific, it is possible for multiple threads of an application to create a window of the same class.  
        /// It is also possible for the threads to attempt to use the device context simultaneously. When this happens, the system allows only one thread to successfully finish its drawing operation.  
        /// </summary>  
        ClassDC = 0x40,

        /// <summary>Sends a double-click message to the window procedure when the user double-clicks the mouse while the cursor is within a window belonging to the class.</summary>  
        DoubleClicks = 0x8,

        /// <summary>  
        /// Enables the drop shadow effect on a window. The effect is turned on and off through SPI_SETDROPSHADOW.  
        /// Typically, this is enabled for small, short-lived windows such as menus to emphasize their Z order relationship to other windows.  
        /// </summary>  
        DropShadow = 0x20000,

        /// <summary>Indicates that the window class is an application global class. For more information, see the "Application Global Classes" section of About Window Classes.</summary>  
        GlobalClass = 0x4000,

        /// <summary>Redraws the entire window if a movement or size adjustment changes the width of the client area.</summary>  
        HorizontalRedraw = 0x2,

        /// <summary>Disables Close on the window menu.</summary>  
        NoClose = 0x200,

        /// <summary>Allocates a unique device context for each window in the class.</summary>  
        OwnDC = 0x20,

        /// <summary>  
        /// Sets the clipping rectangle of the child window to that of the parent window so that the child can draw on the parent.  
        /// A window with the CS_PARENTDC style bit receives a regular device context from the system's cache of device contexts.  
        /// It does not give the child the parent's device context or device context settings. Specifying CS_PARENTDC enhances an application's performance.  
        /// </summary>  
        ParentDC = 0x80,

        /// <summary>  
        /// Saves, as a bitmap, the portion of the screen image obscured by a window of this class.  
        /// When the window is removed, the system uses the saved bitmap to restore the screen image, including other windows that were obscured.  
        /// Therefore, the system does not send WM_PAINT messages to windows that were obscured if the memory used by the bitmap has not been discarded and if other screen actions have not invalidated the stored image.  
        /// This style is useful for small windows (for example, menus or dialog boxes) that are displayed briefly and then removed before other screen activity takes place.  
        /// This style increases the time required to display the window, because the system must first allocate memory to store the bitmap.  
        /// </summary>  
        SaveBits = 0x800,

        /// <summary>Redraws the entire window if a movement or size adjustment changes the height of the client area.</summary>  
        VerticalRedraw = 0x1
    }

    public enum MonitorFlag
    {
        MONITOR_DEFAULTTONULL,
        MONITOR_DEFAULTTOPRIMARY,
        MONITOR_DEFAULTTONEAREST
    }

    public enum MonitorInfoFlag
    {
        MONITORINFOF_NONE,
        MONITORINFOF_PRIMARY
    }

    public enum ShowWindowCommands
    {
        /// <summary>  
        /// Hides the window and activates another window.  
        /// </summary>  
        Hide = 0,

        /// <summary>  
        /// Activates and displays a window. If the window is minimized or   
        /// maximized, the system restores it to its original size and position.  
        /// An application should specify this flag when displaying the window   
        /// for the first time.  
        /// </summary>  
        Normal = 1,

        /// <summary>  
        /// Activates the window and displays it as a minimized window.  
        /// </summary>  
        ShowMinimized = 2,

        /// <summary>  
        /// Maximizes the specified window.  
        /// </summary>  
        Maximize = 3, // is this the right value?  

        /// <summary>  
        /// Activates the window and displays it as a maximized window.  
        /// </summary>         
        ShowMaximized = 3,

        /// <summary>  
        /// Displays a window in its most recent size and position. This value   
        /// is similar to <see cref="Win32.ShowWindowCommand.Normal"/>, except   
        /// the window is not activated.  
        /// </summary>  
        ShowNoActivate = 4,

        /// <summary>  
        /// Activates the window and displays it in its current size and position.   
        /// </summary>  
        Show = 5,

        /// <summary>  
        /// Minimizes the specified window and activates the next top-level   
        /// window in the Z order.  
        /// </summary>  
        Minimize = 6,

        /// <summary>  
        /// Displays the window as a minimized window. This value is similar to  
        /// <see cref="Win32.ShowWindowCommand.ShowMinimized"/>, except the   
        /// window is not activated.  
        /// </summary>  
        ShowMinNoActive = 7,

        /// <summary>  
        /// Displays the window in its current size and position. This value is   
        /// similar to <see cref="Win32.ShowWindowCommand.Show"/>, except the   
        /// window is not activated.  
        /// </summary>  
        ShowNA = 8,

        /// <summary>  
        /// Activates and displays the window. If the window is minimized or   
        /// maximized, the system restores it to its original size and position.   
        /// An application should specify this flag when restoring a minimized window.  
        /// </summary>  
        Restore = 9,

        /// <summary>  
        /// Sets the show state based on the SW_* value specified in the   
        /// STARTUPINFO structure passed to the CreateProcess function by the   
        /// program that started the application.  
        /// </summary>  
        ShowDefault = 10,

        /// <summary>  
        ///  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread   
        /// that owns the window is not responding. This flag should only be   
        /// used when minimizing windows from a different thread.  
        /// </summary>  
        ForceMinimize = 11
    }

    public enum SystemIcons
    {
        IDI_APPLICATION = 32512,
        IDI_HAND = 32513,
        IDI_QUESTION = 32514,
        IDI_EXCLAMATION = 32515,
        IDI_ASTERISK = 32516,
        IDI_WINLOGO = 32517,
        IDI_WARNING = IDI_EXCLAMATION,
        IDI_ERROR = IDI_HAND,
        IDI_INFORMATION = IDI_ASTERISK,
    }

    [Flags]
    public enum WindowLongFlags
    {
        GWL_EXSTYLE = -20,
        GWLP_HINSTANCE = -6,
        GWLP_HWNDPARENT = -8,
        GWLP_ID = -12,
        GWL_STYLE = -16,
        GWLP_USERDATA = -21,
        GWLP_WNDPROC = -4,
        DWLP_DLGPROC = 4,
        DWLP_MSGRESULT = 0,
        DWLP_USER = 8
    }

    [Flags]
    public enum WindowPositionFlags
    {
        ASYNCWINDOWPOS = 0x4000,
        DEFERERASE = 0x2000,
        DRAWFRAME = 0x20,
        FRAMECHANGED = 0x20,
        HIDEWINDOW = 0x80,
        NOACTIVATE = 0x10,
        NOCOPYBITS = 0x100,
        NOMOVE = 2,
        NOOWNERZORDER = 0x200,
        NOREDRAW = 8,
        NOREPOSITION = 0x200,
        NOSENDCHANGING = 0x400,
        NOSIZE = 1,
        NOZORDER = 4,
        SHOWWINDOW = 0x40
    }

    [Flags]
    public enum WindowStyles : uint
    {
        /// <summary>The window has a thin-line border.</summary>  
        WS_BORDER = 0x800000,

        /// <summary>The window has a title bar (includes the WS_BORDER style).</summary>  
        WS_CAPTION = 0xc00000,

        /// <summary>The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the WS_POPUP style.</summary>  
        WS_CHILD = 0x40000000,

        /// <summary>Excludes the area occupied by child windows when drawing occurs within the parent window. This style is used when creating the parent window.</summary>  
        WS_CLIPCHILDREN = 0x2000000,

        /// <summary>  
        /// Clips child windows relative to each other; that is, when a particular child window receives a WM_PAINT message, the WS_CLIPSIBLINGS style clips all other overlapping child windows out of the region of the child window to be updated.  
        /// If WS_CLIPSIBLINGS is not specified and child windows overlap, it is possible, when drawing within the client area of a child window, to draw within the client area of a neighboring child window.  
        /// </summary>  
        WS_CLIPSIBLINGS = 0x4000000,

        /// <summary>The window is initially disabled. A disabled window cannot receive input from the user. To change this after a window has been created, use the EnableWindow function.</summary>  
        WS_DISABLED = 0x8000000,

        /// <summary>The window has a border of a style typically used with dialog boxes. A window with this style cannot have a title bar.</summary>  
        WS_DLGFRAME = 0x400000,

        /// <summary>  
        /// The window is the first control of a group of controls. The group consists of this first control and all controls defined after it, up to the next control with the WS_GROUP style.  
        /// The first control in each group usually has the WS_TABSTOP style so that the user can move from group to group. The user can subsequently change the keyboard focus from one control in the group to the next control in the group by using the direction keys.  
        /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.  
        /// </summary>  
        WS_GROUP = 0x20000,

        /// <summary>The window has a horizontal scroll bar.</summary>  
        WS_HSCROLL = 0x100000,

        /// <summary>The window is initially maximized.</summary>   
        WS_MAXIMIZE = 0x1000000,

        /// <summary>The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.</summary>   
        WS_MAXIMIZEBOX = 0x10000,

        /// <summary>The window is initially minimized.</summary>  
        WS_MINIMIZE = 0x20000000,

        /// <summary>The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.</summary>  
        WS_MINIMIZEBOX = 0x20000,

        /// <summary>The window is an overlapped window. An overlapped window has a title bar and a border.</summary>  
        WS_OVERLAPPED = 0x0,

        /// <summary>The window is an overlapped window.</summary>  
        WS_OVERLAPPEDWINDOW =
            WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_SIZEFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,

        /// <summary>The window is a pop-up window. This style cannot be used with the WS_CHILD style.</summary>  
        WS_POPUP = 0x80000000u,

        /// <summary>The window is a pop-up window. The WS_CAPTION and WS_POPUPWINDOW styles must be combined to make the window menu visible.</summary>  
        WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,

        /// <summary>The window has a sizing border.</summary>  
        WS_SIZEFRAME = 0x40000,

        /// <summary>The window has a window menu on its title bar. The WS_CAPTION style must also be specified.</summary>  
        WS_SYSMENU = 0x80000,

        /// <summary>  
        /// The window is a control that can receive the keyboard focus when the user presses the TAB key.  
        /// Pressing the TAB key changes the keyboard focus to the next control with the WS_TABSTOP style.    
        /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.  
        /// For user-created windows and modeless dialogs to work with tab stops, alter the message loop to call the IsDialogMessage function.  
        /// </summary>  
        WS_TABSTOP = 0x10000,

        /// <summary>The window is initially visible. This style can be turned on and off by using the ShowWindow or SetWindowPos function.</summary>  
        WS_VISIBLE = 0x10000000,

        /// <summary>The window has a vertical scroll bar.</summary>  
        WS_VSCROLL = 0x200000
    }

    [Flags]
    public enum WindowStylesEx : uint
    {
        /// <summary>  
        /// Specifies that a window created with this style accepts drag-drop files.  
        /// </summary>  
        WS_EX_ACCEPTFILES = 0x00000010,

        /// <summary>  
        /// Forces a top-level window onto the taskbar when the window is visible.  
        /// </summary>  
        WS_EX_APPWINDOW = 0x00040000,

        /// <summary>  
        /// Specifies that a window has a border with a sunken edge.  
        /// </summary>  
        WS_EX_CLIENTEDGE = 0x00000200,

        /// <summary>  
        /// Windows XP: Paints all descendants of a window in bottom-to-top painting order using double-buffering. For more information, see Remarks. This cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC.   
        /// </summary>  
        WS_EX_COMPOSITED = 0x02000000,

        /// <summary>  
        /// Includes a question mark in the title bar of the window. When the user clicks the question mark, the cursor changes to a question mark with a pointer. If the user then clicks a child window, the child receives a WM_HELP message. The child window should pass the message to the parent window procedure, which should call the WinHelp function using the HELP_WM_HELP command. The Help application displays a pop-up window that typically contains help for the child window.  
        /// WS_EX_CONTEXTHELP cannot be used with the WS_MAXIMIZEBOX or WS_MINIMIZEBOX styles.  
        /// </summary>  
        WS_EX_CONTEXTHELP = 0x00000400,

        /// <summary>  
        /// The window itself contains child windows that should take part in dialog box navigation. If this style is specified, the dialog manager recurses into children of this window when performing navigation operations such as handling the TAB key, an arrow key, or a keyboard mnemonic.  
        /// </summary>  
        WS_EX_CONTROLPARENT = 0x00010000,

        /// <summary>  
        /// Creates a window that has a double border; the window can, optionally, be created with a title bar by specifying the WS_CAPTION style in the dwStyle parameter.  
        /// </summary>  
        WS_EX_DLGMODALFRAME = 0x00000001,

        /// <summary>  
        /// Windows 2000/XP: Creates a layered window. Note that this cannot be used for child windows. Also, this cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC.   
        /// </summary>  
        WS_EX_LAYERED = 0x00080000,

        /// <summary>  
        /// Arabic and Hebrew versions of Windows 98/Me, Windows 2000/XP: Creates a window whose horizontal origin is on the right edge. Increasing horizontal values advance to the left.   
        /// </summary>  
        WS_EX_LAYOUTRTL = 0x00400000,

        /// <summary>  
        /// Creates a window that has generic left-aligned properties. This is the default.  
        /// </summary>  
        WS_EX_LEFT = 0x00000000,

        /// <summary>  
        /// If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the vertical scroll bar (if present) is to the left of the client area. For other languages, the style is ignored.  
        /// </summary>  
        WS_EX_LEFTSCROLLBAR = 0x00004000,

        /// <summary>  
        /// The window text is displayed using left-to-right reading-order properties. This is the default.  
        /// </summary>  
        WS_EX_LTRREADING = 0x00000000,

        /// <summary>  
        /// Creates a multiple-document interface (MDI) child window.  
        /// </summary>  
        WS_EX_MDICHILD = 0x00000040,

        /// <summary>  
        /// Windows 2000/XP: A top-level window created with this style does not become the foreground window when the user clicks it. The system does not bring this window to the foreground when the user minimizes or closes the foreground window.   
        /// To activate the window, use the SetActiveWindow or SetForegroundWindow function.  
        /// The window does not appear on the taskbar by default. To force the window to appear on the taskbar, use the WS_EX_APPWINDOW style.  
        /// </summary>  
        WS_EX_NOACTIVATE = 0x08000000,

        /// <summary>  
        /// Windows 2000/XP: A window created with this style does not pass its window layout to its child windows.  
        /// </summary>  
        WS_EX_NOINHERITLAYOUT = 0x00100000,

        /// <summary>  
        /// Specifies that a child window created with this style does not send the WM_PARENTNOTIFY message to its parent window when it is created or destroyed.  
        /// </summary>  
        WS_EX_NOPARENTNOTIFY = 0x00000004,

        /// <summary>  
        /// Combines the WS_EX_CLIENTEDGE and WS_EX_WINDOWEDGE styles.  
        /// </summary>  
        WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,

        /// <summary>  
        /// Combines the WS_EX_WINDOWEDGE, WS_EX_TOOLWINDOW, and WS_EX_TOPMOST styles.  
        /// </summary>  
        WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,

        /// <summary>  
        /// The window has generic "right-aligned" properties. This depends on the window class. This style has an effect only if the shell language is Hebrew, Arabic, or another language that supports reading-order alignment; otherwise, the style is ignored.  
        /// Using the WS_EX_RIGHT style for static or edit controls has the same effect as using the SS_RIGHT or ES_RIGHT style, respectively. Using this style with button controls has the same effect as using BS_RIGHT and BS_RIGHTBUTTON styles.  
        /// </summary>  
        WS_EX_RIGHT = 0x00001000,

        /// <summary>  
        /// Vertical scroll bar (if present) is to the right of the client area. This is the default.  
        /// </summary>  
        WS_EX_RIGHTSCROLLBAR = 0x00000000,

        /// <summary>  
        /// If the shell language is Hebrew, Arabic, or another language that supports reading-order alignment, the window text is displayed using right-to-left reading-order properties. For other languages, the style is ignored.  
        /// </summary>  
        WS_EX_RTLREADING = 0x00002000,

        /// <summary>  
        /// Creates a window with a three-dimensional border style intended to be used for items that do not accept user input.  
        /// </summary>  
        WS_EX_STATICEDGE = 0x00020000,

        /// <summary>  
        /// Creates a tool window; that is, a window intended to be used as a floating toolbar. A tool window has a title bar that is shorter than a normal title bar, and the window title is drawn using a smaller font. A tool window does not appear in the taskbar or in the dialog that appears when the user presses ALT+TAB. If a tool window has a system menu, its icon is not displayed on the title bar. However, you can display the system menu by right-clicking or by typing ALT+SPACE.   
        /// </summary>  
        WS_EX_TOOLWINDOW = 0x00000080,

        /// <summary>  
        /// Specifies that a window created with this style should be placed above all non-topmost windows and should stay above them, even when the window is deactivated. To add or remove this style, use the SetWindowPos function.  
        /// </summary>  
        WS_EX_TOPMOST = 0x00000008,

        /// <summary>  
        /// Specifies that a window created with this style should not be painted until siblings beneath the window (that were created by the same thread) have been painted. The window appears transparent because the bits of underlying sibling windows have already been painted.  
        /// To achieve transparency without these restrictions, use the SetWindowRgn function.  
        /// </summary>  
        WS_EX_TRANSPARENT = 0x00000020,

        /// <summary>  
        /// Specifies that a window has a border with a raised edge.  
        /// </summary>  
        WS_EX_WINDOWEDGE = 0x00000100
    }
}