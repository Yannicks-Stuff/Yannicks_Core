using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Yannick.Extensions.DictionaryExtensions;
using Yannick.VM.CLR;

namespace Yannick.UI;

public partial class Console
{
    public enum AnsiGraphicMode
    {
        NORMAL,
        BOLD,
        ITALIC,
        UNDERLINE,
        STRIKETHROUGH,
        BLINK,
        INVERT_COLORS,
        BLINK_BACKGROUND,
        FRAME
    }

    public enum CursorDirection
    {
        UP,
        DOWN,
        RIGHT,
        LEFT
    }

    public enum EraseMode
    {
        TO_END_OF_SCREEN,
        FROM_BEGINNING_OF_SCREEN,
        ENTIRE_SCREEN,
        TO_END_OF_LINE,
        FROM_BEGINNING_OF_LINE,
        ENTIRE_LINE
    }

    public enum FormatStyle
    {
        RESET,
        BOLD,
        ITALIC,
        UNDERLINE,
        STRIKETHROUGH,
        NEW_LINE,
        FOREGROUND_COLOR,
        BACKGROUND_COLOR,
        TEXT,
        MOVE_CURSOR,
        SET_CURSOR_POSITION,
        ERASE
    }

    public enum LineStyle
    {
        NORMAL,
        NEW_LINE
    }

    private static Color _foregroundColor24 = Color.White;
    private static Color _backgroundColor24 = Color.Black;

    private static readonly IReadOnlyDictionary<ConsoleColor, Color> consoleColors;
    private static readonly IReadOnlyDictionary<Color, ConsoleColor> colorsConsole;


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

