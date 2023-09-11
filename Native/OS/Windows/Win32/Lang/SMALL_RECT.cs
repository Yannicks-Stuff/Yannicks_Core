using System.Runtime.InteropServices;

namespace Yannick.Native.OS.Windows.Win32.Lang;

[StructLayout(LayoutKind.Sequential)]
public struct SMALL_RECT
{
    public short Left;
    public short Top;
    public short Right;
    public short Bottom;
}