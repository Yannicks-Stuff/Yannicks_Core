using System.Collections.ObjectModel;
using System.Drawing;
using System.Runtime.InteropServices;
using Yannick.Native.OS.Windows.Win32;

namespace Yannick.Native.OS.Windows.UI;

public sealed class Console
{
    private const int STD_OUTPUT_HANDLE = -11;

    public static ReadOnlyDictionary<ConsoleColor, Color> CurrentColorPlate()
    {
        var dic = new Dictionary<ConsoleColor, Color>();
        var handle = Kernel32.GetStdHandle(STD_OUTPUT_HANDLE);
        var info = new Kernel32.CONSOLE_SCREEN_BUFFER_INFOEX();
        info.cbSize = (uint)Marshal.SizeOf(info);

        if (Kernel32.GetConsoleScreenBufferInfoEx(handle, ref info))
        {
            for (var i = 0; i < 16; i++)
            {
                var colorValue = info.ColorTable[i];
                var r = (int)(colorValue & 0xFF);
                var g = (int)((colorValue >> 8) & 0xFF);
                var b = (int)((colorValue >> 16) & 0xFF);

                dic.Add((ConsoleColor)i, Color.FromArgb(r, g, b));
            }
        }
        else
        {
            dic = new Dictionary<ConsoleColor, Color>
            {
                { ConsoleColor.Black, Color.Black },
                { ConsoleColor.DarkBlue, Color.DarkBlue },
                { ConsoleColor.DarkGreen, Color.DarkGreen },
                { ConsoleColor.DarkCyan, Color.DarkCyan },
                { ConsoleColor.DarkRed, Color.DarkRed },
                { ConsoleColor.DarkMagenta, Color.DarkMagenta },
                { ConsoleColor.DarkYellow, Color.Olive },
                { ConsoleColor.Gray, Color.Gray },
                { ConsoleColor.DarkGray, Color.DarkGray },
                { ConsoleColor.Blue, Color.Blue },
                { ConsoleColor.Green, Color.Green },
                { ConsoleColor.Cyan, Color.Cyan },
                { ConsoleColor.Red, Color.Red },
                { ConsoleColor.Magenta, Color.Magenta },
                { ConsoleColor.Yellow, Color.Yellow },
                { ConsoleColor.White, Color.White }
            };
        }

        return dic.AsReadOnly();
    }
}