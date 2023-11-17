using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Yannick.Native.OS.Windows.Win32;

public static partial class User32
{
    public delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    public const int
        IDC_ARROW = 32512,
        IDC_IBEAM = 32513,
        IDC_WAIT = 32514,
        IDC_CROSS = 32515,
        IDC_UPARROW = 32516,
        IDC_SIZE = 32640,
        IDC_ICON = 32641,
        IDC_SIZENWSE = 32642,
        IDC_SIZENESW = 32643,
        IDC_SIZEWE = 32644,
        IDC_SIZENS = 32645,
        IDC_SIZEALL = 32646,
        IDC_NO = 32648,
        IDC_HAND = 32649,
        IDC_APPSTARTING = 32650,
        IDC_HELP = 32651,
        CW_USEDEFAULT = -1;


    /// <summary>
    /// Updates the client area of the specified window by sending a WM_PAINT message to the window 
    /// if the window's update region is not empty. The function sends a WM_PAINT message directly to 
    /// the window procedure of the specified window, bypassing the application queue.
    /// </summary>
    /// <param name="hWnd">A handle to the window to be updated.</param>
    /// <returns>
    /// If the function succeeds, the return value is true. If the function fails, the return value is false.
    /// </returns>
    [DllImport(Dll)]
    public static extern bool UpdateWindow(IntPtr hWnd);

    /// <summary>
    /// Loads the specified cursor resource from the executable (.EXE) file associated with an instance of a module.
    /// </summary>
    /// <param name="hInstance">A handle to an instance of the module whose executable file contains the cursor to be loaded.</param>
    /// <param name="lpCursorName">The name of the cursor resource to be loaded.</param>
    /// <returns>A handle to the newly loaded cursor. If the function fails, the return value is null.</returns>
    [DllImport(Dll)]
    public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

    /// <summary>
    /// Registers a window class for subsequent use in calls to the CreateWindow or CreateWindowEx function.
    /// </summary>
    /// <param name="lpwcx">A pointer to a WNDCLASSEX structure that contains the information about the class of windows.</param>
    /// <returns>
    /// If the function succeeds, the return value is a class atom that uniquely identifies the class being registered. 
    /// This atom can only be used by the CreateWindow, CreateWindowEx, GetClassInfo, GetClassInfoEx, FindWindow, FindWindowEx, 
    /// and UnregisterClass functions and the IActiveIMMap::FilterClientWindows method. 
    /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
    /// </returns>
    [DllImport(Dll, SetLastError = true, EntryPoint = "RegisterClassEx")]
    public static extern ushort RegisterClassEx2([In] ref WNDCLASSEX lpwcx);

    /// <summary>
    /// Destroys the specified window. The function sends WM_DESTROY and WM_NCDESTROY messages to the window to deactivate it and remove the keyboard focus from it.
    /// </summary>
    /// <param name="hWnd">A handle to the window to be destroyed.</param>
    /// <returns>If the function succeeds, the return value is true. If the function fails, the return value is false. To get extended error information, call GetLastError.</returns>
    [DllImport(Dll)]
    public static extern bool DestroyWindow(IntPtr hWnd);

    /// <summary>
    /// Retrieves the length, in characters, of the specified window's title bar text (if the window has a title bar). If the specified window is a control, the function retrieves the length of the text within the control.
    /// </summary>
    /// <param name="hWnd">A handle to the window or control.</param>
    /// <returns>The length, in characters, of the text. Under certain conditions, this value may be greater than the length of the text. For more information, see the following Remarks section.</returns>
    [DllImport(Dll, CharSet = CharSet.Unicode)]
    public static extern int GetWindowTextLength(IntPtr hWnd);

    /// <summary>
    /// Copies the text of the specified window's title bar (if it has one) into a buffer. If the specified window is a control, the function copies the text of the control into the buffer.
    /// </summary>
    /// <param name="hWnd">A handle to the window or control containing the text.</param>
    /// <param name="lpString">The buffer that will receive the text. If the string is as long or longer than the buffer, the string is truncated and terminated with a null character.</param>
    /// <param name="nMaxCount">The maximum number of characters to copy to the buffer, including the null character.</param>
    /// <returns>If the function succeeds, the return value is the length, in characters, of the copied string, not including the terminating null character. If the window has no title bar or text, if the title bar is empty, or if the window or control handle is invalid, the return value is zero. To get extended error information, call GetLastError.</returns>
    [DllImport(Dll, SetLastError = true)]
    public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    /// <summary>
    /// Changes the text of the specified window's title bar (if it has one). 
    /// If the specified window is a control, the text of the control is changed.
    /// </summary>
    /// <param name="hWnd">A handle to the window or control whose text is to be changed.</param>
    /// <param name="lpString">The new title or control text.</param>
    /// <returns>If the function succeeds, the return value is true; otherwise, it is false.</returns>
    [DllImport(Dll)]
    public static extern bool SetWindowText(IntPtr hWnd, string lpString);

