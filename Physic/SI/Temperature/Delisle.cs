using System.Runtime.InteropServices;

namespace Yannick.Physic.SI.Temperature;

/// <summary>
/// Represents a temperature value in Delisle.
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Delisle : ITemperature<Delisle>
{
    public override bool Equals(object? obj)
    {
        return obj is Delisle other && Equals(other);
    }

    public override int GetHashCode()
    {
        return m_value.GetHashCode();
    }

    internal readonly decimal m_value; // Do not rename (binary serialization)

    public Delisle(decimal mValue)
    {
        m_value = mValue < 0 ? 0 : mValue;
    }


    public static Delisle operator +(Delisle a, Delisle b) => new(a.m_value + b.m_value);
    public static Delisle operator -(Delisle a, Delisle b) => new(a.m_value - b.m_value);
    public static Delisle operator /(Delisle a, Delisle b) => new(a.m_value / b.m_value);
    public static Delisle operator *(Delisle a, Delisle b) => new(a.m_value * b.m_value);
    public static Delisle operator +(Delisle a, decimal b) => new(a.m_value + b);
    public static Delisle operator -(Delisle a, decimal b) => new(a.m_value - b);
    public static Delisle operator /(Delisle a, decimal b) => new(a.m_value / b);
    public static Delisle operator *(Delisle a, decimal b) => new(a.m_value * b);

    #region SIMPLE_OP

    public static Delisle operator +(Delisle a, Celsius b) => new(a.ToKelvin().m_value + b.ToKelvin().m_value);
    public static Delisle operator -(Delisle a, Celsius b) => new(a.ToKelvin().m_value - b.ToKelvin().m_value);
    public static Delisle operator /(Delisle a, Celsius b) => new(a.ToKelvin().m_value / b.ToKelvin().m_value);
    public static Delisle operator *(Delisle a, Celsius b) => new(a.ToKelvin().m_value * b.ToKelvin().m_value);

    public static Delisle operator +(Delisle a, Fahrenheit b) =>
        new(a.ToKelvin().m_value + b.ToKelvin().m_value);

    public static Delisle operator -(Delisle a, Fahrenheit b) =>
        new(a.ToKelvin().m_value - b.ToKelvin().m_value);

    public static Delisle operator /(Delisle a, Fahrenheit b) =>
        new(a.ToKelvin().m_value / b.ToKelvin().m_value);

    public static Delisle operator *(Delisle a, Fahrenheit b) =>
        new(a.ToKelvin().m_value * b.ToKelvin().m_value);

    public static Delisle operator +(Delisle a, Kelvin b) => new(a.ToKelvin().m_value + b.m_value);
    public static Delisle operator -(Delisle a, Kelvin b) => new(a.ToKelvin().m_value - b.m_value);
    public static Delisle operator /(Delisle a, Kelvin b) => new(a.ToKelvin().m_value / b.m_value);
    public static Delisle operator *(Delisle a, Kelvin b) => new(a.ToKelvin().m_value * b.m_value);
    public static Delisle operator +(Delisle a, Newton b) => new(a.ToKelvin().m_value + b.ToKelvin().m_value);
    public static Delisle operator -(Delisle a, Newton b) => new(a.ToKelvin().m_value - b.ToKelvin().m_value);
    public static Delisle operator /(Delisle a, Newton b) => new(a.ToKelvin().m_value / b.ToKelvin().m_value);
    public static Delisle operator *(Delisle a, Newton b) => new(a.ToKelvin().m_value * b.ToKelvin().m_value);
    public static Delisle operator +(Delisle a, Rankine b) => new(a.ToKelvin().m_value + b.ToKelvin().m_value);
    public static Delisle operator -(Delisle a, Rankine b) => new(a.ToKelvin().m_value - b.ToKelvin().m_value);
    public static Delisle operator /(Delisle a, Rankine b) => new(a.ToKelvin().m_value / b.ToKelvin().m_value);
    public static Delisle operator *(Delisle a, Rankine b) => new(a.ToKelvin().m_value * b.ToKelvin().m_value);
    public static Delisle operator +(Delisle a, Reaumur b) => new(a.ToKelvin().m_value + b.ToKelvin().m_value);
    public static Delisle operator -(Delisle a, Reaumur b) => new(a.ToKelvin().m_value - b.ToKelvin().m_value);
    public static Delisle operator /(Delisle a, Reaumur b) => new(a.ToKelvin().m_value / b.ToKelvin().m_value);
    public static Delisle operator *(Delisle a, Reaumur b) => new(a.ToKelvin().m_value * b.ToKelvin().m_value);
    public static Delisle operator +(Delisle a, Romer b) => new(a.ToKelvin().m_value + b.ToKelvin().m_value);
    public static Delisle operator -(Delisle a, Romer b) => new(a.ToKelvin().m_value - b.ToKelvin().m_value);
    public static Delisle operator /(Delisle a, Romer b) => new(a.ToKelvin().m_value / b.ToKelvin().m_value);
    public static Delisle operator *(Delisle a, Romer b) => new(a.ToKelvin().m_value * b.ToKelvin().m_value);

    public static Delisle operator +(Delisle value) => value;
    public static Delisle operator -(Delisle value) => value;
    public static Delisle operator --(Delisle value) => new(value.m_value - 1);
    public static Delisle operator ++(Delisle value) => new(value.m_value + 1);

    public static bool operator ==(Delisle left, Delisle right) => left.m_value == right.m_value;
    public static bool operator !=(Delisle left, Delisle right) => left.m_value != right.m_value;
    public static bool operator ==(Delisle left, decimal right)=> left.m_value == right;
    public static bool operator !=(Delisle left, decimal right)=> left.m_value != right; 
    
    #endregion

    public static implicit operator Delisle(decimal b) => new(b);
    public static implicit operator Delisle(Fahrenheit b) => new((b.m_value - 32) * 0.83333m - 100.00m);
    public static implicit operator Delisle(Kelvin b) => new((b.m_value - 273.15m) * 1.5000m - 100.00m);
    public static implicit operator Delisle(Celsius b) => new(b.m_value * 1.5000m - 100.00m);
    public static implicit operator Delisle(Newton b) => new((b.m_value * 4.5455m) - 100.00m);
    public static implicit operator Delisle(Rankine b) => new((b.m_value - 491.67m) * 0.83333m - 100.00m);
    public static implicit operator Delisle(Reaumur b) => new(b.m_value * 1.8750m - 100.00m);
    public static implicit operator Delisle(Romer b) => new((b.m_value - 7.5m) * 2.8571m - 100.00m);


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

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format,
        IFormatProvider? provider)
        => m_value.TryFormat(destination, out charsWritten, format, provider);


    public static Delisle Parse(string s, IFormatProvider? provider)
        => decimal.Parse(s, provider);

    public static bool TryParse(string? s, IFormatProvider? provider, out Delisle result)
    {
        var rs = decimal.TryParse(s, provider, out var f);
        result = new Delisle(f);
        return rs;
    }

    public static Delisle Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        => decimal.Parse(s, provider);

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Delisle result)
    {
        var rs = decimal.TryParse(s, provider, out var f);
        result = new Delisle(f);
        return rs;
    }
    
    public Kelvin ToKelvin() => new(((m_value + 100) / 2) + 273.15m);
    public static Kelvin ToKelvin(Delisle i) => i;
    public bool Equals(Delisle other) => other.m_value.Equals(m_value);
}