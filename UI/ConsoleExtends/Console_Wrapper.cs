using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Text;

namespace Yannick.UI;

/// <summary>
/// Wrap <see cref="System.Console"/> and provides utility methods for enhanced console functionality.
/// </summary>
[SuppressMessage("Interoperability",
    "CA1416:Plattformkompatibilität überprüfen")] //TODO Fix with Properties --> [SupportedOSPlatform("windows")]
public partial class Console
{
    public static ConsoleColor BackgroundColor
    {
        get => System.Console.BackgroundColor;
        set => SetColor(null, value);
    }

    public static ConsoleColor ForegroundColor
    {
        get => System.Console.ForegroundColor;
        set => SetColor(value);
    }


    public static int BufferHeight
    {
        get => System.Console.BufferHeight;
        [SupportedOSPlatform("windows")] set { System.Console.BufferHeight = value; }
    }

    public static int BufferWidth
    {
        get => System.Console.BufferHeight;
        [SupportedOSPlatform("windows")] set => System.Console.BufferHeight = value;
    }

    [SupportedOSPlatform("windows")] public static bool CapsLock => System.Console.CapsLock;

    public static int CursorLeft
    {
        get => System.Console.CursorLeft;
        set => System.Console.CursorLeft = value;
    }

    public static int CursorSize
    {
        get => System.Console.CursorSize;
        [SupportedOSPlatform("windows")] set => System.Console.CursorSize = value;
    }

    public static int CursorTop
    {
        get => System.Console.CursorTop;
        set => System.Console.CursorTop = value;
    }

    public static bool CursorVisible
    {
        [SupportedOSPlatform("windows")] get => System.Console.CursorVisible;
        set => System.Console.CursorVisible = value;
    }

    public static TextWriter Error => System.Console.Error;


    public static TextReader In => System.Console.In;

    public static Encoding InputEncoding
    {
        get => System.Console.InputEncoding;
        set => System.Console.InputEncoding = value;
    }


    public static bool IsErrorRedirected => System.Console.IsErrorRedirected;

    public static bool IsInputRedirected => System.Console.IsInputRedirected;

    public static bool IsOutputRedirected => System.Console.IsOutputRedirected;

    public static bool KeyAvailable => System.Console.KeyAvailable;

    public static int LargestWindowHeight => System.Console.LargestWindowHeight;

    public static int LargestWindowWidth => System.Console.LargestWindowWidth;

    [SupportedOSPlatform("windows")] public static bool NumberLock => System.Console.NumberLock;

    public static TextWriter Out => System.Console.Out;

    public static Encoding OutputEncoding
    {
        get => System.Console.InputEncoding;
        set => System.Console.InputEncoding = value;
    }

    public static string Title
    {
        [SupportedOSPlatform("windows")] get => System.Console.Title;
        [MethodImpl(MethodImplOptions.Synchronized)]
        set => System.Console.Title = value;
    }

    public static bool TreatControlCAsInput
    {
        get => System.Console.TreatControlCAsInput;
        set => System.Console.TreatControlCAsInput = value;
    }

    public static int WindowHeight
    {
        get => System.Console.WindowHeight;
        set => System.Console.WindowHeight = value;
    }

    public static int WindowLeft
    {
        get => System.Console.WindowLeft;
        [SupportedOSPlatform("windows")] set => System.Console.WindowLeft = value;
    }

    public static int WindowTop
    {
        get => System.Console.WindowTop;
        [SupportedOSPlatform("windows")] set => System.Console.WindowTop = value;
    }

    public static int WindowWidth
    {
        get => System.Console.WindowWidth;
        set => System.Console.WindowWidth = value;
    }


    public static void Flush()
    {
        while (KeyAvailable)
        {
            ReadKey(intercept: true);
        }
    }


    public static void Clear() => System.Console.Clear();
    public static void Clear24() => Fill(0, 0, WindowWidth, WindowHeight, ' ', ForegroundColor24, BackgroundColor24);


