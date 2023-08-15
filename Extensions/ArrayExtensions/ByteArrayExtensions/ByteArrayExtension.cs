using System.Text;

namespace Yannick.Extensions.ArrayExtensions.ByteArrayExtensions;

/// <summary>
/// Include extensions for <see cref="byte"/> arrays
/// </summary>
public static class ByteArrayExtension
{
    /// <summary>
    /// Converts a byte array to a string using the specified encoding.
    /// </summary>
    /// <param name="source">The byte array to convert.</param>
    /// <param name="encoding">The character encoding to use. If null, UTF-8 will be used.</param>
    /// <returns>A string that represents the converted byte array.</returns>
    public static string ToString(this byte[] source, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;

        return encoding.GetString(source);
    }

    /// <summary>
    /// Searches for a character in the byte array and returns the bytes up to the position of the character.
    /// </summary>
    /// <param name="source">The byte array to search.</param>
    /// <param name="searchChar">The character to search for.</param>
    /// <param name="includeSearchChar">If set to <c>true</c>, the returned array will include the search character; otherwise, it will not.</param>
    /// <returns>The bytes up to the position of the search character. If the search character is not found, returns the original array.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the source array is null.</exception>
    public static byte[] GetBytesUntil(this byte[] source, char searchChar, bool includeSearchChar = false)
    {
        ArgumentNullException.ThrowIfNull(source);

        var index = Array.IndexOf(source, (byte)searchChar);
        if (index == -1)
            return source;

        var length = includeSearchChar ? index + 1 : index;
        var result = new byte[length];
        Array.Copy(source, result, length);
        return result;
    }
}