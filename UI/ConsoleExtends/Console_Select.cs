using System;
using System.Numerics;

namespace Yannick.UI;

public partial class Console
{
    /// <summary>
    /// Displays a list of options for the user to select using the up and down arrow keys.
    /// </summary>
    /// <param name="design">The appearance design for the selection menu.</param>
    /// <param name="border">The border design for the selection menu.</param>
    /// <param name="clearOnExit">Clear all lines after enter ?</param>
    /// <param name="options">The list of options for the user to select from.</param>
    /// <returns>The index of the selected option.</returns>
    public static int Select(SelectOption design, Border border, bool clearOnExit, params string[] options)
    {
        var cv = CursorVisible;
        CursorVisible = false;
        var selectedIndex = 0;
        var startX = Console.CursorLeft;
        var startY = Console.CursorTop;

        // Adjust the border size if there are more options than the border can display
        if (options.Length > border.Size.Y - 2)
        {
            border.SetContent(options.Take((int)border.Size.Y - 2));
        }
        else
        {
            border.SetContent(options);
        }

        while (true)
        {
            Console.SetCursorPosition(Math.Min(startX, Console.WindowWidth - 1),
                Math.Min(startY, Console.WindowHeight - 1));
            border.Draw();

            var visibleOptions = options.Skip(border._scrollOffset).Take((int)border.Size.Y - 2).ToArray();

            for (var i = 0; i < visibleOptions.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = design.SelectBackground;
                    Console.ForegroundColor = design.SelectForeground;
                }
                else
                {
                    Console.BackgroundColor = design.Background;
                    Console.ForegroundColor = design.Foreground;
                }

                Console.SetCursorPosition(Math.Min(startX + 1, Console.WindowWidth - 1),
                    Math.Min(startY + i + 1, Console.WindowHeight - 1));
                Console.Write(visibleOptions[i].PadRight((int)border.Size.X - 2));
                Console.ResetColor();
            }

            var keyInfo = Console.ReadKey(intercept: true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (selectedIndex == 0)
                        border.ScrollUp();
                    selectedIndex = (selectedIndex - 1 + visibleOptions.Length) % visibleOptions.Length;
                    break;

                case ConsoleKey.DownArrow:
                    if (selectedIndex == visibleOptions.Length - 1)
                        border.ScrollDown();
                    selectedIndex = (selectedIndex + 1) % visibleOptions.Length;
                    break;

                case ConsoleKey.Enter:
                    if (clearOnExit)
                    {
                        for (var i = 0; i < visibleOptions.Length; i++)
                        {
                            Console.SetCursorPosition(Math.Min(startX, Console.WindowWidth - 1),
                                Math.Min(startY + i, Console.WindowHeight - 1));
                            Console.Write(new string(' ', (int)border.Size.X));
                        }
                    }

                    CursorVisible = cv;
                    return selectedIndex + border._scrollOffset;
            }
        }
    }

    /// <summary>
    /// Displays a list of options for the user to select using the up and down arrow keys.
    /// </summary>
    /// <param name="options">The list of options for the user to select from.</param>
    /// <returns>The index of the selected option.</returns>
    public static int Select(params string[] options) => Select(new SelectOption
    {
        Foreground = ConsoleColor.White,
        Background = ConsoleColor.Black,
        SelectBackground = BackgroundColor,
        SelectForeground = ConsoleColor.Cyan
    }, Border.LinePointStyle(), true, options);

    /// <summary>
    /// Defines the appearance options for the selection menu.
    /// </summary>
    public readonly struct SelectOption
    {
        public ConsoleColor Foreground { get; init; }
        public ConsoleColor Background { get; init; }
        public ConsoleColor SelectForeground { get; init; }
        public ConsoleColor SelectBackground { get; init; }

        public SelectOption(ConsoleColor? foregroundColor = null,
            ConsoleColor? backgroundColor = null,
            ConsoleColor? selectForeground = null,
            ConsoleColor? selectBackground = null)
        {
            Foreground = foregroundColor ?? ForegroundColor;
            Background = backgroundColor ?? BackgroundColor;
            SelectForeground = selectForeground ?? ConsoleColor.Gray;
            SelectBackground = selectBackground ?? ForegroundColor;
        }
    }

    /// <summary>
    /// Represents a border with customizable characters, colors, and other features that can be drawn in the console.
    /// </summary>
    public sealed class Border : IDisposable
    {
        private readonly int _x = CursorLeft;
        private readonly int _y = CursorTop;
        private List<string> _contentBuffer = new List<string>();
        private CancellationTokenSource? _cts;
        private bool _monitorWindowSize = false;
        internal int _scrollOffset = 0;


        public Border(
            Vector2? size = null,
            char? top = null, char? bottom = null, char? left = null, char? right = null,
            char? topLeft = null, char? topRight = null, char? bottomLeft = null, char? bottomRight = null,
            string title = "",
            ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null,
            bool shadow = false)
        {
            Size = size ?? new Vector2(Console.WindowWidth, Console.WindowHeight);
            Top = top ?? '-';
            Bottom = bottom ?? '-';
            Left = left ?? '|';
            Right = right ?? '|';
            TopLeft = topLeft ?? '+';
            TopRight = topRight ?? '+';
            BottomLeft = bottomLeft ?? '+';
            BottomRight = bottomRight ?? '+';
            Title = title;
            Foreground = foregroundColor ?? ForegroundColor;
            Background = backgroundColor ?? BackgroundColor;
            Shadow = shadow;
        }

        public Vector2 _size { get; set; }

        public Vector2 Size
        {
            get => _size;
            init => _size = value;
        }

        public char Top { get; init; }
        public char Bottom { get; init; }
        public char Left { get; init; }
        public char Right { get; init; }
        public char TopLeft { get; init; }
        public char TopRight { get; init; }
        public char BottomLeft { get; init; }
        public char BottomRight { get; init; }
        public string Title { get; init; }
        public ConsoleColor Foreground { get; init; }
        public ConsoleColor Background { get; init; }
        public bool Shadow { get; init; }

        public bool MonitorWindowSize
        {
            get { return _monitorWindowSize; }
            set
            {
                _monitorWindowSize = value;
                if (_monitorWindowSize)
                    StartWindowSizeMonitor();
                else
                    _cts?.Cancel();
            }
        }

        public void Dispose()
        {
            _cts?.Cancel();
        }

        private void StartWindowSizeMonitor()
        {
            _cts = new CancellationTokenSource();
            Task.Run(() =>
            {
                var lastWidth = Console.WindowWidth;
                var lastHeight = Console.WindowHeight;
                while (!_cts.Token.IsCancellationRequested)
                {
                    if (lastWidth != Console.WindowWidth || lastHeight != Console.WindowHeight)
                    {
                        lastWidth = Console.WindowWidth;
                        lastHeight = Console.WindowHeight;
                        _size = new Vector2(lastWidth, lastHeight);
                        Draw();
                    }

                    Thread.Sleep(100);
                }
            }, _cts.Token);
        }

        public void SetContent(IEnumerable<string> content)
        {
            _contentBuffer = content.ToList();
            var maxWidth = _contentBuffer.Max(line => line.Length);
            _size = new Vector2(maxWidth + 2, _contentBuffer.Count + 2);
        }

        public void ScrollUp()
        {
            if (_scrollOffset > 0)
                _scrollOffset--;
        }

        public void ScrollDown()
        {
            if (_scrollOffset < _contentBuffer.Count - Size.Y)
                _scrollOffset++;
        }

        public void Draw()
        {
            var prevForeground = ForegroundColor;
            var prevBackground = BackgroundColor;

            ForegroundColor = Foreground;
            BackgroundColor = Background;

            for (var i = 0; i < Size.Y; i++)
            {
                if (i == 0)
                {
                    WriteAt(TopLeft + Top.ToString().PadRight((int)Size.X - 2, Top) + TopRight, _x, _y + i);
                    if (!string.IsNullOrEmpty(Title))
                    {
                        WriteAt(Title, _x + 2, _y);
                    }
                }
                else if (i == Size.Y - 1)
                {
                    WriteAt(BottomLeft + Bottom.ToString().PadRight((int)Size.X - 2, Bottom) + BottomRight, _x, _y + i);
                }
                else
                {
                    WriteAt(Left + new string(' ', (int)Size.X - 2) + Right, _x, _y + i);
                }
            }

            prevForeground = ForegroundColor;
            for (var i = 0; i < Size.Y - 2; i++)
            {
                if (i + _scrollOffset < _contentBuffer.Count)
                {
                    WriteAt(_contentBuffer[i + _scrollOffset], _x + 1, _y + i + 1);
                }
            }

            ForegroundColor = prevForeground;

            if (Shadow)
            {
                DrawShadow();
            }

            ForegroundColor = prevForeground;
            BackgroundColor = prevBackground;
        }

        private void DrawShadow()
        {
            var shadowChar = '░';
            var shadowColor = ConsoleColor.DarkGray;
            var prevForeground = ForegroundColor;
            ForegroundColor = shadowColor;

            for (var i = 1; i <= Size.Y; i++)
            {
                WriteAt(shadowChar.ToString(), (int)(_x + Size.X), (int)_y + i);
            }

            WriteAt(new string(shadowChar, (int)Size.X + 1), _x + 1, (int)(_y + Size.Y));

            ForegroundColor = prevForeground;
        }

        public void Clear()
        {
            var prevBackground = BackgroundColor;
            BackgroundColor = Console.BackgroundColor;

            for (var i = 0; i < Size.Y; i++)
            {
                WriteAt(new string(' ', (int)Size.X), _x, _y + i);
            }

            if (Shadow) // new
            {
                WriteAt(new string(' ', (int)Size.X + 1), _x + 1, (int)(_y + Size.Y));
                for (var i = 1; i <= Size.Y; i++)
                {
                    WriteAt(" ", (int)(_x + Size.X), _y + i);
                }
            }

            BackgroundColor = prevBackground;
        }

        private void WriteAt(string text, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }

        #region STATIC

        public static Border LinePointStyle(Vector2? size = null, string title = "",
            ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null,
            bool shadow = false)
        {
            return new Border(size, '┄', '┄', '┆', '┆', '┌', '┐', '└', '┘',
                title, foregroundColor, backgroundColor, shadow);
        }

        public static Border DoubleLine(Vector2? size = null, string title = "",
            ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null,
            bool shadow = false)
        {
            return new Border(size, '═', '═', '║', '║', '╔', '╗', '╚', '╝',
                title, foregroundColor, backgroundColor, shadow);
        }

        #endregion
    }
}