using System.Text;

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
}