using System.Collections.ObjectModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Yannick.Native;

namespace Yannick.UI;

public partial class Console
{
    /// <summary>
    /// Represents a method that is invoked during the reading of a line of input.
    /// </summary>
    /// <param name="currentKey">
    /// The <see cref="ConsoleKeyInfo"/> object representing the key currently pressed by the user.
    /// </param>
    /// <param name="completeTxt">
    /// The complete text that has been entered so far by the user.
    /// </param>
    /// <returns>
    /// A tuple consisting of the following elements:
    /// <list type="bullet">
    /// <item><description><c>OnReadLineStatus skip</c>: A value indicating whether how to continue the program</description></item>
    /// <item><description><c>string? addTxt</c>: An optional text to be appended to the current input. If null, no text is added.</description></item>
    /// <item><description><c>Color? foreground</c>: An optional color to change the console's foreground color. If null, the color from ReadLine is use</description></item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// This delegate can be used to customize the behavior of reading a line of input, allowing for dynamic modifications or validations as the user types.
    /// </remarks>
    public delegate (OnReadLineStatus, string?, Color?) OnReadLine(ConsoleKeyInfo currentKey, string completeTxt);

    public enum OnReadLineStatus
    {
        /// <summary>
        /// Skip the current key
        /// </summary>
        SKIP,

        /// <summary>
        /// Continue the loop
        /// </summary>
        CONTINUE,

        /// <summary>
        /// Break the loop and get the current txt
        /// </summary>
        EXIT,

