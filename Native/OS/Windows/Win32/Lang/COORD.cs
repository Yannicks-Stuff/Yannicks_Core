using System.Runtime.InteropServices;

namespace Yannick.Native.OS.Windows.Win32.Lang;

[StructLayout(LayoutKind.Sequential)]
public struct COORD
{
    public short X;
    public short Y;
}