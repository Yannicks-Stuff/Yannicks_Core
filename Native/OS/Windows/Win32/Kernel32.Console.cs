using System.Runtime.InteropServices;
using Yannick.Native.OS.Windows.Win32.Lang;

namespace Yannick.Native.OS.Windows.Win32;

public static partial class Kernel32
{
    private const string DLL = "kernel32.dll";

    public const int STD_OUTPUT_HANDLE = -11;
    public const int ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

    [DllImport(DLL, SetLastError = true)]
    public static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport(DLL, SetLastError = true)]
    public static extern bool GetConsoleMode(IntPtr hConsoleHandle, out int lpMode);

    [DllImport(DLL, SetLastError = true)]
    public static extern bool SetConsoleMode(IntPtr hConsoleHandle, int dwMode);


    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool GetConsoleScreenBufferInfoEx(IntPtr hConsoleOutput,
        ref CONSOLE_SCREEN_BUFFER_INFOEX csbie);


    [StructLayout(LayoutKind.Sequential)]
    public struct CONSOLE_SCREEN_BUFFER_INFOEX
    {
        public uint cbSize;
        public COORD dwSize;
        public COORD dwCursorPosition;
        public ushort wAttributes;
        public SMALL_RECT srWindow;
        public COORD dwMaximumWindowSize;
        public ushort wPopupAttributes;
        public bool bFullscreenSupported;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public uint[] ColorTable;
    }
}