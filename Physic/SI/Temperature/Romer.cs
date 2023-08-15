using System.Runtime.InteropServices;

namespace Yannick.Physic.SI.Temperature;

/// <summary>
/// Represents a temperature value in Romer.
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Romer : ITemperature<Romer>
{
    public override bool Equals(object? obj)
    {
        return obj is Romer other && Equals(other);
    }

    public override int GetHashCode()
    {
        return m_value.GetHashCode();
    }

    internal readonly decimal m_value; // Do not rename (binary serialization)

    public Romer(decimal mValue)
    {
        m_value = mValue < 0 ? 0 : mValue;
    }


    public static Romer operator +(Romer a, Romer b) => new(a.m_value + b.m_value);
    public static Romer operator -(Romer a, Romer b) => new(a.m_value - b.m_value);
    public static Romer operator /(Romer a, Romer b) => new(a.m_value / b.m_value);
    public static Romer operator *(Romer a, Romer b) => new(a.m_value * b.m_value);
    public static Romer operator +(Romer a, decimal b) => new(a.m_value + b);
    public static Romer operator -(Romer a, decimal b) => new(a.m_value - b);
    public static Romer operator /(Romer a, decimal b) => new(a.m_value / b);
    public static Romer operator *(Romer a, decimal b) => new(a.m_value * b);
    
        public static Romer operator +(Romer a, Delisle b) => new(a.ToKelvin().m_value + b.ToKelvin().m_value);
    public static Romer operator -(Romer a, Delisle b) => new(a.ToKelvin().m_value - b.ToKelvin().m_value);
    public static Romer operator /(Romer a, Delisle b) => new(a.ToKelvin().m_value / b.ToKelvin().m_value);
    public static Romer operator *(Romer a, Delisle b) => new(a.ToKelvin().m_value * b.ToKelvin().m_value);

    public static Romer operator +(Romer a, Fahrenheit b) =>
        new(a.ToKelvin().m_value + b.ToKelvin().m_value);

    public static Romer operator -(Romer a, Fahrenheit b) =>
        new(a.ToKelvin().m_value - b.ToKelvin().m_value);

    public static Romer operator /(Romer a, Fahrenheit b) =>
        new(a.ToKelvin().m_value / b.ToKelvin().m_value);

    public static Romer operator *(Romer a, Fahrenheit b) =>
        new(a.ToKelvin().m_value * b.ToKelvin().m_value);

    public static Romer operator +(Romer a, Kelvin b) => new(a.ToKelvin().m_value + b.m_value);
    public static Romer operator -(Romer a, Kelvin b) => new(a.ToKelvin().m_value - b.m_value);
    public static Romer operator /(Romer a, Kelvin b) => new(a.ToKelvin().m_value / b.m_value);
    public static Romer operator *(Romer a, Kelvin b) => new(a.ToKelvin().m_value * b.m_value);
    public static Romer operator +(Romer a, Newton b) => new(a.ToKelvin().m_value + b.ToKelvin().m_value);
    public static Romer operator -(Romer a, Newton b) => new(a.ToKelvin().m_value - b.ToKelvin().m_value);
    public static Romer operator /(Romer a, Newton b) => new(a.ToKelvin().m_value / b.ToKelvin().m_value);
    public static Romer operator *(Romer a, Newton b) => new(a.ToKelvin().m_value * b.ToKelvin().m_value);
    public static Romer operator +(Romer a, Celsius b) => new(a.ToKelvin().m_value + b.ToKelvin().m_value);
    public static Romer operator -(Romer a, Celsius b) => new(a.ToKelvin().m_value - b.ToKelvin().m_value);
    public static Romer operator /(Romer a, Celsius b) => new(a.ToKelvin().m_value / b.ToKelvin().m_value);
    public static Romer operator *(Romer a, Celsius b) => new(a.ToKelvin().m_value * b.ToKelvin().m_value);
    public static Romer operator +(Romer a, Rankine b) => new(a.ToKelvin().m_value + b.ToKelvin().m_value);
    public static Romer operator -(Romer a, Rankine b) => new(a.ToKelvin().m_value - b.ToKelvin().m_value);
    public static Romer operator /(Romer a, Rankine b) => new(a.ToKelvin().m_value / b.ToKelvin().m_value);
    public static Romer operator *(Romer a, Rankine b) => new(a.ToKelvin().m_value * b.ToKelvin().m_value);
    public static Romer operator +(Romer a, Reaumur b) => new(a.ToKelvin().m_value + b.ToKelvin().m_value);
    public static Romer operator -(Romer a, Reaumur b) => new(a.ToKelvin().m_value - b.ToKelvin().m_value);
    public static Romer operator /(Romer a, Reaumur b) => new(a.ToKelvin().m_value / b.ToKelvin().m_value);
    public static Romer operator *(Romer a, Reaumur b) => new(a.ToKelvin().m_value * b.ToKelvin().m_value);

    public static Romer operator +(Romer value) => value;
    public static Romer operator -(Romer value) => value;
    public static Romer operator --(Romer value) => new(value.m_value - 1);
    public static Romer operator ++(Romer value) => new(value.m_value + 1);

    public static bool operator ==(Romer left, Romer right) => left.m_value == right.m_value;
    public static bool operator !=(Romer left, Romer right) => left.m_value != right.m_value;
    public static bool operator ==(Romer left, decimal right)=> left.m_value == right;
    public static bool operator !=(Romer left, decimal right)=> left.m_value != right;

    public static implicit operator Romer(decimal b) => new(b);
    public static implicit operator Romer(Delisle b) => new((b.m_value + 100m) * 0.35000m + 7.50m);
    public static implicit operator Romer(Kelvin b) => new((b.m_value - 273.15m) * 0.52500m + 7.50m);
    public static implicit operator Romer(Celsius b) => new(b.m_value * 0.52500m + 7.50m);
    public static implicit operator Romer(Fahrenheit b) => new((b.m_value - 32m) * 0.29167m + 7.50m);
    public static implicit operator Romer(Newton b) => new(b.m_value * 1.5909m + 7.50m);
    public static implicit operator Romer(Rankine b) => new((b.m_value - 491.67m) * 0.29167m + 7.50m);
    public static implicit operator Romer(Reaumur b) => new(b.m_value * 0.65625m + 7.50m);



    /// <summary>Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.</summary>
    /// <param name="other">An object to compare with this instance.</param>
    /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings:
    /// <list type="table"><listheader><term> Value</term><description> Meaning</description></listheader><item><term> Less than zero</term><description> This instance precedes <paramref name="other" /> in the sort order.</description></item><item><term> Zero</term><description> This instance occurs in the same position in the sort order as <paramref name="other" />.</description></item><item><term> Greater than zero</term><description> This instance follows <paramref name="other" /> in the sort order.</description></item></list></returns>
    public int CompareTo(decimal? other) => m_value.CompareTo(other);

    /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
    public bool Equals(decimal? other) => m_value.Equals(other);
    
    public string ToString(string? format, IFormatProvider? formatProvider)
        => m_value.ToString(format, formatProvider);

    /// <summary>Tries to format the value of the current instance into the provided span of characters.</summary>
    /// <param name="destination">When this method returns, this instance's value formatted as a span of characters.</param>
    /// <param name="charsWritten">When this method returns, the number of characters that were written in <paramref name="destination"/>.</param>
    /// <param name="format">A span containing the characters that represent a standard or custom format string that defines the acceptable format for <paramref name="destination"/>.</param>
    /// <param name="provider">An optional object that supplies culture-specific formatting information for <paramref name="destination"/>.</param>
    /// <returns><see langword="true"/> if the formatting was successful; otherwise, <see langword="false"/>.</returns>
    /// <remarks>
    /// An implementation of this interface should produce the same string of characters as an implementation of <see cref="IFormattable.ToString(string?, IFormatProvider?)"/>
    /// on the same type.
    /// TryFormat should return false only if there is not enough space in the destination buffer. Any other failures should throw an exception.
    /// </remarks>
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format,
        IFormatProvider? provider)
        => m_value.TryFormat(destination, out charsWritten, format, provider);

    /// <summary>Parses a string into a value.</summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s" />.</param>
    /// <returns>The result of parsing <paramref name="s" />.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="s" /> is <c>null</c>.</exception>
    /// <exception cref="FormatException"><paramref name="s" /> is not in the correct format.</exception>
    /// <exception cref="OverflowException"><paramref name="s" /> is not representable by <typeparamref name="TSelf" />.</exception>
    public static Romer Parse(string s, IFormatProvider? provider)
        => decimal.Parse(s, provider);

    /// <summary>Tries to parse a string into a value.</summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s" />.</param>
    /// <param name="result">On return, contains the result of successfully parsing <paramref name="s" /> or an undefined value on failure.</param>
    /// <returns><c>true</c> if <paramref name="s" /> was successfully parsed; otherwise, <c>false</c>.</returns>
    public static bool TryParse(string? s, IFormatProvider? provider, out Romer result)
    {
        var rs = decimal.TryParse(s, provider, out var f);
        result = new Romer(f);
        return rs;
    }
    
    /// <summary>Parses a span of characters into a value.</summary>
    /// <param name="s">The span of characters to parse.</param>
    /// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s" />.</param>
    /// <returns>The result of parsing <paramref name="s" />.</returns>
    /// <exception cref="FormatException"><paramref name="s" /> is not in the correct format.</exception>
    /// <exception cref="OverflowException"><paramref name="s" /> is not representable by <typeparamref name="TSelf" />.</exception>
    public static Romer Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        => decimal.Parse(s, provider);

    /// <summary>Tries to parse a span of characters into a value.</summary>
    /// <param name="s">The span of characters to parse.</param>
    /// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s" />.</param>
    /// <param name="result">On return, contains the result of successfully parsing <paramref name="s" /> or an undefined value on failure.</param>
    /// <returns><c>true</c> if <paramref name="s" /> was successfully parsed; otherwise, <c>false</c>.</returns>
    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Romer result)
    {
        var rs = decimal.TryParse(s, provider, out var f);
        result = new Romer(f);
        return rs;
    }
    
    public Kelvin ToKelvin() => new(((m_value - 7.5m) / 0.52500m) + 273.15m);
    public static Kelvin ToKelvin(Romer i) => i;
    public bool Equals(Romer other) => other.m_value.Equals(m_value);
}