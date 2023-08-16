using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace Yannick.Extensions.StringExtensions;

/// <summary>
/// Include extensions for the class <see cref="string"/>
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// Converts a string to a byte array using the specified encoding.
    /// </summary>
    /// <param name="source">The string to convert.</param>
    /// <param name="encoding">The character encoding to use. If null, UTF-8 will be used.</param>
    /// <returns>The convert string to a byte array</returns>
    public static byte[] ToByteArray(this string source, Encoding? encoding = null)
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