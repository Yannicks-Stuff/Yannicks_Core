namespace Yannick.UI;

public partial class Console
{
    /// <summary>
    /// Represents a console animation that displays a sequence of characters with specified colors and timing.
    /// </summary>
    public class Animation
    {
        private volatile bool _isRunning;
        private volatile bool _isStop;
        private int Lines;

        /// <summary>
        /// Initializes a new instance of the <see cref="Animation"/> class with the specified animation steps.
        /// </summary>
        /// <param name="steps">The animation steps.</param>
        public Animation(params StepOption[] steps)
        {
            Queue = steps;
        }

        /// <summary>
        /// The sequence of animation steps.
        /// </summary>
        public StepOption[] Queue { get; init; }

        /// <summary>
        /// Whether the displayed characters should be cleared after the animation finishes.
        /// </summary>
        public bool ClearOnExit { get; init; } = true;

        private (int, int) StartCursor => (CursorLeft, CursorTop);

        /// <summary>
        /// Represents an animation sequence of rotating characters.
        /// </summary>
        /// <remarks>
        /// This animation consists of rotating between the '/' and '\' characters.
        /// </remarks>
        public static Animation Animation1 => new(
            new StepOption { Wait = TimeSpan.FromMilliseconds(500), Char = '/', RemoveAfter = true },
            new StepOption
                { Wait = TimeSpan.FromMilliseconds(500), Char = '\\', RemoveAfter = true })
        {
            ClearOnExit = true
        };

        /// <summary>
        /// Represents an animation sequence of consecutive dots.
        /// </summary>
        /// <remarks>
        /// This animation consists of showing dots '.' one after the other.
        /// </remarks>
        public static Animation Animation2 => new(
            new StepOption { Wait = TimeSpan.FromMilliseconds(500), Char = '.', RemoveAfter = false },
            new StepOption { Wait = TimeSpan.FromMilliseconds(500), Char = '.', RemoveAfter = false },
            new StepOption { Wait = TimeSpan.FromMilliseconds(500), Char = '.', RemoveAfter = false },
            new StepOption { Wait = TimeSpan.FromMilliseconds(500), Char = '.', RemoveAfter = false })
        {
            ClearOnExit = true
        };

        /// <summary>
        /// Gets or sets the current cursor top position.
        /// </summary>
        public int CursorTop
        {
            get => Console.CursorTop;
            set => Console.CursorTop = value;
        }

        /// <summary>
        /// Gets or sets the current cursor left position.
        /// </summary>
        public int CursorLeft
        {
            get => Console.CursorLeft;
            set => Console.CursorLeft = value;
        }

        private void cursor(out int x, out int y)
        {
            x = StartCursor.Item1;
            y = StartCursor.Item2;
        }

        /// <summary>
        /// Starts the animation asynchronously.
        /// </summary>
        public async Task StartAsync()
        {
            _isRunning = true;
            _isStop = false;
            do
            {
                cursor(out var xs, out var ys);
                Lines = 0;

                foreach (var step in Queue)
                {
                    if (!_isRunning)
                        goto exit;

                    Lines++;
                    SetCursorPosition(xs + Lines, ys);
                    Write(step.Char.ToString(), ForegroundColor, BackgroundColor);

                    await Task.Delay(step.Wait);

                    if (!step.RemoveAfter)
                        continue;

                    Backspace();
                    Lines--;
                }

                for (var i = xs; i < Lines + 1; i++)
                    Backspace();

                continue;
                exit:
                break;
            } while (true);

            if (ClearOnExit) Clear();

            _isRunning = false;
            _isStop = true;
        }

        private void Backspace()
        {
            Console.CursorLeft--;
            Write(" ");
            Console.CursorLeft--;
        }

        /// <summary>
        /// Stops the animation.
        /// </summary>
        public void Stop()
        {
            if (_isStop)
                return;

            _isRunning = false;
            while (_isStop)
            {
            }

            if (ClearOnExit) Clear();
        }

        private void Clear()
        {
            cursor(out var xs, out var ys);
            for (var i = Lines - 1; i < xs; i++)
                Backspace();
        }

        /// <summary>
        /// Represents an individual animation step with character, color settings, and timing.
        /// </summary>
        public readonly struct StepOption
        {
            /// <summary>
            /// The character to display in the animation step.
            /// </summary>
            public char Char { get; init; }

            /// <summary>
            /// The foreground color of the character in the animation step.
            /// </summary>
            public ConsoleColor Foreground { get; init; }

            /// <summary>
            /// The background color behind the character in the animation step.
            /// </summary>
            public ConsoleColor Background { get; init; }

            /// <summary>
            /// The time to wait after displaying the character before moving to the next step.
            /// </summary>
            public TimeSpan Wait { get; init; }

            /// <summary>
            /// Whether the character should be removed after the wait time.
            /// </summary>
            public bool RemoveAfter { get; init; }
        }
    }
}