    /// <summary>
    /// Changes the size, position, and Z order of a child, pop-up, or top-level window.
    /// </summary>
    /// <param name="hwnd">A handle to the window.</param>
    /// <param name="hWndInsertAfter">A handle to the window to precede the positioned window in the Z order. 
    /// This parameter must be a window handle or one of the following values: HWND_BOTTOM, HWND_NOTOPMOST, HWND_TOP, or HWND_TOPMOST.</param>
    /// <param name="x">The new position of the left side of the window, in client coordinates.</param>
    /// <param name="y">The new position of the top of the window, in client coordinates.</param>
    /// <param name="cx">The new width of the window, in pixels.</param>
    /// <param name="cy">The new height of the window, in pixels.</param>
    /// <param name="flags">The window sizing and positioning flags.</param>
    /// <returns>If the function succeeds, the return value is true; otherwise, it is false.</returns>
    [DllImport(Dll, SetLastError = true)]
    public static extern bool SetWindowPos(IntPtr hwnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy,
        WindowPositionFlags flags);

    /// <summary>
    /// Retrieves the dimensions of the bounding rectangle of the specified window.
    /// </summary>
    /// <param name="hwnd">A handle to the window.</param>
    /// <param name="lpRect">A pointer to a RECT structure that receives the screen coordinates of the upper-left and lower-right corners of the window.</param>
    /// <returns>If the function succeeds, the return value is true. If the function fails, the return value is false.</returns>
    [DllImport(Dll, SetLastError = true)]
    public static extern bool GetWindowRect(IntPtr hwnd, out Rectangle lpRect);

    /// <summary>
    /// Retrieves the coordinates of a window's client area.
    /// </summary>
    /// <param name="hwnd">A handle to the window whose client coordinates are to be retrieved.</param>
    /// <param name="lpRect">A pointer to a RECT structure that receives the client coordinates of the specified window.</param>
    /// <returns>If the function succeeds, the return value is true. If the function fails, the return value is false.</returns>
    [DllImport(Dll, SetLastError = true)]
    public static extern bool GetClientRect(IntPtr hwnd, out Rectangle lpRect);

    /// <summary>
    /// Retrieves the visibility state of the specified window.
    /// </summary>
    /// <param name="hwnd">A handle to the window to be tested.</param>
    /// <returns>If the specified window, its parent window, its parent's parent window, and so forth, have the WS_VISIBLE style, the return value is true. Otherwise, the return value is false.</returns>
    [DllImport(Dll, SetLastError = true)]
    public static extern bool IsWindowVisible(IntPtr hwnd);

    /// <summary>
    /// Determines whether the specified window handle identifies an enabled window.
    /// </summary>
    /// <param name="hWnd">A handle to the window to be tested.</param>
    /// <returns>true if the window is enabled; otherwise, false.</returns>
    [DllImport(Dll, ExactSpelling = true)]
    public static extern bool IsWindowEnabled(IntPtr hWnd);

    /// <summary>
    /// Retrieves information about the specified window. The function also retrieves the value at a specified offset into the extra window memory.
    /// </summary>
    /// <param name="hwnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
    /// <param name="nIndex">The zero-based offset to the value to be retrieved. Valid values are in the range zero through the number of bytes of extra window memory, minus the size of an IntPtr.</param>
    /// <returns>If the function succeeds, the return value is the requested value.</returns>
    [DllImport(Dll, CharSet = CharSet.Unicode, EntryPoint = "GetWindowLongPtr")]
    public static extern IntPtr GetWindowLongPtr_x64(IntPtr hwnd, int nIndex);

    /// <summary>
    /// Retrieves information about the specified window. The function also retrieves the value at a specified offset into the extra window memory.
    /// </summary>
    /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
    /// <param name="nIndex">The zero-based offset to the value to be retrieved. Valid values are in the range zero through the number of bytes of extra window memory, minus four; for example, if you create a window with 12 bytes of extra memory, you can specify an index of 0, 4, or 8.</param>
    /// <returns>If the function succeeds, the return value is the requested value.</returns>
    [DllImport(Dll)]
    public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    /// <summary>
    /// Retrieves information about the specified window. The function also retrieves the value at a specified offset into the extra window memory.
    /// </summary>
    /// <param name="hwnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
    /// <param name="nIndex">The zero-based offset to the value to be retrieved. Valid values are in the range zero through the number of bytes of extra window memory, minus the size of an IntPtr.</param>
    /// <returns>If the function succeeds, the return value is the requested value.</returns>
    public static IntPtr GetWindowLongPtr(IntPtr hwnd, int nIndex)
    {
        return (IntPtr.Size > 4) ? GetWindowLongPtr_x64(hwnd, nIndex) : new IntPtr(GetWindowLong(hwnd, nIndex));
    }

