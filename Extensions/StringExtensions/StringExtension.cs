using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace Yannick.Extensions.StringExtensions;

/// <summary>
/// Include extensions for the class <see cref="string"/>
/// </summary>
public static class StringExtension
{
    private static readonly char[] DefaultEscapeChars =
    [
        '\0', // Null character
        '\a', // Alert
        '\b', // Backspace
        '\f', // Form feed
        '\n', // New line
        '\r', // Carriage return
        '\t', // Horizontal tab
        '\v', // Vertical tab
        '\\', // Backslash
        '\'', // Single quote
        '\"' // Double quote
    ];

    // Precompute the default escape characters lookup for quick access
    private static readonly bool[] DefaultEscapeLookup = InitializeLookup(DefaultEscapeChars);

    /// <summary>
    /// Determines whether the specified string contains only characters from the provided set.
    /// </summary>
    /// <param name="source">The string to evaluate.</param>
    /// <param name="allowedChars">A span of characters that are allowed in the string.</param>
    /// <returns>
    /// <c>true</c> if the string contains only characters from <paramref name="allowedChars"/>; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/> or <paramref name="allowedChars"/> is <c>null</c>.
    /// </exception>
    public static bool ContainsOnly(this string source, ReadOnlySpan<char> allowedChars)
    {
        ArgumentNullException.ThrowIfNull(source);

        if (allowedChars.IsEmpty)
            return source.Length == 0;

        var charLookup = new bool[char.MaxValue + 1];

        foreach (var c in allowedChars)
            charLookup[c] = true;

        foreach (var c in source.AsSpan())
            if (!charLookup[c])
                return false;

        return true;
    }

    /// <summary>
    /// Determines whether the specified string contains only characters from the provided set.
    /// </summary>
    /// <param name="source">The string to evaluate.</param>
    /// <param name="allowedChars">A string containing the set of allowed characters.</param>
    /// <returns>
    /// <c>true</c> if the string contains only characters from <paramref name="allowedChars"/>; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/> or <paramref name="allowedChars"/> is <c>null</c>.
    /// </exception>
    public static bool ContainsOnly(this string source, string allowedChars)
    {
        ArgumentNullException.ThrowIfNull(allowedChars);

        if (allowedChars.Length == 0)
            return source.Length == 0;

        return source.ContainsOnly(allowedChars.AsSpan());
    }

    /// <summary>
    /// Determines whether the specified string contains only characters from the provided set using a regular expression.
    /// </summary>
    /// <param name="source">The string to evaluate.</param>
    /// <param name="allowedChars">A Regex containing the set of allowed characters.</param>
    /// <returns>
    /// <c>true</c> if the string contains only characters from <paramref name="allowedChars"/>; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/> or <paramref name="allowedChars"/> is <c>null</c>.
    /// </exception>
    public static bool IsContainsOnlyRegex(this string source,
        [StringSyntax(StringSyntaxAttribute.Regex, nameof(allowedChars))] string allowedChars)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(allowedChars);

        if (allowedChars.Length == 0)
            return source.Length == 0;

        var escapedAllowedChars = Regex.Escape(allowedChars);
        var pattern = $"^[{escapedAllowedChars}]+$";

        return Regex.IsMatch(source, pattern, RegexOptions.Compiled | RegexOptions.CultureInvariant);
    }

    /// <summary>
    /// Replaces all specified escape characters in the string with an empty string.
    /// If no characters are specified, it replaces a default set of C# escape characters.
    /// </summary>
    /// <param name="input">The input string to process.</param>
    /// <param name="charsToReplace">
    /// An optional array of characters to replace.
    /// - If <c>null</c>, replaces a default set of C# escape characters.
    /// - If an empty array, dynamically identifies and replaces all escape characters present in the string.
    /// </param>
    /// <returns>
    /// A new string with the specified characters replaced by an empty string.
    /// </returns>
    public static string ReplaceAllEscapesEvents(this string input, char[]? charsToReplace = null)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        bool[] replaceLookup;
        if (charsToReplace == null)
        {
            replaceLookup = DefaultEscapeLookup;
        }
        else if (charsToReplace.Length == 0)
        {
            var presentEscapeChars = DefaultEscapeChars.Where(input.Contains).ToArray();

            if (presentEscapeChars.Length == 0)
                return input;

            replaceLookup = new bool[65536];
            foreach (var c in presentEscapeChars)
                replaceLookup[c] = true;
        }
        else
        {
            replaceLookup = new bool[65536];
            foreach (var c in charsToReplace)
            {
                replaceLookup[c] = true;
            }
        }

        Span<char> buffer = stackalloc char[input.Length];
        var writeIndex = 0;

        foreach (var current in input.Where(current => !replaceLookup[current]))
            buffer[writeIndex++] = current;

        return writeIndex == input.Length ? input : new string(buffer[..writeIndex]);
    }

    /// <summary>
    /// Initializes a lookup table for the specified characters.
    /// </summary>
    /// <param name="chars">The characters to include in the lookup table.</param>
    /// <returns>A boolean array where indices corresponding to the specified characters are set to true.</returns>
    private static bool[] InitializeLookup(char[] chars)
    {
        var lookup = new bool[65536];
        foreach (var c in chars)
        {
            lookup[c] = true;
        }

        return lookup;
    }

    /// <summary>
    /// Converts a string to a byte array using the specified encoding.
    /// </summary>
    /// <param name="source">The string to convert.</param>
    /// <param name="encoding">The character encoding to use. If null, UTF-8 will be used.</param>
    /// <returns>The convert string to a byte array</returns>
    public static byte[]? ToByteArray(this string source, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;

        return encoding.GetBytes(source);
    }

    /// <summary>
    /// Converts a hexadecimal string to a byte array, with optional encoding.
    /// </summary>
    /// <param name="hexString">The hexadecimal string to convert.</param>
    /// <param name="encoding">The optional character encoding to use when converting the byte array to a string and back.</param>
    /// <returns>The byte array.</returns>
    public static byte[] FromHexStringToByteArray(this string hexString, Encoding? encoding = null)
    {
        var byteArray = Convert.FromHexString(hexString);

        if (encoding == null)
            return byteArray;

        var decodedString = encoding.GetString(byteArray);
        byteArray = encoding.GetBytes(decodedString);

        return byteArray;
    }

    /// <summary>
    /// Converts the current string to a Match object by applying the specified regular expression.
    /// </summary>
    /// <param name="str">The current string.</param>
    /// <param name="regex">The regular expression pattern to match.</param>
    /// <param name="options">A bitwise combination of the enumeration values that specify options for matching.</param>
    /// <returns>A Match object that contains information about the match.</returns>
    /// <exception cref="RegexMatchTimeoutException">A time-out occurred.</exception>
    public static Match ToMatch(this string str,
        [StringSyntax(StringSyntaxAttribute.Regex, nameof(options))]
        string regex, RegexOptions options = RegexOptions.None)
        => new Regex(regex, options).Match(str);

    /// <summary>
    /// Converts the current string to a collection of Match objects by applying the specified regular expression.
    /// </summary>
    /// <param name="str">The current string.</param>
    /// <param name="regex">The regular expression pattern to match.</param>
    /// <param name="options">A bitwise combination of the enumeration values that specify options for matching.</param>
    /// <returns>A collection of Match objects that contains information about the matches.</returns>
    /// <exception cref="RegexMatchTimeoutException">A time-out occurred.</exception>
    public static MatchCollection ToMatches(this string str,
        [StringSyntax(StringSyntaxAttribute.Regex, nameof(options))]
        string regex, RegexOptions options = RegexOptions.None)
        => new Regex(regex, options).Matches(str);
}