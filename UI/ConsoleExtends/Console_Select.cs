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
    /// <param name="returnOnKey">Where key is press the int get return where is not the correct console key return null
    /// to continue</param>
    /// <param name="options">The list of options for the user to select from.</param>
    /// <returns>The index of the selected option.</returns>
    public static int Select(SelectOption design, Border border, bool clearOnExit,
        Func<ConsoleKey, int?>? returnOnKey = null, params string[] options)
    {
        var cv = CursorVisible;
        CursorVisible = false;
        var selectedIndex = 0;

        var startX = CursorLeft;
        var startY = CursorTop;
        var offset = 0;

        SetCursorPosition((int)border.Start.X, (int)border.Start.Y);

        while (true)
        {
            SetCursorPosition(Math.Min(startX, WindowWidth - 1), Math.Min(startY, WindowHeight - 1));
            border.Draw();

            var visibleOptions = options.Skip(offset).Take((int)border.Size.Y - 2).ToArray();

            for (var i = 0; i < visibleOptions.Length; i++)
            {
                var actualIndex = i + offset;
                if (actualIndex == selectedIndex)
                {
                    BackgroundColor = design.SelectBackground;
                    ForegroundColor = design[i] ?? design.SelectForeground;
                }
                else
                {
                    BackgroundColor = design.Background;
                    ForegroundColor = design[i, false] ?? design.Foreground;
                }

                SetCursorPosition(Math.Min((int)border.Start.X + 1, WindowWidth - 1),
                    Math.Min((int)border.Start.Y + i + 1, WindowHeight - 1));
                Write(visibleOptions[i].PadRight((int)border.Size.X - 2));
                ResetColor();
            }

            var keyInfo = ReadKey(intercept: true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (selectedIndex > 0)
                    {
                        selectedIndex--;
                        if (selectedIndex < offset)
                            offset--;
                    }

                    break;

                case ConsoleKey.DownArrow:
                    if (selectedIndex < options.Length - 1)
                    {
                        selectedIndex++;
                        if (selectedIndex >= offset + visibleOptions.Length)
                            offset++;
                    }

                    break;

                case ConsoleKey.Enter:
                    goto exit;
                default:
                    var rs = returnOnKey?.Invoke(keyInfo.Key);
                    if (rs != null)
                    {
                        selectedIndex = rs.Value;
                        goto exit;
                    }

                    break;
            }
        }

        exit:

        if (clearOnExit)
        {
            border.Clear();
            CursorLeft = startX;
            CursorTop = startY;
        }

        CursorVisible = cv;
        return selectedIndex;
    }

    /// <summary>
    /// Displays a list of options for the user to select using the up and down arrow keys.
    /// </summary>
    /// <param name="options">The list of options for the user to select from.</param>
    /// <returns>The index of the selected option.</returns>
    public static int Select(params string[] options)
    {
        var length = options.Max(line => line!.Length) + 2;
        var sX = Convert.ToInt32(Math.Floor(((WindowWidth - CursorLeft + 1) - length) / 2.0));
        sX = (sX >= 0) ? sX : 0;
        sX = (sX <= WindowWidth) ? sX : 0;

        var borderSize = new Vector2(sX, CursorTop);
        var border = Border.DoubleLine(start: borderSize,
            size: new Vector2(length, options.Length + 2));

        return Select(new SelectOption(ConsoleColor.White, ConsoleColor.Black,
            BackgroundColor, ConsoleColor.Cyan), border, true, null, options);
    }

    /// <summary>
    /// Allows the user to interactively select a file or directory from the given starting directory.
    /// </summary>
    /// <param name="startDirectory">The starting directory from which the file or subdirectory selection begins.</param>
    /// <param name="deep">Specifies how many directory levels deep the selection should go. A negative value means there's no limit.</param>
    /// <returns>Returns the path to the selected file. If a directory is selected, the method will navigate into that directory and continue the selection process.</returns>
    /// <exception cref="System.IO.DirectoryNotFoundException">Thrown when the provided start directory is not found.</exception>
    /// <remarks>
    /// This method uses a console-based UI for file and directory selection. The user can navigate through directories and select a file.
    /// If the user presses the backspace key, it signals a request to go up to the parent directory.
    /// </remarks>
    public static string SelectFile(string startDirectory, int deep = -1)
    {
        if (File.Exists(startDirectory))
            return startDirectory;

        if (!Directory.Exists(startDirectory))
            throw new DirectoryNotFoundException("Use a valid Directory Path");

        var pathList = new Stack<string>();

        pathList.Push(startDirectory);

        loop:

        _Select(pathList.Peek(), out var currentPath, out var isFile, out var isRequestParent);

        if (isRequestParent)
        {
            if (pathList.Count > 1)
                currentPath = pathList.Pop();

            goto loop;
        }

        if (isFile)
            return currentPath;
        if (deep < 0 || pathList.Count <= deep)
            pathList.Push(currentPath);

        goto loop;

        static void _Select(string path, out string pathSelect, out bool isFile, out bool isRequestParent)
        {
            var a = Directory.GetFiles(path);
            var b = Directory.GetDirectories(path);
            var c = a.Select(Path.GetFileName).Concat(b.Select(Path.GetFileName)).ToArray();

            var length = c.Max(line => line!.Length) + 2;
            var sX = Convert.ToInt32(Math.Floor(((WindowWidth - CursorLeft + 1) - length) / 2.0));
            sX = (sX >= 0) ? sX : 0;
            sX = (sX <= WindowWidth) ? sX : 0;

            var borderSize = new Vector2(sX, CursorTop);
            var border = Border.DoubleLine(start: borderSize,
                size: new Vector2(length, WindowHeight - borderSize.Y));

            var option = new SelectOption(ConsoleColor.White, ConsoleColor.Black,
                BackgroundColor, ConsoleColor.Magenta);

            for (var i = 0; i < a.Length; i++)
                option[i, false] = ConsoleColor.Cyan;

            for (var i = 0; i < b.Length; i++)
                option[i + a.Length, false] = ConsoleColor.White;

            var s = Select(option, border, true, key =>
            {
                return key switch
                {
                    ConsoleKey.Backspace => -100,
                    _ => null
                };
            }, c!);

            if (s == -100)
            {
                isRequestParent = true;
                isFile = false;
                pathSelect = path;
            }
            else
            {
                isRequestParent = false;
                pathSelect = s < a.Length ? a[s] : b[s - a.Length];
                isFile = s < a.Length;
            }
        }
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
        private readonly Dictionary<int, ConsoleColor> _selectColorForIndex = new Dictionary<int, ConsoleColor>();
        private readonly Dictionary<int, ConsoleColor> _colorForIndex = new Dictionary<int, ConsoleColor>();

        /// <summary>
        /// Set Foreground per Index
        /// </summary>
        /// <param name="index">the index from option</param>
        /// <param name="isSelect">the color for select or not</param>
        public ConsoleColor? this[int index, bool isSelect = true]
        {
            get
            {
                if (isSelect)
                    return _selectColorForIndex.TryGetValue(index, out var value) ? value : null;
                else
                    return _colorForIndex.TryGetValue(index, out var value) ? value : null;
            }
            set
            {
                if (isSelect)
                {
                    _selectColorForIndex.Remove(index, out _);

                    if (value != null)
                        _selectColorForIndex.Add(index, value.Value);
                }
                else
                {
                    _colorForIndex.Remove(index, out _);

                    if (value != null)
                        _colorForIndex.Add(index, value.Value);
                }
            }
        }

        public SelectOption(ConsoleColor? foregroundColor = null,
            ConsoleColor? backgroundColor = null,
            ConsoleColor? selectForeground = null,
            ConsoleColor? selectBackground = null)
        {
            Foreground = foregroundColor ?? ForegroundColor;
            Background = backgroundColor ?? BackgroundColor;
            SelectForeground = selectForeground ?? ConsoleColor.Gray;
            SelectBackground = selectBackground ?? ForegroundColor;
            _selectColorForIndex = new Dictionary<int, ConsoleColor>();
            _colorForIndex = new Dictionary<int, ConsoleColor>();
        }
    }

    /// <summary>
    /// Represents a border with customizable characters, colors, and other features that can be drawn in the console.
    /// </summary>
    public sealed class Border
    {
        private readonly int _x = CursorLeft;
        private readonly int _y = CursorTop;

        public readonly Vector2 Start;


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

            Start = new Vector2(_x, _y);
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

            /*prevForeground = ForegroundColor;
            for (var i = 0; i < _size.Y - 2; i++)
            {
                if (i + _scrollOffset < _contentBuffer.Count)
                {
                    WriteAt(_contentBuffer[i + _scrollOffset], _x + 1, _y + i + 1);
                }
            }

            ForegroundColor = prevForeground;*/

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
                WriteAt(shadowChar.ToString(), (int)(_x + _size.X), _y + i);
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