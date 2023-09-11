using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Yannick.Extensions.DictionaryExtensions;
using Yannick.Native;

namespace Yannick.UI;

public partial class Console
{
    private static Color _foregroundColor24 = Color.White;
    private static Color _backgroundColor24 = Color.Black;

    private static Dictionary<ConsoleColor, Color> consoleColors = new Dictionary<ConsoleColor, Color>
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

    private static Dictionary<Color, ConsoleColor> colorsConsole = consoleColors.Swap();


    static Console()
    {
        if (IsWindows)
        {
            var linker = new Linker("Kernel32", "Yannick.Native.OS.Windows.Win32");
            var handle = linker.LinkStatic<GetStdHandle>()!(-11);
            if (linker.LinkStatic<GetConsoleMode>()!(handle, out var mode))
            {
                mode |= 0x0004;
                linker.LinkStatic<SetConsoleMode>()!(handle, mode);
            }
        }
    }

    public static Color ForegroundColor24
    {
        get => _foregroundColor24;
        set => SetColor(value, _backgroundColor24);
    }

    public static Color BackgroundColor24
    {
        get => _backgroundColor24;
        set => SetColor(_foregroundColor24, value);
    }

    private static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);


    public static void SetColor(Color fb, Color bg)
    {
        ForegroundColor = colorsConsole[fb];
        BackgroundColor = colorsConsole[bg];
    }

    private static string GetAnsiColorCode(ConsoleColor color)
    {
        return color switch
        {
            ConsoleColor.Black => "30",
            ConsoleColor.DarkRed => "31",
            ConsoleColor.DarkGreen => "32",
            ConsoleColor.DarkYellow => "33",
            ConsoleColor.DarkBlue => "34",
            ConsoleColor.DarkMagenta => "35",
            ConsoleColor.DarkCyan => "36",
            ConsoleColor.Gray => "37",
            ConsoleColor.DarkGray => "90",
            ConsoleColor.Red => "91",
            ConsoleColor.Green => "92",
            ConsoleColor.Yellow => "93",
            ConsoleColor.Blue => "94",
            ConsoleColor.Magenta => "95",
            ConsoleColor.Cyan => "96",
            ConsoleColor.White => "97",
            _ => "39" // Standardfarbe
        };
    }


    private static double GetColorDistance(Color c1, Color c2)
    {
        double rDiff = c1.R - c2.R;
        double gDiff = c1.G - c2.G;
        double bDiff = c1.B - c2.B;

        return rDiff * rDiff + gDiff * gDiff + bDiff * bDiff;
    }

    private static ConsoleColor ConvertFromColor(Color color, bool foreground = true)
    {
        // Find the console color that's closest to the provided color
        var closest = consoleColors.MinBy(pair => GetColorDistance(color, pair.Value)).Key;

        // If background color, avoid returning dark colors that might not be visible
        if (!foreground && (closest == ConsoleColor.Black || closest == ConsoleColor.DarkBlue ||
                            closest == ConsoleColor.DarkGreen || closest == ConsoleColor.DarkCyan ||
                            closest == ConsoleColor.DarkRed || closest == ConsoleColor.DarkMagenta ||
                            closest == ConsoleColor.DarkYellow))
        {
            return ConsoleColor.Black; // Or some other default dark color
        }

        return closest;
    }

    public static void WriteBold(string text, ConsoleColor? foreground = null, ConsoleColor? background = null)
    {
        Write($"\x1B[1m{text}\x1B[22m", foreground ?? ForegroundColor, background);
    }

    public static void WriteLineBold(string text, ConsoleColor? foreground = null, ConsoleColor? background = null)
    {
        WriteBold(text, foreground, background);
        WriteLine();
    }

    public static void WriteUnderlined(string text, ConsoleColor? foreground = null, ConsoleColor? background = null)
    {
        Write($"\x1B[4m{text}\x1B[24m", foreground ?? ForegroundColor, background);
    }

    public static void WriteLineUnderlined(string text, ConsoleColor? foreground = null,
        ConsoleColor? background = null)
    {
        WriteUnderlined(text, foreground, background);
        WriteLine();
    }

    [SupportedOSPlatform("windows10.0.14393.0")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("osx")]
    public static void WriteBold(string text, Color? foreground = null, Color? background = null)
    {
        var oldF = ForegroundColor24;
        var oldB = BackgroundColor24;

        WriteColor(foreground ?? oldF, background ?? oldB);
        Write($"\x1B[1m{text}\x1B[22m");
        SetColor(oldF, oldB);
    }

    [SupportedOSPlatform("windows10.0.14393.0")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("osx")]
    public static void WriteLineBold(string text, Color? foreground = null, Color? background = null)
    {
        WriteBold(text, foreground, background);
        WriteLine();
    }

    [SupportedOSPlatform("windows10.0.14393.0")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("osx")]
    public static void WriteUnderlined(string text, Color? foreground = null, Color? background = null)
    {
        var oldF = ForegroundColor24;
        var oldB = BackgroundColor24;

        WriteColor(foreground ?? oldF, background ?? oldB);
        Write($"\x1B[4m{text}\x1B[24m");

        SetColor(oldF, oldB);
    }

    [SupportedOSPlatform("windows10.0.14393.0")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("osx")]
    public static void WriteLineUnderlined(string text, Color? foreground = null, Color? background = null)
    {
        WriteUnderlined(text, foreground, background);
        WriteLine();
    }

    [SupportedOSPlatform("windows10.0.14393.0")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("osx")]
    public static void WriteItalic(string text, Color? foreground = null, Color? background = null)
    {
        var oldF = ForegroundColor24;
        var oldB = BackgroundColor24;

        WriteColor(foreground ?? oldF, background ?? oldB);
        Write($"\x1B[3m{text}\x1B[23m");

        SetColor(oldF, oldB);
    }

    [SupportedOSPlatform("windows10.0.14393.0")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("osx")]
    public static void WriteLineItalic(string text, Color? foreground = null, Color? background = null)
    {
        WriteItalic(text, foreground, background);
        WriteLine();
    }

    [SupportedOSPlatform("windows10.0.14393.0")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("osx")]
    public static void WriteStrikethrough(string text, Color? foreground = null, Color? background = null)
    {
        var oldF = ForegroundColor24;
        var oldB = BackgroundColor24;

        WriteColor(foreground ?? oldF, background ?? oldB);
        Write($"\x1B[9m{text}\x1B[29m");

        SetColor(oldF, oldB);
    }

    [SupportedOSPlatform("windows10.0.14393.0")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("osx")]
    public static void WriteLineStrikethrough(string text, Color? foreground = null, Color? background = null)
    {
        WriteStrikethrough(text, foreground, background);
        WriteLine();
    }

    [SupportedOSPlatform("windows10.0.14393.0")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("osx")]
    public static void Write(string txt, Color? foreground, Color? background)
    {
        var oldF = ForegroundColor24;
        var oldB = BackgroundColor24;

        WriteColor(foreground ?? ForegroundColor24, background ?? BackgroundColor24);
        Write(txt);

        SetColor(oldF, oldB);
    }

    [SupportedOSPlatform("windows10.0.14393.0")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("osx")]
    public static void WriteLine(string txt, Color? foreground, Color? background)
    {
        Write(txt, foreground, background);
        WriteLine();
    }

    private static string GetTrueColorAnsiCode(Color color, bool isBackground = false)
    {
        return isBackground ? $"\x1B[48;2;{color.R};{color.G};{color.B}m" : $"\x1B[38;2;{color.R};{color.G};{color.B}m";
    }

    private static void WriteColor(Color? foreground, Color? background)
    {
        if (foreground.HasValue)
        {
            Console.Write(GetTrueColorAnsiCode(foreground.Value));
        }

        if (background.HasValue)
        {
            Console.Write(GetTrueColorAnsiCode(background.Value, true));
        }
    }
}