    /// <summary>
    /// Changes an attribute of the specified window. The function also sets the 32-bit (long) value at the specified offset into the extra window memory.
    /// </summary>
    /// <param name="hwnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
    /// <param name="nIndex">The zero-based offset to the value to be set. Valid values are in the range zero through the number of bytes of extra window memory, minus the size of an IntPtr.</param>
    /// <param name="dwNewLong">The replacement value.</param>
    /// <returns>If the function succeeds, the return value is the previous value of the specified 32-bit integer.</returns>
    [DllImport(Dll, CharSet = CharSet.Unicode, EntryPoint = "SetWindowLongPtr")]
    public static extern IntPtr SetWindowLongPtr_x64(IntPtr hwnd, int nIndex, IntPtr dwNewLong);

    /// <summary>
    /// Changes an attribute of the specified window.
    /// </summary>
    /// <param name="hWnd">A handle to the window.</param>
    /// <param name="nIndex">The zero-based offset to the value to be set.</param>
    /// <param name="value">The replacement value.</param>
    /// <returns>The previous value of the specified offset in the extra window memory.</returns>
    [DllImport(Dll)]
    public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int value);

    /// <summary>
    /// Changes an attribute of the specified window.
    /// </summary>
    /// <param name="hwnd">A handle to the window.</param>
    /// <param name="nIndex">The zero-based offset to the value to be set.</param>
    /// <param name="dwNewLong">The replacement value.</param>
    /// <returns>The previous value of the specified offset in the extra window memory.</returns>
    public static IntPtr SetWindowLongPtr(IntPtr hwnd, int nIndex, IntPtr dwNewLong)
    {
        return (IntPtr.Size > 4)
            ? SetWindowLongPtr_x64(hwnd, nIndex, dwNewLong)
            : new IntPtr(SetWindowLong(hwnd, nIndex, dwNewLong.ToInt32()));
    }

    /// <summary>
    /// Sets the keyboard focus to the specified window.
    /// </summary>
    /// <param name="hWnd">A handle to the window that will receive the keyboard input.</param>
    /// <returns>The handle to the window that previously had the keyboard focus.</returns>
    [DllImport(Dll, ExactSpelling = true)]
    public static extern IntPtr SetFocus(IntPtr hWnd);

    /// <summary>
    /// Retrieves the handle to the window that has the keyboard focus.
    /// </summary>
    /// <returns>The handle to the window with the keyboard focus.</returns>
    [DllImport(Dll)]
    public static extern IntPtr GetFocus();

    /// <summary>
    /// Sets the specified window's show state.
    /// </summary>
    /// <param name="hWnd">A handle to the window.</param>
    /// <param name="nCmdShow">Controls how the window is to be shown.</param>
    /// <returns>If the window was previously visible, the return value is true.</returns>
    [DllImport(Dll)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);

    /// <summary>
    /// Retrieves the identifier of the thread that created the specified window.
    /// </summary>
    /// <param name="hWnd">A handle to the window.</param>
    /// <param name="lpdwProcessId">A pointer to a variable that receives the process identifier.</param>
    /// <returns>The identifier of the thread that created the window.</returns>
    [DllImport(Dll)]
    public static extern uint GetWindowThreadProcessId(
        IntPtr hWnd,
        ref uint lpdwProcessId
    );

    /// <summary>
    /// Retrieves the identifier of the thread that created the specified window.
    /// </summary>
    /// <param name="hWnd">A handle to the window.</param>
    /// <returns>The identifier of the thread that created the window.</returns>
    public static uint GetWindowThreadProcessId(
        IntPtr hWnd
    )
    {
        uint a = 0;
        var r = GetWindowThreadProcessId(hWnd, ref a);
        return r;
    }

    /// <summary>
    /// Loads the specified icon resource from the executable file associated with an application instance.
    /// </summary>
    /// <param name="hInstance">A handle to an instance of the module whose executable file contains the icon to be loaded.</param>
    /// <param name="lpIconName">The name of the icon resource to be loaded.</param>
    /// <returns>The handle of the icon if successful, otherwise NULL.</returns>
    [DllImport(Dll)]
    public static extern IntPtr LoadIcon(IntPtr hInstance, string lpIconName);

    /// <summary>
    /// Loads the specified icon resource from the executable file associated with an application instance.
    /// </summary>
    /// <param name="hInstance">A handle to an instance of the module whose executable file contains the icon to be loaded.</param>
    /// <param name="lpIConName">The name of the icon resource to be loaded.</param>
    /// <returns>The handle of the icon if successful, otherwise NULL.</returns>
    [DllImport(Dll)]
    public static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIConName);

    /// <summary>
    /// Creates an overlapped, pop-up, or child window with an extended window style.
    /// </summary>
    /// <param name="dwExStyle">The extended window style of the window being created.</param>
    /// <param name="lpClassName">A pointer to a null-terminated string or a class atom.</param>
    /// <param name="lpWindowName">The window name.</param>
    /// <param name="dwStyle">The style of the window being created.</param>
    /// <param name="x">The initial horizontal position of the window.</param>
    /// <param name="y">The initial vertical position of the window.</param>
    /// <param name="nWidth">The width of the window.</param>
    /// <param name="nHeight">The height of the window.</param>
    /// <param name="hWndParent">A handle to the parent or owner window of the window being created.</param>
    /// <param name="hMenu">A handle to a menu, or specifies a child-window identifier depending on the window style.</param>
    /// <param name="hInstance">A handle to the instance of the module to be associated with the window.</param>
    /// <param name="lpParam">A pointer to a value to be passed to the window through the CREATESTRUCT structure.</param>
    /// <returns>If the function succeeds, the return value is a handle to the new window.</returns>
    [DllImport(Dll, SetLastError = true, EntryPoint = "CreateWindowEx")]
    public static extern IntPtr CreateWindowEx2(
        WindowStylesEx dwExStyle,
        ushort lpClassName,
        string lpWindowName,
        WindowStyles dwStyle,
        int x,
        int y,
        int nWidth,
        int nHeight,
        IntPtr hWndParent,
        IntPtr hMenu,
        IntPtr hInstance,
        IntPtr lpParam);

    /// <summary>
    /// Creates an overlapped, pop-up, or child window with an extended window style.
    /// </summary>
    /// <param name="dwExStyle">The extended window style of the window being created.</param>
    /// <param name="lpClassName">className.</param>
    /// <param name="lpWindowName">The window name.</param>
    /// <param name="dwStyle">The style of the window being created.</param>
    /// <param name="x">The initial horizontal position of the window.</param>
    /// <param name="y">The initial vertical position of the window.</param>
    /// <param name="nWidth">The width of the window.</param>
    /// <param name="nHeight">The height of the window.</param>
    /// <param name="hWndParent">A handle to the parent or owner window of the window being created.</param>
    /// <param name="hMenu">A handle to a menu, or specifies a child-window identifier depending on the window style.</param>
    /// <param name="hInstance">A handle to the instance of the module to be associated with the window.</param>
    /// <param name="lpParam">A pointer to a value to be passed to the window through the CREATESTRUCT structure.</param>
    /// <returns>If the function succeeds, the return value is a handle to the new window.</returns>
    [DllImport(Dll, SetLastError = true, EntryPoint = "CreateWindowEx")]
    public static extern IntPtr CreateWindowEx(
        WindowStylesEx dwExStyle,
        string lpClassName,
        string lpWindowName,
        WindowStyles dwStyle,
        int x,
        int y,
        int nWidth,
        int nHeight,
        IntPtr hWndParent,
        IntPtr hMenu,
        IntPtr hInstance,
        IntPtr lpParam);

    /// <summary>
    /// Changes the parent window of the specified child window.
    /// </summary>
    /// <param name="hWndChild">A handle to the child window.</param>
    /// <param name="hWndNewParent">A handle to the new parent window. If this parameter is NULL, the desktop window becomes the new parent window.</param>
    /// <returns>If the function succeeds, the return value is a handle to the previous parent window. If the function fails, the return value is NULL.</returns>
    /// <remarks>
    /// The SetParent function changes the parent window of the specified child window, but it does not change the owner window. An application can change the owner window by calling the SetWindowLong function.
    /// For more information, see the native SetParent function documentation: https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setparent
    /// </remarks>
    [DllImport(Dll, SetLastError = true)]
    public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

    /// <summary>
    /// Retrieves a handle to the top-level window whose class name and window name match the specified strings.
    /// This function does not search child windows.
    /// </summary>
    /// <param name="lpClassName">
    /// The class name or a class atom created by a previous call to the RegisterClass or RegisterClassEx function.
    /// If lpClassName is NULL, it finds any window whose title matches the lpWindowName parameter.
    /// </param>
    /// <param name="lpWindowName">
    /// The window name (the window's title). If this parameter is NULL, all window names match.
    /// </param>
    /// <returns>
    /// If the function succeeds, the return value is a handle to the window that has the specified class name and window name.
    /// If the function fails, the return value is NULL. To get extended error information, call GetLastError.
    /// </returns>
    /// <remarks>
    /// The FindWindow function does not find child windows.
    /// </remarks>
    [DllImport(Dll, SetLastError = true)]
    public static extern IntPtr FindWindow([Optional] string? lpClassName, [Optional] string? lpWindowName);
}