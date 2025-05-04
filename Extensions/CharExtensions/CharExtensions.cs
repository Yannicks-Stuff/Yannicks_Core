using System.Globalization;

namespace Yannick.Extensions.CharExtensions;

public static class CharExtensions
{
    public static char ToUpper(this char c, CultureInfo? cultureInfo = null) =>
        cultureInfo == null ? char.ToUpper(c) : char.ToUpper(c, cultureInfo);

    public static char ToLower(this char c, CultureInfo? cultureInfo = null) =>
        cultureInfo == null ? char.ToLower(c) : char.ToLower(c, cultureInfo);

    public static char ToUpperInvariant(this char c) => char.ToUpperInvariant(c);
    public static char ToLowerInvariant(this char c) => char.ToLowerInvariant(c);


    public static bool IsAscii(this char c) => char.IsAscii(c);

    public static bool IsAscii(this char c, char minInclusive, char maxInclusive) =>
        char.IsBetween(c, minInclusive, maxInclusive);

    public static bool IsControl(this char c) => char.IsControl(c);
    public static bool IsDigit(this char c) => char.IsDigit(c);
    public static bool IsLetter(this char c) => char.IsLetter(c);
    public static bool IsLower(this char c) => char.IsLower(c);
    public static bool IsNumber(this char c) => char.IsNumber(c);
    public static bool IsPunctuation(this char c) => char.IsPunctuation(c);
    public static bool IsSeparator(this char c) => char.IsSeparator(c);
    public static bool IsSurrogate(this char c) => char.IsSurrogate(c);
    public static bool IsSymbol(this char c) => char.IsSymbol(c);
    public static bool IsUpper(this char c) => char.IsUpper(c);
    public static bool IsAsciiDigit(this char c) => char.IsAsciiDigit(c);
    public static bool IsAsciiLetter(this char c) => char.IsAsciiLetter(c);
    public static bool IsHighSurrogate(this char c) => char.IsHighSurrogate(c);
    public static bool IsLowSurrogate(this char c) => char.IsLowSurrogate(c);
    public static bool IsSurrogatePair(this char c, char low) => char.IsSurrogatePair(c, low);
    public static bool IsWhiteSpace(this char c) => char.IsWhiteSpace(c);
    public static bool IsAsciiHexDigit(this char c) => char.IsAsciiHexDigit(c);
    public static bool IsAsciiLetterLower(this char c) => char.IsAsciiLetterLower(c);
    public static bool IsAsciiLetterUpper(this char c) => char.IsAsciiLetterUpper(c);
    public static bool IsLetterOrDigit(this char c) => char.IsLetterOrDigit(c);
    public static bool IsAsciiHexDigitLower(this char c) => char.IsAsciiHexDigitLower(c);
    public static bool IsAsciiHexDigitUpper(this char c) => char.IsAsciiHexDigitUpper(c);
    public static bool IsAsciiLetterOrDigit(this char c) => char.IsAsciiLetterOrDigit(c);
}