        /// <summary>
        /// Call the error msg and reset the txt
        /// </summary>
        ERROR
    }

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
    /// <param name="onKeyInput">Represents a method that is invoked during the reading of a line of input.</param>
    /// <returns>The user input string, or the default value if max attempts are reached.</returns>
    [SupportedOSPlatform("windows10.0.14393.0")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("osx")]
    public static string? ReadLine(string? beforeReadMsg = null, Color? beforeMsgForeground = null,
        Color? userForeground = null,
        Func<string?, bool>? onUserInput = null,
        string? msgOnError = null, Color? msgErrorForeground = null, string? msgOnSuccess = null,
        Color? msgSuccessForeground = null, uint? maxWidth = null, bool allowedInvisibleChars = true, char? mask = null,
        TimeSpan? wait = null, string? defaultVal = default, uint maxTrys = 0, bool clearAfterReadLine = false,
        OnReadLine? onKeyInput = null, bool cursorVisible = true)
    {
        if (beforeReadMsg == null &&
            beforeMsgForeground == null &&
            userForeground == null &&
            onUserInput == null &&
            msgOnError == null &&
            msgErrorForeground == null &&
            msgOnSuccess == null &&
            msgSuccessForeground == null &&
            maxWidth == null &&
            mask == null &&
            wait == null &&
            defaultVal == null)
            return global::System.Console.ReadLine();


        var s = CursorVisible;
        var cF = ForegroundColor24;
        var cB = BackgroundColor24;

        CursorVisible = cursorVisible;

        beforeMsgForeground ??= Color.White;
        userForeground ??= Color.Cyan;
        msgErrorForeground ??= Color.Red;
        msgSuccessForeground ??= Color.Green;
        maxWidth ??= Convert.ToUInt32(WindowWidth - (beforeReadMsg?.Length ?? 0));
        maxTrys = (maxTrys < 1 ? 1 : maxTrys);
        maxTrys = maxTrys < uint.MaxValue ? maxTrys + 1 : maxTrys;

        var txt = "";
        var realTxt = "";
        var sX = CursorLeft;
        var sXR = CursorLeft;
        var sY = CursorTop;
        //var isStartEscapeS = false;

        if (beforeReadMsg != null)
        {
            Write(beforeReadMsg, beforeMsgForeground.Value, cB);
            sX = CursorLeft;
        }

        do
        {
            if (maxTrys == 0)
                break;

            var key = ReadKey(true);

            if (onKeyInput != null)
            {
                var (skip, addTxt, foreground) = onKeyInput(key, realTxt);

                if (addTxt != null)
                    foreach (var t in addTxt)
                        Append(t, foreground);

                if (skip == OnReadLineStatus.EXIT)
                    break;
                else
                    switch (skip)
                    {
                        case OnReadLineStatus.SKIP:
                            continue;
                        case OnReadLineStatus.ERROR:
                            DrawError();
                            continue;
                    }
            }

            if (key.Key == ConsoleKey.Enter)
            {
                if (onUserInput == null)
                    break;

                if (onUserInput.Invoke(realTxt))
                {
                    if (msgOnSuccess == null)
                        break;

                    ClearLine();
                    Write(msgOnSuccess,
                        msgSuccessForeground.Value, cB);

                    if (wait != null)
                        Thread.Sleep(wait.Value);

                    break;
                }
                else
                {
                    maxTrys--;
                    txt = "";
                    realTxt = "";

                    if (msgOnError == null)
                        continue;

                    DrawError();
                }
            }
            else
                switch (key.Key)
                {
                    case ConsoleKey.Backspace when txt.Length <= 0:
                        continue;
                    case ConsoleKey.Backspace:
                    {
                        var cursorPosition = CursorLeft - sX;
                        if (cursorPosition <= 0)
                            continue;

                        txt = txt.Remove(cursorPosition - 1, 1);
                        realTxt = realTxt.Remove(cursorPosition - 1, 1);

                        CursorLeft -= 1;

                        var visibleText = txt.Length - cursorPosition + 1 <= maxWidth.Value
                            ? txt[(cursorPosition - 1)..]
                            : txt.Substring(cursorPosition - 1,
                                (int)maxWidth.Value - cursorPosition + 1);
                        Write(visibleText + " ", userForeground.Value, cB);
                        CursorLeft = cursorPosition - 1 + sX;
                        break;
                    }
                    case ConsoleKey.Tab:
                    {
                        var cursorPosition = CursorLeft - sX;
                        txt = txt.Insert(cursorPosition, "    ");
                        realTxt = realTxt.Insert(cursorPosition, "    ");

                        if (txt.Length > maxWidth)
                        {
                            Write(
                                string.Concat(txt.AsSpan(cursorPosition, 4),
                                    cursorPosition + 4 < txt.Length ? txt[(cursorPosition + 4)..] : ""),
                                userForeground.Value, cB);
                            CursorLeft = cursorPosition + 4 + sX;
                        }
                        else
                        {
                            Write("    ", userForeground.Value, cB);
                            CursorLeft += 4;
                        }

                        break;
                    }
                    case ConsoleKey.LeftArrow:
                    {
                        if (CursorLeft > sX)
                            CursorLeft--;
                        break;
                    }
                    case ConsoleKey.RightArrow:
                    {
                        var visibleLength = Math.Min(txt.Length, maxWidth.Value);
                        if (CursorLeft < sX + visibleLength)
                            CursorLeft++;
                        break;
                    }
                    default:
                    {
                        Append(key.KeyChar);
                        break;
                    }
                }
        } while (true);

        if (clearAfterReadLine)
            ClearLine(true);

        WriteLine();

        CursorVisible = s;

        return maxTrys == 0 ? defaultVal : realTxt;

        void ClearLine(bool isOnExit = false)
        {
            CursorLeft = isOnExit ? sXR : sX;
            CursorTop = sY;
            Write(new string(' ', Convert.ToInt32(maxWidth!.Value) + (Math.Min(sX, sXR) - Math.Min(sXR, sX))), cF, cB);
            CursorLeft = isOnExit ? sXR : sX;
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

        void Append(char k, Color? otherForeground = null)
        {
            var cursorPosition = CursorLeft - sX;

            var t = NewText(k);
            txt = txt.Insert(cursorPosition, t);
            realTxt = realTxt.Insert(cursorPosition, k.ToString());


            if (txt.Length <= maxWidth)
            {
                Write(txt[cursorPosition..], otherForeground ?? userForeground.Value, cB);
                CursorLeft = cursorPosition + t.Length + sX;
            }
            else if (allowedInvisibleChars && cursorPosition < maxWidth)
            {
                Write(txt.Substring(cursorPosition, (int)maxWidth.Value - cursorPosition),
                    otherForeground ?? userForeground.Value, cB);
                CursorLeft = cursorPosition + t.Length + sX;
            }
        }

        void DrawError()
        {
            txt = "";
            realTxt = "";

            if (msgOnError == null)
                return;

            ClearLine();
            Write(msgOnError,
                msgErrorForeground.Value, cB);

            if (wait != null)
                Thread.Sleep(wait.Value);

            ClearLine();
        }
    }

    private delegate IntPtr GetStdHandle(int nStdHandle);

    private delegate bool GetConsoleMode(IntPtr hConsoleHandle, out int lpMode);

    private delegate bool SetConsoleMode(IntPtr hConsoleHandle, int dwMode);

    private delegate ReadOnlyDictionary<ConsoleColor, Color> CurrentBackgroundFromWindowsCMD();
}