            linker = new Linker("Console", "Yannick.Native.OS.Windows.UI");
            consoleColors = linker.LinkStatic<CurrentBackgroundFromWindowsCMD>("CurrentColorPlate")!();
            colorsConsole = consoleColors.Swap();
        }
        else
        {
            consoleColors = new Dictionary<ConsoleColor, Color>
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
            colorsConsole = consoleColors.Swap();
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
        if (!OperatingSystem.IsWindows())
            return;

        if (colorsConsole.ContainsKey(fb))
            ForegroundColor = colorsConsole[fb];
        else
            ForegroundColor = ConvertFromColor(fb);

        if (colorsConsole.ContainsKey(bg))
            BackgroundColor = colorsConsole[bg];
        else
            BackgroundColor = ConvertFromColor(bg, false);
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

    public static string GetTrueColorAnsiCode(Color color, bool isBackground = false)
    {
        return isBackground ? $"\x1B[48;2;{color.R};{color.G};{color.B}m" : $"\x1B[38;2;{color.R};{color.G};{color.B}m";
    }

    private static void WriteColor(Color? foreground, Color? background)
    {
        if (foreground.HasValue)
        {
            Write(GetTrueColorAnsiCode(foreground.Value));
        }

        if (background.HasValue)
        {
            Write(GetTrueColorAnsiCode(background.Value, true));
        }
    }

    private static void WriteStyleStart(AnsiGraphicMode mode)
    {
        switch (mode)
        {
            case AnsiGraphicMode.NORMAL:
                break;
            case AnsiGraphicMode.BOLD:
                Write("\x1B[1m");
                break;
            case AnsiGraphicMode.ITALIC:
                Write("\x1B[3m");
                break;
            case AnsiGraphicMode.UNDERLINE:
                Write("\x1B[4m");
                break;
            case AnsiGraphicMode.STRIKETHROUGH:
                Write("\x1B[9m");
                break;
            case AnsiGraphicMode.BLINK:
                Write("\x1B[5m");
                break;
            case AnsiGraphicMode.INVERT_COLORS:
                Write("\x1B[7m");
                break;
            case AnsiGraphicMode.BLINK_BACKGROUND:
                Write("\x1B[6m");
                break;
            case AnsiGraphicMode.FRAME:
                Write("\x1B[51m");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }
    }

    private static void WriteStyleStop(AnsiGraphicMode mode)
    {
        switch (mode)
        {
            case AnsiGraphicMode.NORMAL:
                break;
            case AnsiGraphicMode.BOLD:
                Write("\x1B[22m");
                break;
            case AnsiGraphicMode.ITALIC:
                Write("\x1B[23m");
                break;
            case AnsiGraphicMode.UNDERLINE:
                Write("\x1B[24m");
                break;
            case AnsiGraphicMode.STRIKETHROUGH:
                Write("\x1B[29m");
                break;
            case AnsiGraphicMode.BLINK:
                Write("\x1B[25m");
                break;
            case AnsiGraphicMode.INVERT_COLORS:
                Write("\x1B[27m");
                break;
            case AnsiGraphicMode.BLINK_BACKGROUND:
                Write("\x1B[26m");
                break;
            case AnsiGraphicMode.FRAME:
                Write("\x1B[54m");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }
    }

    private static void WriteMoveCursor(CursorDirection direction)
    {
        switch (direction)
        {
            case CursorDirection.UP:
                Write("\x1B[A");
                break;
            case CursorDirection.DOWN:
                Write("\x1B[B");
                break;
            case CursorDirection.RIGHT:
                Write("\x1B[C");
                break;
            case CursorDirection.LEFT:
                Write("\x1B[D");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }

    private static void WriteSetCursorPosition(int y, int x)
    {
        Write($"\x1B[{y};{x}H");
    }

    private static void WriteErase(EraseMode mode)
    {
        switch (mode)
        {
            case EraseMode.TO_END_OF_SCREEN:
                Write("\x1B[0J");
                break;
            case EraseMode.FROM_BEGINNING_OF_SCREEN:
                Write("\x1B[1J");
                break;
            case EraseMode.ENTIRE_SCREEN:
                Write("\x1B[2J");
                break;
            case EraseMode.TO_END_OF_LINE:
                Write("\x1B[0K");
                break;
            case EraseMode.FROM_BEGINNING_OF_LINE:
                Write("\x1B[1K");
                break;
            case EraseMode.ENTIRE_LINE:
                Write("\x1B[2K");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }
    }

    public static void WriteFormat(string? txt = null, Color? foreground = null, Color? background = null,
        AnsiGraphicMode? graphicMode = null, LineStyle? lineStyle = null, TimeSpan? waitBetweenChars = null)
    {
        var oldF = _foregroundColor24;
        var olfB = _backgroundColor24;

        WriteColor(foreground ?? _foregroundColor24, background ?? _backgroundColor24);
        WriteStyleStart(graphicMode ?? AnsiGraphicMode.NORMAL);
        if (waitBetweenChars == null)
            Write(txt ?? string.Empty);
        else
        {
            txt ??= " ";
            if (txt.Length == 0)
                txt = " ";

            foreach (var c in txt.ToCharArray())
            {
                Thread.Sleep(waitBetweenChars.Value);
                System.Console.Write(c);
            }
        }

        WriteStyleStop(graphicMode ?? AnsiGraphicMode.NORMAL);
        Write(lineStyle == LineStyle.NEW_LINE ? "\n" : "");
        WriteColor(oldF, olfB);
    }


    public sealed class Formatter
    {
        private readonly Color _baseBackground = BackgroundColor24;
        private readonly Color _baseForeground = ForegroundColor24;
        public static Formatter Create => new();

        public Formatter UseForeground(Color foreground)
        {
            SetColor(foreground, BackgroundColor24);
            return this;
        }

        public Formatter UseBackground(Color background)
        {
            SetColor(ForegroundColor24, background);
            return this;
        }

        public Formatter Wait(TimeSpan time)
        {
            Thread.Sleep(time);
            return this;
        }

        public Formatter UseStyle(AnsiGraphicMode style)
        {
            WriteStyleStart(style);
            return this;
        }

        public Formatter StopUseStyle(AnsiGraphicMode style)
        {
            WriteStyleStop(style);
            return this;
        }

        public Formatter Write(string txt)
        {
            Console.Write(txt);
            return this;
        }

        public Formatter Write(string txt, Color fb, Color? bg = null, TimeSpan? waitBetweenChars = null)
        {
            if (waitBetweenChars == null)
                Console.Write(txt, fb, bg);
            else
                foreach (var c in txt.ToCharArray())
                {
                    Console.Write(c.ToString(), fb, bg);
                    Thread.Sleep(waitBetweenChars.Value);
                }

            return this;
        }

        public Formatter MoveCursor(CursorDirection direction)
        {
            WriteMoveCursor(direction);
            return this;
        }

        public Formatter Erase(EraseMode mode)
        {
            WriteErase(mode);
            return this;
        }

        public Formatter MoveCursorToLeft(int n)
            => Write($"\x1B[{n}D");

        public Formatter DeleteCharactersToLeft(int n)
            => MoveCursorToLeft(n)
                .Write(new string(' ', n), _baseForeground, _baseBackground)
                .MoveCursorToLeft(n);

        public Formatter UseBlink()
        {
            WriteStyleStart(AnsiGraphicMode.BLINK);
            return this;
        }

        public Formatter ChangeCursorPosition(int x, int y)
        {
            WriteSetCursorPosition(x, y);
            return this;
        }

        public Formatter StopUseBlink()
        {
            WriteStyleStop(AnsiGraphicMode.BLINK);
            return this;
        }

        public Formatter SaveCursorPosition() => Write("\x1B[s");

        public Formatter RestoreCursorPosition() => Write("\x1B[u");

        public Formatter ScrollScreenUp(int n) => Write($"\x1B[{n}S");

        public Formatter NewLine()
        {
            WriteLine();
            return this;
        }

        public Formatter Reset()
        {
            Console.Write("\x1B[0m");
            ResetColor();
            SetColor(ConsoleColor.White, ConsoleColor.Black);
            return this;
        }

        public Formatter UseInvertColors()
        {
            WriteStyleStart(AnsiGraphicMode.INVERT_COLORS);
            return this;
        }

        public Formatter StopUseInvertColors()
        {
            WriteStyleStop(AnsiGraphicMode.INVERT_COLORS);
            return this;
        }

        public Formatter UseBlinkBackground()
        {
            WriteStyleStart(AnsiGraphicMode.BLINK_BACKGROUND);
            return this;
        }

        public Formatter StopUseBlinkBackground()
        {
            WriteStyleStop(AnsiGraphicMode.BLINK_BACKGROUND);
            return this;
        }

        public Formatter UseFrame()
        {
            WriteStyleStart(AnsiGraphicMode.FRAME);
            return this;
        }

        public Formatter StopUseFrame()
        {
            WriteStyleStop(AnsiGraphicMode.FRAME);
            return this;
        }

        public Formatter HideCursor() => Write("\x1B[?25l");

        public Formatter ShowCursor() => Write("\x1B[?25h");

        public Formatter ScrollScreenDown(int n) => Write($"\x1B[{n}T");

        public Formatter ScrollUp(int n) => Write($"\x1B[{n}S");

        public Formatter ScrollDown(int n) => Write($"\x1B[{n}T");

        public Formatter ChangeWindowSize(int width, int height)
            => Write($"\x1B[8;{height};{width}t");

        public Formatter SetConsoleTitle(string title)
            => Write($"\x1B]2;{title}\x1B\\");

        public Formatter DrawBoxAroundText(string text, int padding = 1)
        {
            var totalWidth = text.Length + 2 * padding + 2;
            Write(new string('═', totalWidth));
            NewLine();
            for (var i = 0; i < padding; i++)
            {
                Write($"║{new string(' ', text.Length + 2 * padding)}║");
                NewLine();
            }

            Write($"║{new string(' ', padding)}{text}{new string(' ', padding)}║");
            NewLine();
            for (var i = 0; i < padding; i++)
            {
                Write($"║{new string(' ', text.Length + 2 * padding)}║");
                NewLine();
            }

            Write(new string('═', totalWidth));
            return this;
        }

        public Formatter CenterText(string text)
        {
            var consoleWidth = WindowWidth;
            var padding = (consoleWidth - text.Length) / 2;
            Write(new string(' ', padding));
            Write(text);
            return this;
        }
    }
}