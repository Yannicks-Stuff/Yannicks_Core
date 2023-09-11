using System.Runtime.InteropServices;

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
}