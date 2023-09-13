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

        var startX = CursorLeft;
        var startY = CursorTop;

        if (options.Length > border.Size.Y - 2)
            border.SetContent(options.Take((int)border.Size.Y - 2));
        else
            border.SetContent(options);

        while (true)
        {
            SetCursorPosition(Math.Min(startX, WindowWidth - 1), Math.Min(startY, WindowHeight - 1));
            border.Draw();

            var visibleOptions = options.Skip(border._scrollOffset).Take((int)border.Size.Y - 2).ToArray();

            for (var i = 0; i < visibleOptions.Length; i++)
            {
                if (i == selectedIndex)
                {
                    BackgroundColor = design.SelectBackground;
                    ForegroundColor = design.SelectForeground;
                }
                else
                {
                    BackgroundColor = design.Background;
                    ForegroundColor = design.Foreground;
                }

                SetCursorPosition(Math.Min(startX + 1, WindowWidth - 1),
                    Math.Min(startY + i + 1, WindowHeight - 1));
                Write(visibleOptions[i].PadRight((int)border.Size.X - 2));
                ResetColor();
            }

            var keyInfo = ReadKey(intercept: true);

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
                        border.Clear();
                        /*for (var i = 0; i < visibleOptions.Length; i++)
                        {
                            SetCursorPosition(Math.Min(startX, WindowWidth - 1),
                                Math.Min(startY + i, WindowHeight - 1));
                            Write(new string(' ', (int)border.Size.X));
                        }*/
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
    public static int Select(params string[] options)
    {
        var sX = (from o in options
                select Convert.ToInt32(Math.Floor(((WindowWidth - CursorLeft + 1) - o.Length) / 2.0))
                into x3
                select ((x3 >= 0) ? x3 : 0)
                into x3
                select ((x3 <= WindowWidth) ? x3 : 0))
            .Prepend(0).Max();

        var borderSize = new Vector2(sX, CursorTop);
        var border = Border.DoubleLine(start: borderSize);

        CursorLeft = sX;

        return Select(new SelectOption
        {
            Foreground = ConsoleColor.White,
            Background = ConsoleColor.Black,
            SelectBackground = BackgroundColor,
            SelectForeground = ConsoleColor.Cyan
        }, border, true, options);
    }

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
            Vector2? start = null,
            Vector2? size = null,
            char? top = null, char? bottom = null, char? left = null, char? right = null,
            char? topLeft = null, char? topRight = null, char? bottomLeft = null, char? bottomRight = null,
            string title = "",
            ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null,
            bool shadow = false)
        {
            if (start.HasValue)
            {
                if ((int)start.Value.X >= 0)
                    _x = (int)start.Value.X;

                if ((int)start.Value.Y >= 0)
                    _y = (int)start.Value.Y;
            }

            Size = size ?? new Vector2(WindowWidth - _x, WindowHeight - _y);
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

        /*public bool MonitorWindowSize
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
        }*/

        public void Dispose()
        {
            _cts?.Cancel();
        }

        /*private void StartWindowSizeMonitor()
        {
            _cts = new CancellationTokenSource();
            Task.Run(() =>
            {
                var lastWidth = WindowWidth;
                var lastHeight = WindowHeight;
                while (!_cts.Token.IsCancellationRequested)
                {
                    if (lastWidth != WindowWidth || lastHeight != WindowHeight)
                    {
                        lastWidth = WindowWidth;
                        lastHeight = WindowHeight;
                        _size = new Vector2(lastWidth, lastHeight);
                        Draw();
                    }

                    Thread.Sleep(100);
                }
            }, _cts.Token);
        }*/

        public void SetContent(IEnumerable<string> content)
        {
            _contentBuffer = content.ToList();
            int maxWidth;
            if (_contentBuffer.Count == 0)
                maxWidth = 0;
            else
                maxWidth = _contentBuffer.Max(line => line.Length);

            _size = new Vector2(maxWidth + 2, _contentBuffer.Count + 2);
        }

        public void ScrollUp()
        {
            if (_scrollOffset > 0)
                _scrollOffset--;
        }

        public void ScrollDown()
        {
            if (_scrollOffset < _contentBuffer.Count - _size.Y)
                _scrollOffset++;
        }

        public void Draw()
        {
            var prevForeground = ForegroundColor;
            var prevBackground = BackgroundColor;

            ForegroundColor = Foreground;
            BackgroundColor = Background;

            for (var i = 0; i < _size.Y; i++)
            {
                if (i == 0)
                {
                    WriteAt(TopLeft + Top.ToString().PadRight((int)_size.X - 2, Top) + TopRight, _x, _y + i);
                    if (!string.IsNullOrEmpty(Title))
                    {
                        WriteAt(Title, _x + 2, _y);
                    }
                }
                else if (i == _size.Y - 1)
                {
                    WriteAt(BottomLeft + Bottom.ToString().PadRight((int)_size.X - 2, Bottom) + BottomRight, _x,
                        _y + i);
                }
                else
                {
                    WriteAt(Left + new string(' ', (int)_size.X - 2) + Right, _x, _y + i);
                }
            }

            prevForeground = ForegroundColor;
            for (var i = 0; i < _size.Y - 2; i++)
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

            for (var i = 1; i <= _size.Y; i++)
            {
                WriteAt(shadowChar.ToString(), (int)(_x + _size.X), (int)_y + i);
            }

            WriteAt(new string(shadowChar, (int)_size.X + 1), _x + 1, (int)(_y + _size.Y));

            ForegroundColor = prevForeground;
        }

        public void Clear()
        {
            var prevBackground = BackgroundColor;
            BackgroundColor = BackgroundColor;

            for (var i = 0; i < _size.Y; i++)
            {
                WriteAt(new string(' ', (int)_size.X), _x, _y + i);
            }

            if (Shadow) // new
            {
                WriteAt(new string(' ', (int)_size.X + 1), _x + 1, (int)(_y + _size.Y));
                for (var i = 1; i <= _size.Y; i++)
                {
                    WriteAt(" ", (int)(_x + _size.X), _y + i);
                }
            }

            BackgroundColor = prevBackground;

            SetCursorPosition(_x, _y);
        }

        private void WriteAt(string text, int x, int y)
        {
            SetCursorPosition(x, y);
            Write(text);
        }

        #region STATIC

        public static Border LinePointStyle(Vector2? start = null, Vector2? size = null, string title = "",
            ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null,
            bool shadow = false)
        {
            return new Border(start, size, '┄', '┄', '┆', '┆', '┌', '┐', '└', '┘',
                title, foregroundColor, backgroundColor, shadow);
        }

        public static Border DoubleLine(Vector2? start = null, Vector2? size = null, string title = "",
            ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null,
            bool shadow = false)
        {
            return new Border(start, size, '═', '═', '║', '║', '╔', '╗', '╚', '╝',
                title, foregroundColor, backgroundColor, shadow);
        }

        #endregion
    }
}