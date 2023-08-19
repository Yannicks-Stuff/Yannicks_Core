using System.Drawing;
using System.Runtime.InteropServices;

namespace Yannick.Native.OS.Windows.Win32;

public static partial class User32
{
    /// <summary>
    /// Represents the window class, which is a set of attributes that the operating system uses as a template to create a window.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct WNDCLASS
    {
        /// <summary>
        /// Specifies the class styles. This member can be a combination of the ClassStyles enumeration.
        /// </summary>
        public ClassStyles style;

        /// <summary>
        /// Pointer to the window procedure. You must use the MarshalAs attribute to indicate that this is a function pointer.
        /// </summary>
        [MarshalAs(UnmanagedType.FunctionPtr)] public WndProc lpfnWndProc;

        /// <summary>
        /// Specifies the number of extra bytes to allocate following the window-class structure. The system initializes the bytes to zero.
        /// </summary>
        public int cbClsExtra;

        /// <summary>
        /// Specifies the number of extra bytes to allocate following the window instance. The system initializes the bytes to zero.
        /// </summary>
        public int cbWndExtra;

        /// <summary>
        /// Handle to the instance that contains the window procedure for the class.
        /// </summary>
        public IntPtr hInstance;

        /// <summary>
        /// Handle to the class icon. This member must be a handle to an icon resource.
        /// </summary>
        public IntPtr hIcon;

        /// <summary>
        /// Handle to the class cursor. This member must be a handle to a cursor resource.
        /// </summary>
        public IntPtr hCursor;

        /// <summary>
        /// Handle to the class background brush. This member can be a handle to the physical brush to be used for painting the background, or it can be a color value.
        /// </summary>
        public IntPtr hbrBackground;

        /// <summary>
        /// Pointer to a null-terminated string that specifies the resource name of the class menu, as the name appears in the resource file. If you use an integer to identify the menu, use the MAKEINTRESOURCE macro.
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)] public string lpszMenuName;

        /// <summary>
        /// Pointer to a null-terminated string or is an atom. If this parameter is an atom, it must be a global atom created by a previous call to the GlobalAddAtom function.
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)] public string lpszClassName;
    }

    /// <summary>
    /// Represents the extended window class information. It is used with the RegisterClassEx and GetClassInfoEx functions.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct WNDCLASSEX
    {
        /// <summary>
        /// The size, in bytes, of this structure.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)] public int cbSize;

        /// <summary>
        /// The class style(s). This member can be any combination of the ClassStyles enumeration.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)] public int style;

        /// <summary>
        /// A pointer to the window procedure.
        /// </summary>
        public IntPtr lpfnWndProc;

        /// <summary>
        /// The number of extra bytes to allocate following the window-class structure. The system initializes the bytes to zero.
        /// </summary>
        public int cbClsExtra;

        /// <summary>
        /// The number of extra bytes to allocate following the window instance. The system initializes the bytes to zero.
        /// </summary>
        public int cbWndExtra;

        /// <summary>
        /// A handle to the instance that contains the window procedure for the class.
        /// </summary>
        public IntPtr hInstance;

        /// <summary>
        /// A handle to the class icon. This member must be a handle to an icon resource.
        /// </summary>
        public IntPtr hIcon;

        /// <summary>
        /// A handle to the class cursor. This member must be a handle to a cursor resource.
        /// </summary>
        public IntPtr hCursor;

        /// <summary>
        /// A handle to the class background brush. This member can be a handle to the physical brush to be used for painting the background, or it can be a color value.
        /// </summary>
        public IntPtr hbrBackground;

        /// <summary>
        /// Pointer to a null-terminated string that specifies the resource name of the class menu, as the name appears in the resource file. If you use an integer to identify the menu, use the MAKEINTRESOURCE macro.
        /// </summary>
        public string lpszMenuName;

        /// <summary>
        /// Pointer to a null-terminated string or is an atom. If this parameter is an atom, it must be a global atom created by a previous call to the GlobalAddAtom function.
        /// </summary>
        public string lpszClassName;

        /// <summary>
        /// A handle to a small icon that is associated with the window class.
        /// </summary>
        public IntPtr hIconSm;
    }

    /// <summary>
    /// Contains information about a display monitor.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MonitorInfo
    {
        /// <summary>
        /// The size of the structure, in bytes.
        /// </summary>
        public uint Size;

        /// <summary>
        /// The display monitor rectangle.
        /// </summary>
        public Rectangle MonitorRect;

        /// <summary>
        /// The work area rectangle of the display monitor that can be used by applications, expressed in virtual-screen coordinates. Windows uses this rectangle to maximize an application on the monitor.
        /// </summary>
        public Rectangle WorkRect;

        /// <summary>
        /// A set of flags that represent attributes of the display monitor.
        /// </summary>
        public MonitorInfoFlag Flags;
    }
}