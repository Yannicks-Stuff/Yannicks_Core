using System.Collections.ObjectModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
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
        {
            y = CursorTop - (y * -1);
            y = y.Value < 0 ? 0 : y.Value;
        }


        var (cx, cy) = Cursor;

        SetCursorPosition(0, y.Value);
        Write(new string(' ', Math.Max(BufferWidth, WindowWidth)));

        if (setNewCords)
            SetCursorPosition(0, y.Value);
        else
            SetCursorPosition(cx, cy);
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


    /// <summary>
    /// Reads a line of text from the console, providing various customization options.
    /// </summary>
    /// <param name="beforeReadMsg">Message to display before reading input.</param>
    /// <param name="beforeMsgForeground">Foreground color for the before-read message.</param>
    /// <param name="userForeground">Foreground color for user input.</param>
    /// <param name="onUserInput">Function to validate user input. Returns true if valid, false otherwise.</param>
    /// <param name="msgOnError">Message to display on validation error.</param>
    /// <param name="msgErrorForeground">Foreground color for the error message.</param>
    /// <param name="msgOnSuccess">Message to display on successful validation.</param>
    /// <param name="msgSuccessForeground">Foreground color for the success message.</param>
    /// <param name="maxWidth">Maximum width for the input field.</param>
    /// <param name="allowedInvisibleChars">Indicates whether characters beyond the maxWidth are allowed.</param>
    /// <param name="mask">Character to mask the input with, e.g., '*' for password fields.</param>
    /// <param name="wait">Duration to wait after displaying success or error message.</param>
    /// <param name="defaultVal">Default return value if max attempts are reached.</param>
    /// <param name="maxTrys">Maximum number of input attempts allowed.</param>
    /// <param name="clearAfterReadLine">Indicates whether to clear the line after reading input.</param>
    /// <returns>The user input string, or the default value if max attempts are reached.</returns>
    [SupportedOSPlatform("windows10.0.14393.0")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("osx")]
    public static string? ReadLine(string? beforeReadMsg = null, Color? beforeMsgForeground = null,
        Color? userForeground = null,
        Func<string?, bool>? onUserInput = null,
        string? msgOnError = null, Color? msgErrorForeground = null, string? msgOnSuccess = null,
        Color? msgSuccessForeground = null, uint? maxWidth = null, bool allowedInvisibleChars = true, char? mask = null,
        TimeSpan? wait = null, string? defaultVal = default, byte maxTrys = 0, bool clearAfterReadLine = false)
    {
        var cF = Console.ForegroundColor24;
        var cB = Console.BackgroundColor24;

        beforeMsgForeground ??= Color.White;
        userForeground ??= Color.Cyan;
        msgErrorForeground ??= Color.Red;
        msgSuccessForeground ??= Color.Green;
        maxWidth ??= Convert.ToUInt32(Console.WindowWidth - (beforeReadMsg?.Length ?? 0));
        maxTrys = Convert.ToByte((maxTrys < 1 ? 1 : maxTrys) + 1);

        var txt = "";
        var sX = Console.CursorLeft;
        var isStartEscapeS = false;

        if (beforeReadMsg != null)
        {
            Console.Write(beforeReadMsg, beforeMsgForeground.Value, cB);
            sX = Console.CursorLeft;
        }

        do
        {
            if (maxTrys == 0)
                break;

            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();

                if (onUserInput == null)
                    break;

                if (onUserInput.Invoke(txt))
                {
                    if (msgOnSuccess == null)
                        break;

                    ClearLine();
                    Console.Write(msgOnSuccess,
                        msgSuccessForeground.Value, cB);

                    if (wait != null)
                        Thread.Sleep(wait.Value);

                    break;
                }
                else
                {
                    maxTrys--;
                    txt = "";

                    if (msgOnError == null)
                        continue;

                    ClearLine();
                    Console.Write(msgOnError,
                        msgErrorForeground.Value, cB);

                    if (wait != null)
                        Thread.Sleep(wait.Value);

                    ClearLine();
                }
            }
            else
                switch (key.Key)
                {
                    case ConsoleKey.Backspace when txt.Length <= 0:
                        continue;
                    case ConsoleKey.Backspace:
                    {
                        var cursorPosition = Console.CursorLeft - sX;
                        if (cursorPosition <= 0)
                            continue;

                        txt = txt.Remove(cursorPosition - 1, 1);

                        Console.CursorLeft -= 1;

                        var visibleText = txt.Length - cursorPosition + 1 <= maxWidth.Value
                            ? txt[(cursorPosition - 1)..]
                            : txt.Substring(cursorPosition - 1,
                                (int)maxWidth.Value - cursorPosition + 1);
                        Console.Write(visibleText + " ", userForeground.Value, cB);
                        Console.CursorLeft = cursorPosition - 1 + sX;
                        break;
                    }
                    case ConsoleKey.Tab:
                    {
                        var cursorPosition = Console.CursorLeft - sX;
                        txt = txt.Insert(cursorPosition, "    ");

                        if (txt.Length > maxWidth)
                        {
                            Console.Write(
                                string.Concat(txt.AsSpan(cursorPosition, 4),
                                    cursorPosition + 4 < txt.Length ? txt[(cursorPosition + 4)..] : ""),
                                userForeground.Value, cB);
                            Console.CursorLeft = cursorPosition + 4 + sX;
                        }
                        else
                        {
                            Console.Write("    ", userForeground.Value, cB);
                            Console.CursorLeft += 4;
                        }

                        break;
                    }
                    case ConsoleKey.LeftArrow:
                    {
                        if (Console.CursorLeft > sX)
                            Console.CursorLeft--;
                        break;
                    }
                    case ConsoleKey.RightArrow:
                    {
                        var visibleLength = Math.Min(txt.Length, maxWidth.Value);
                        if (Console.CursorLeft < sX + visibleLength)
                            Console.CursorLeft++;
                        break;
                    }
                    default:
                    {
                        var cursorPosition = Console.CursorLeft - sX;
                        var t = NewText(key.KeyChar);
                        txt = txt.Insert(cursorPosition, t);

                        if (txt.Length <= maxWidth)
                        {
                            Console.Write(txt[cursorPosition..], userForeground.Value, cB);
                            Console.CursorLeft = cursorPosition + t.Length + sX;
                        }
                        else if (allowedInvisibleChars && cursorPosition < maxWidth)
                        {
                            Console.Write(txt.Substring(cursorPosition, (int)maxWidth.Value - cursorPosition),
                                userForeground.Value, cB);
                            Console.CursorLeft = cursorPosition + t.Length + sX;
                        }

                        break;
                    }
                }
        } while (true);

        if (clearAfterReadLine)
            ClearLine();

        return maxTrys == 0 ? defaultVal : txt;

        void ClearLine()
        {
            Console.CursorLeft = sX;
            Console.Write(new string(' ', Convert.ToInt32(maxWidth!.Value)), cF, cB);
            Console.CursorLeft = sX;
        }

        string NewText(char key)
        {
            var newK = key.ToString();

            /*
            if (isStartEscapeS)
            {
                switch (key)
                {
                    case '\\':
                        newK = "\\";
                        break;
                    case 't':
                        newK = "    ";
                        break;
                }
            }
            else if (key == '\\')
            {
                isStartEscapeS = true;
            }*/

            return mask.HasValue ? mask.Value.ToString() : newK;
        }
    }

    private delegate IntPtr GetStdHandle(int nStdHandle);

    private delegate bool GetConsoleMode(IntPtr hConsoleHandle, out int lpMode);

    private delegate bool SetConsoleMode(IntPtr hConsoleHandle, int dwMode);

    private delegate ReadOnlyDictionary<ConsoleColor, Color> CurrentBackgroundFromWindowsCMD();
}