using System.Diagnostics.CodeAnalysis;
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
        get => global::System.Console.BackgroundColor;
        set => SetColor(null, value);
    }

    public static ConsoleColor ForegroundColor
    {
        get => global::System.Console.ForegroundColor;
        set => SetColor(value);
    }


    public static int BufferHeight
    {
        get => global::System.Console.BufferHeight;
        [SupportedOSPlatform("windows")] set { global::System.Console.BufferHeight = value; }
    }

    public static int BufferWidth
    {
        get => global::System.Console.BufferHeight;
        [SupportedOSPlatform("windows")] set => global::System.Console.BufferHeight = value;
    }

    [SupportedOSPlatform("windows")] public static bool CapsLock => global::System.Console.CapsLock;

    public static int CursorLeft
    {
        get => global::System.Console.CursorLeft;
        set => global::System.Console.CursorLeft = value;
    }

    public static int CursorSize
    {
        get => global::System.Console.CursorSize;
        [SupportedOSPlatform("windows")] set => global::System.Console.CursorSize = value;
    }

    public static int CursorTop
    {
        get => global::System.Console.CursorTop;
        set => global::System.Console.CursorTop = value;
    }

    public static bool CursorVisible
    {
        [SupportedOSPlatform("windows")] get => global::System.Console.CursorVisible;
        set => global::System.Console.CursorVisible = value;
    }

    public static TextWriter Error => global::System.Console.Error;


    public static TextReader In => global::System.Console.In;

    public static Encoding InputEncoding
    {
        get => global::System.Console.InputEncoding;
        set => global::System.Console.InputEncoding = value;
    }


    public static bool IsErrorRedirected => global::System.Console.IsErrorRedirected;

    public static bool IsInputRedirected => global::System.Console.IsInputRedirected;

    public static bool IsOutputRedirected => global::System.Console.IsOutputRedirected;

    public static bool KeyAvailable => global::System.Console.KeyAvailable;

    public static int LargestWindowHeight => global::System.Console.LargestWindowHeight;

    public static int LargestWindowWidth => global::System.Console.LargestWindowWidth;

    [SupportedOSPlatform("windows")] public static bool NumberLock => global::System.Console.NumberLock;

    public static TextWriter Out => global::System.Console.Out;

    public static Encoding OutputEncoding
    {
        get => global::System.Console.InputEncoding;
        set => global::System.Console.InputEncoding = value;
    }

    public static string Title
    {
        [SupportedOSPlatform("windows")] get => global::System.Console.Title;
        [MethodImpl(MethodImplOptions.Synchronized)]
        set => global::System.Console.Title = value;
    }

    public static bool TreatControlCAsInput
    {
        get => global::System.Console.TreatControlCAsInput;
        set => global::System.Console.TreatControlCAsInput = value;
    }

    public static int WindowHeight
    {
        get => global::System.Console.WindowHeight;
        set => global::System.Console.WindowHeight = value;
    }

    public static int WindowLeft
    {
        get => global::System.Console.WindowLeft;
        [SupportedOSPlatform("windows")] set => global::System.Console.WindowLeft = value;
    }

    public static int WindowTop
    {
        get => global::System.Console.WindowTop;
        [SupportedOSPlatform("windows")] set => global::System.Console.WindowTop = value;
    }

    public static int WindowWidth
    {
        get => global::System.Console.WindowWidth;
        set => global::System.Console.WindowWidth = value;
    }


    public static void Flush()
    {
        while (KeyAvailable)
        {
            ReadKey(intercept: true);
        }
    }


    public static void Clear() => global::System.Console.Clear();

    public static void Beep()
    {
        global::System.Console.Beep();
    }

    [SupportedOSPlatform("windows")]
    public static void Beep(int frequency, int duration)
    {
        global::System.Console.Beep(frequency, duration);
    }

    [SupportedOSPlatform("windows")]
    public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight,
        int targetLeft, int targetTop)
    {
        global::System.Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop);
    }

    [SupportedOSPlatform("windows")]
    public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight,
        int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
    {
        global::System.Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop,
            sourceChar, sourceForeColor, sourceBackColor);
    }

    public static Stream OpenStandardError()
    {
        return global::System.Console.OpenStandardError();
    }

    public static Stream OpenStandardError(int bufferSize)
    {
        return global::System.Console.OpenStandardError(bufferSize);
    }

    public static Stream OpenStandardInput()
    {
        return global::System.Console.OpenStandardInput();
    }

    public static Stream OpenStandardInput(int bufferSize)
    {
        return global::System.Console.OpenStandardInput(bufferSize);
    }

    public static Stream OpenStandardOutput()
    {
        return global::System.Console.OpenStandardOutput();
    }

    public static Stream OpenStandardOutput(int bufferSize)
    {
        return global::System.Console.OpenStandardOutput(bufferSize);
    }

    public static int Read()
    {
        return global::System.Console.Read();
    }

    public static ConsoleKeyInfo ReadKey()
    {
        return global::System.Console.ReadKey();
    }

    public static ConsoleKeyInfo ReadKey(bool intercept)
    {
        return global::System.Console.ReadKey(intercept);
    }


    public static string? ReadLine() => global::System.Console.ReadLine();


    public static void ResetColor()
    {
        global::System.Console.ResetColor();
    }

    [SupportedOSPlatform("windows")]
    public static void SetBufferSize(int width, int height)
    {
        global::System.Console.SetBufferSize(width, height);
    }

    public static void SetCursorPosition(int left, int top)
    {
        global::System.Console.SetCursorPosition(left, top);
    }

    public static void SetError(TextWriter newError)
    {
        global::System.Console.SetError(newError);
    }

    public static void SetIn(TextReader newIn)
    {
        global::System.Console.SetIn(newIn);
    }

    public static void SetOut(TextWriter newOut)
    {
        global::System.Console.SetOut(newOut);
    }

    [SupportedOSPlatform("windows")]
    public static void SetWindowPosition(int left, int top)
    {
        global::System.Console.SetWindowPosition(left, top);
    }

    public static void SetWindowSize(int width, int height)
    {
        global::System.Console.SetWindowSize(width, height);
    }


    public static void Write(string text)
    {
        global::System.Console.Write(text);
    }


    public static void Write(object text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        global::System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void Write(string text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        global::System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void Write(long text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        global::System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void Write(float text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        global::System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void Write(int text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        global::System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void Write(ulong text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        global::System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void Write(uint text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        global::System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void Write(ushort text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        global::System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void Write(short text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        global::System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void Write(sbyte text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        global::System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void Write(byte text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        global::System.Console.Write(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void WriteLine(object? text = null)
    {
        if (text == null)
        {
            global::System.Console.WriteLine();
        }
        else
        {
            global::System.Console.WriteLine(text);
        }
    }

    public static void WriteLine(object text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        var oldForegroundColor = ForegroundColor;
        var oldBackgroundColor = BackgroundColor;
        ForegroundColor = foregroundColor;
        BackgroundColor = (backgroundColor ?? oldBackgroundColor);
        global::System.Console.WriteLine(text);
        ForegroundColor = oldForegroundColor;
        BackgroundColor = oldBackgroundColor;
    }


    public static void WriteLine(string text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        WriteLine((object)text, foregroundColor, backgroundColor);
    }


    public static void WriteLine(long text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        WriteLine((object)text, foregroundColor, backgroundColor);
    }


    public static void WriteLine(float text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        WriteLine((object)text, foregroundColor, backgroundColor);
    }


    public static void WriteLine(int text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        WriteLine((object)text, foregroundColor, backgroundColor);
    }


    public static void WriteLine(ulong text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        WriteLine((object)text, foregroundColor, backgroundColor);
    }


    public static void WriteLine(uint text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        WriteLine((object)text, foregroundColor, backgroundColor);
    }


    public static void WriteLine(ushort text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        WriteLine((object)text, foregroundColor, backgroundColor);
    }


    public static void WriteLine(short text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        WriteLine((object)text, foregroundColor, backgroundColor);
    }


    public static void WriteLine(sbyte text, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        WriteLine((object)text, foregroundColor, backgroundColor);
    }
}