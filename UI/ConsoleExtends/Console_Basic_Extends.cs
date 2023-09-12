using System.Collections.ObjectModel;
using System.Drawing;
using System.Runtime.InteropServices;
using Yannick.Native;

namespace Yannick.UI;

public partial class Console
{
    private static Animation? _animation;

    private static bool _activeAnimation = false;


    public static (int, int) Cursor => (CursorTop, CursorLeft);

    public static Animation? UseAnimation
    {
        get => _animation;
        set
        {
            _animation?.Stop();
            _animation = value;
        }
    }

    public static bool ActiveAnimation
    {
        get => _activeAnimation;
        set
        {
            if (value)
                _animation?.StartAsync();
            else
                _animation?.Stop();

            _activeAnimation = value;
        }
    }


    /// <summary>
    /// Sets the foreground and/or background color of the console.
    /// </summary>
    /// <param name="fb">The color to set for the console's foreground. If null, the current color will be maintained.</param>
    /// <param name="bg">The color to set for the console's background. If null, the current color will be maintained.</param>
    public static void SetColor(ConsoleColor? fb = null, ConsoleColor? bg = null)
    {
        global::System.Console.ForegroundColor = fb ?? global::System.Console.ForegroundColor;
        global::System.Console.BackgroundColor = bg ?? global::System.Console.BackgroundColor;
        _foregroundColor24 = consoleColors[global::System.Console.ForegroundColor];
        _backgroundColor24 = consoleColors[global::System.Console.BackgroundColor];
    }

    /// <summary>
    /// Writes the specified string value to the console, centered horizontally.
    /// </summary>
    /// <param name="text">The string to write to the console.</param>
    public static void WriteCenter(string text)
    {
        var x3 = Convert.ToInt32(Math.Floor(((WindowWidth - CursorLeft + 1) - text.Length) / 2.0));
        x3 = ((x3 >= 0) ? x3 : 0);
        x3 = ((x3 <= WindowWidth) ? x3 : 0);
        SetCursorPosition(x3, CursorTop);
        Write(text);
    }

    /// <summary>
    /// Writes the specified string value followed by a newline character to the console, centered horizontally.
    /// </summary>
    /// <param name="text">The string to write to the console.</param>
    public static void WriteLineCenter(string text)
    {
        WriteCenter(text);
        global::System.Console.WriteLine();
    }

    public static string ReadLine(ConsoleColor foreground)
    {
        var fb = ForegroundColor;
        ForegroundColor = foreground;
        var rs = global::System.Console.ReadLine();
        ForegroundColor = fb;
        return rs ?? string.Empty;
    }

    public static string ReadLineAsHint(char hint = '*', ConsoleColor? foreground = null)
    {
        var rs = "";
        while (true)
        {
            var k = System.Console.ReadKey();

            if (k.Key == ConsoleKey.Enter)
                break;

            rs += k.KeyChar;
            Write(hint, ForegroundColor);
        }

        return rs;
    }

    public static string ReadKeysUntil(ConsoleKey key)
    {
        var rs = "";
        while (true)
        {
            var k = System.Console.ReadKey();

            if (k.Key == key)
                break;

            rs += k.KeyChar;
        }

        return rs;
    }

    public static void ClearLine(int? y = null, bool setNewCords = false)
    {
        y = y ?? CursorTop;

        if (y < 0)
            y = CursorTop - y * -1;

        var (cx, cy) = Cursor;

        SetCursorPosition(0, y.Value < 0 ? 0 : y.Value);
        Write(new string(' ', Math.Max(BufferWidth, WindowWidth)));
        SetCursorPosition(cx, cy < 0 ? 0 : cy);

        if (setNewCords)
            SetCursorPosition(0, cy < 0 ? 0 : cy);
    }

    public static void ClearLine(bool setNewCordsOnLastEntry = false, params int[]? y)
    {
        if (y is not { Length: > 1 })
        {
            ClearLine(y?[0], setNewCordsOnLastEntry);
            return;
        }

        foreach (var cy in y.SkipLast(1))
        {
            ClearLine(cy, false);
        }

        ClearLine(y[^1], setNewCordsOnLastEntry);
    }

    private delegate IntPtr GetStdHandle(int nStdHandle);

    private delegate bool GetConsoleMode(IntPtr hConsoleHandle, out int lpMode);

    private delegate bool SetConsoleMode(IntPtr hConsoleHandle, int dwMode);

    private delegate ReadOnlyDictionary<ConsoleColor, Color> CurrentBackgroundFromWindowsCMD();
}