    public static void Fill(int startX = 0, int startY = 0, int width = 0, int height = 0, char character = '*',
        Color? foregroundColor = null, Color? backgroundColor = null)
    {
        var sA = CursorVisible;
        CursorVisible = false;
        var (xC, yC) = Cursor;

        startX = Math.Max(0, startX);
        startY = Math.Max(0, startY);
        width = Math.Max(0, width);
        width = Math.Min(width, WindowWidth - startX);
        height = Math.Max(0, height);
        height = Math.Min(height, WindowHeight - startY);


        for (var y = startY; y < startY + height; y++)
        {
            for (var x = startX; x < startX + width; x++)
            {
                SetCursorPosition(x, y);
                Write(character.ToString(), foregroundColor, backgroundColor);
            }
        }

        SetCursorPosition(xC, yC);
        CursorVisible = sA;
    }


    public static void Beep()
    {
        System.Console.Beep();
    }

    [SupportedOSPlatform("windows")]
    public static void Beep(int frequency, int duration)
    {
        System.Console.Beep(frequency, duration);
    }

    [SupportedOSPlatform("windows")]
    public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight,
        int targetLeft, int targetTop)
    {
        System.Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop);
    }

    [SupportedOSPlatform("windows")]
    public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight,
        int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
    {
        System.Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop,
            sourceChar, sourceForeColor, sourceBackColor);
    }

    public static Stream OpenStandardError()
    {
        return System.Console.OpenStandardError();
    }

    public static Stream OpenStandardError(int bufferSize)
    {
        return System.Console.OpenStandardError(bufferSize);
    }

    public static Stream OpenStandardInput()
    {
        return System.Console.OpenStandardInput();
    }

    public static Stream OpenStandardInput(int bufferSize)
    {
        return System.Console.OpenStandardInput(bufferSize);
    }

    public static Stream OpenStandardOutput()
    {
        return System.Console.OpenStandardOutput();
    }

    public static Stream OpenStandardOutput(int bufferSize)
    {
        return System.Console.OpenStandardOutput(bufferSize);
    }

    public static int Read()
    {
        return System.Console.Read();
    }

    public static ConsoleKeyInfo ReadKey()
    {
        return System.Console.ReadKey();
    }

    public static ConsoleKeyInfo ReadKey(bool intercept)
    {
        return System.Console.ReadKey(intercept);
    }

    public static void ResetColor()
    {
        System.Console.ResetColor();
    }

    [SupportedOSPlatform("windows")]
    public static void SetBufferSize(int width, int height)
    {
        System.Console.SetBufferSize(width, height);
    }

    public static void SetCursorPosition(int left, int top)
    {
        System.Console.SetCursorPosition(left, top);
    }

    public static void SetError(TextWriter newError)
    {
        System.Console.SetError(newError);
    }

    public static void SetIn(TextReader newIn)
    {
        System.Console.SetIn(newIn);
    }

    public static void SetOut(TextWriter newOut)
    {
        System.Console.SetOut(newOut);
    }

    [SupportedOSPlatform("windows")]
    public static void SetWindowPosition(int left, int top)
    {
        System.Console.SetWindowPosition(left, top);
    }

    public static void SetWindowSize(int width, int height)
    {
        System.Console.SetWindowSize(width, height);
    }


    public static void Write(string text)
    {
        System.Console.Write(text);
    }


    public static void Write(object text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void Write(string text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void Write(long text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void Write(float text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void Write(int text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void Write(ulong text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void Write(uint text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void Write(ushort text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void Write(short text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void Write(sbyte text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void Write(byte text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void WriteLine(object? text = null)
    {
        if (text == null)
        {
            System.Console.WriteLine();
        }
        else
        {
            System.Console.WriteLine(text);
        }
    }

    public static void WriteLine(object text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        System.Console.WriteLine(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void WriteLine(string text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        System.Console.WriteLine(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void WriteLine(long text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        System.Console.WriteLine(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void WriteLine(float text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        System.Console.WriteLine(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void WriteLine(int text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        System.Console.WriteLine(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void WriteLine(ulong text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        System.Console.WriteLine(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void WriteLine(uint text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        System.Console.WriteLine(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void WriteLine(ushort text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        System.Console.WriteLine(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void WriteLine(short text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        System.Console.WriteLine(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void WriteLine(sbyte text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        System.Console.WriteLine(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }
}