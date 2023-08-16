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

    /// <summary>
    /// Converts a byte array to a Base64 encoded string, with optional encoding.
    /// </summary>
    /// <param name="array">The byte array to convert.</param>
    /// <param name="base64FormattingOptions">Specifies whether relevant line breaks should be inserted.</param>
    /// <param name="encoding">The optional character encoding to use when converting the Base64 string to bytes and back.</param>
    /// <returns>The Base64 encoded string.</returns>
    public static string ToBase64String(this byte[] array,
        Base64FormattingOptions base64FormattingOptions = Base64FormattingOptions.None,
        Encoding? encoding = null)
    {
        var base64String = Convert.ToBase64String(array, base64FormattingOptions);

        if (encoding == null)
            return base64String;

        var bytes = encoding.GetBytes(base64String);
        base64String = encoding.GetString(bytes);
        return base64String;
    }

    /// <summary>
    /// Converts a byte array to a hexadecimal string, with optional encoding.
    /// </summary>
    /// <param name="array">The byte array to convert.</param>
    /// <param name="encoding">The optional character encoding to use when converting the hexadecimal string to bytes and back.</param>
    /// <param name="includeHyphens">Whether to include hyphens between byte values in the output string.</param>
    /// <returns>The hexadecimal string.</returns>
    public static string ToHexString(this byte[] array, Encoding? encoding = null, bool includeHyphens = false)
    {
        var hexString = BitConverter.ToString(array);

        if (!includeHyphens)
            hexString = hexString.Replace("-", "");

        if (encoding != null)
            hexString = encoding.GetString(array);

        return hexString;
    }
}