using System.Drawing;
using System.Runtime.InteropServices;

namespace Yannick.Native.OS.Windows.Win32;

public static partial class User32
{
    /// <summary>
    /// Represents a simplified version of a Windows message.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Message
    {
        /// <summary>
        /// Handle to the window whose window procedure receives the message.
        /// </summary>
        public IntPtr Hwnd;

        /// <summary>
        /// Specifies the message identifier.
        /// </summary>
        public uint Value;

        /// <summary>
        /// Additional message-specific information.
        /// </summary>
        public IntPtr WParam;

        /// <summary>
        /// Additional message-specific information.
        /// </summary>
        public IntPtr LParam;

        /// <summary>
        /// The time at which the message was posted.
        /// </summary>
        public uint Time;

        /// <summary>
        /// The cursor position, in screen coordinates, when the message was posted.
        /// </summary>
        public Point Point;
    }

    /// <summary>
    /// Contains message information from a thread's message queue.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct MSG
    {
        /// <summary>
        /// Handle to the window whose window procedure receives the message.
        /// </summary>
        public IntPtr hwnd;

        /// <summary>
        /// Specifies the message identifier.
        /// </summary>
        public uint message;

        /// <summary>
        /// Additional message-specific information.
        /// </summary>
        public UIntPtr wParam;

        /// <summary>
        /// Additional message-specific information.
        /// </summary>
        public UIntPtr lParam;

        /// <summary>
        /// The time at which the message was posted.
        /// </summary>
        public uint time;

        /// <summary>
        /// The cursor position, in screen coordinates, when the message was posted.
        /// </summary>
        public Point pt;
    }

    #region HOOKS

    /// <summary>
    /// MessageProc delegate.
    /// </summary>
    public delegate IntPtr MessageProc(int nCode, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// Calls the next hook in the chain.
    /// </summary>
    /// <param name="hhk">Handle of the hook.</param>
    /// <param name="nCode">Hook code.</param>
    /// <param name="wParam">WParam.</param>
    /// <param name="lParam">LParam.</param>
    /// <returns>
    /// The return value of the next hook in the chain.
    /// </returns>
    [DllImport(Dll)]
    public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// Sets a windows hook.
    /// </summary>
    /// <param name="idHook">Hook identifier.</param>
    /// <param name="callback">MessageProc delegate.</param>
    /// <param name="hInstance">Instance handle.</param>
    /// <param name="threadId">Thread ID.</param>
    /// <returns>
    /// The handle of the hook, or null if the function fails.
    /// </returns>
    [DllImport(Dll)]
    public static extern IntPtr SetWindowsHookEx(int idHook, MessageProc callback, IntPtr hInstance, uint threadId);

    /// <summary>
    /// Unhooks a windows hook.
    /// </summary>
    /// <param name="hInstance">Instance handle.</param>
    /// <returns>
    /// True if the function succeeds, false otherwise.
    /// </returns>
    [DllImport(Dll)]
    public static extern bool UnhookWindowsHookEx(IntPtr hInstance);

    /// <summary>
    /// Gets a message.
    /// </summary>
    /// <param name="lpMsg">Out parameter for the message.</param>
    /// <param name="hwnd">Window handle.</param>
    /// <param name="wMsgFilterMin">Lowest message filter.</param>
    /// <param name="wMsgFilterMax">Highest message filter.</param>
    /// <returns>
    /// The number of messages retrieved.
    /// </returns>
    [DllImport(Dll, CharSet = CharSet.Unicode)]
    public static extern int GetMessage(out Message lpMsg, IntPtr hwnd, uint wMsgFilterMin, uint wMsgFilterMax);

    /// <summary>
    /// Gets a message.
    /// </summary>
    /// <param name="lpMsg">Out parameter for the message.</param>
    /// <param name="hwnd">Window handle.</param>
    /// <param name="wMsgFilterMin">Lowest message filter.</param>
    /// <param name="wMsgFilterMax">Highest message filter.</param>
    /// <returns>
    /// if new message available
    /// </returns>
    [DllImport(Dll)]
    public static extern bool GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin,
        uint wMsgFilterMax);

    /// <summary>
    /// Translates a message.
    /// </summary>
    /// <param name="lpMsg">Message to translate.</param>
    /// <returns>
    /// True if the message was translated, false otherwise.
    /// </returns>
    [DllImport(Dll)]
    public static extern bool TranslateMessage([In] ref MSG lpMsg);

    /// <summary>
    /// Dispatches a message to the appropriate window procedure.
    /// </summary>
    /// <param name="lpMsg">A pointer to a <see cref="Message"/> structure that contains the message to be dispatched.</param>
    /// <returns>The handle of the window that processed the message.</returns>
    [DllImport(Dll)]
    public static extern IntPtr DispatchMessage([In] ref MSG lpmsg);

    /// <summary>
    /// Translates a message.
    /// </summary>
    /// <param name="lpMsg">Message to translate.</param>
    /// <returns>
    /// True if the message was translated, false otherwise.
    /// </returns>
    [DllImport(Dll, ExactSpelling = true)]
    public static extern bool TranslateMessage([In] ref Message lpMsg);

    /// <summary>
    /// Dispatches a message to the appropriate window procedure.
    /// </summary>
    /// <param name="lpMsg">A pointer to a <see cref="Message"/> structure that contains the message to be dispatched.</param>
    /// <returns>The handle of the window that processed the message.</returns>
    [DllImport(Dll, CharSet = CharSet.Unicode)]
    public static extern IntPtr DispatchMessage([In] ref Message lpMsg);

    /// <summary>
    /// Peeks at a message in the message queue for the specified window.
    /// </summary>
    /// <param name="lpMsg">A pointer to a <see cref="Message"/> structure that receives the message.</param>
    /// <param name="hWnd">The handle of the window whose message queue is to be peeked.</param>
    /// <param name="wMsgFilterMin">The minimum message value to peek for.</param>
    /// <param name="wMsgFilterMax">The maximum message value to peek for.</param>
    /// <param name="wRemoveMsg">A value that indicates whether to remove the message from the message queue.</param>
    /// <returns>
    /// true if a message was peeked; false if the message queue is empty or the specified window does not have a message queue.
    /// </returns>
    [DllImport(Dll, CharSet = CharSet.Unicode)]
    public static extern bool PeekMessage(out Message lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax,
        uint wRemoveMsg);

    #endregion

    #region KeyBoard/Mouse

    public enum VirtualKeyMapType
    {
        VK_TO_CHAR = 2,
        VK_TO_VSC = 0,
        VK_TO_VSC_EX = 4,
        VSC_TO_VK = 1,
        VSC_TO_VK_EX = 3
    }

    /// <summary>
    /// Maps a virtual key code to a character or scan code.
    /// </summary>
    /// <param name="uCode">The virtual key code to be mapped.</param>
    /// <param name="uMapType">The type of mapping to be performed.</param>
    /// <returns>
    /// The mapped character or scan code, if successful.
    /// 0, if the specified virtual key code is not a valid virtual key code.
    /// -1, if the specified mapping type is not valid.
    /// </returns>
    [DllImport(Dll, CharSet = CharSet.Unicode)]
    public static extern uint MapVirtualKey(uint uCode, VirtualKeyMapType uMapType);

    #endregion
}