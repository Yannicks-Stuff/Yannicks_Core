namespace Yannick.UI;

public partial class Console
{
    /// <summary>
    /// Sets the foreground and/or background color of the console.
    /// </summary>
    /// <param name="fb">The color to set for the console's foreground. If null, the current color will be maintained.</param>
    /// <param name="bg">The color to set for the console's background. If null, the current color will be maintained.</param>
    public static void SetColor(ConsoleColor? fb = null, ConsoleColor? bg = null)
    {
        global::System.Console.ForegroundColor = fb ?? global::System.Console.ForegroundColor;
        global::System.Console.BackgroundColor = bg ?? global::System.Console.BackgroundColor;
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
}