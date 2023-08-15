using System.Globalization;
using System.Runtime.InteropServices;

namespace Yannick.Physic.SI.Temperature;

/// <summary>
/// Represents a temperature value in Kelvin.
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Kelvin : ITemperature<Kelvin>
{
    public override bool Equals(object? obj)
    {
        return obj is Kelvin other && Equals(other);
    }

    public override int GetHashCode()
    {
        return m_value.GetHashCode();
    }

    internal readonly decimal m_value;


    public Kelvin(decimal mValue)
    {
        m_value = mValue;
        if (mValue < 0)
            mValue = 0;
    }

    public static implicit operator decimal(Kelvin b) => b.m_value;
    public static implicit operator Celsius(Kelvin b) => new(b.m_value - 273.15m);
    public static implicit operator Fahrenheit(Kelvin b) => new(((b.m_value - 273.15m) * 1.8000m) + 32.00m);
    public static implicit operator Delisle(Kelvin b) => new(((b.m_value - 273.15m) * 1.5000m) - 100.00m);
    public static implicit operator Newton(Kelvin b) => new((b.m_value - 273.15m) * 0.33000m);
    public static implicit operator Rankine(Kelvin b) => new(((b.m_value - 273.15m) * 1.8000m) + 491.67m);
    public static implicit operator Reaumur(Kelvin b) => new((b.m_value - 273.15m) * 0.80000m);
    public static implicit operator Romer(Kelvin b) => new(((b.m_value - 273.15m) * 0.52500m) + 7.50m);

    public static implicit operator Kelvin(decimal b) => new(b);
    public static implicit operator Kelvin(Delisle b) => b.ToKelvin();
    public static implicit operator Kelvin(Newton b) => b.ToKelvin();
    public static implicit operator Kelvin(Celsius b) => b.ToKelvin();
    public static implicit operator Kelvin(Fahrenheit b) => b.ToKelvin();
    public static implicit operator Kelvin(Rankine b) => b.ToKelvin();
    public static implicit operator Kelvin(Reaumur b) => b.ToKelvin();
    public static implicit operator Kelvin(Romer b) => b.ToKelvin();

    #region OPERATOREN

    public static Kelvin operator +(Kelvin a, Kelvin b) => new(a.m_value + b.m_value);
    public static Kelvin operator -(Kelvin a, Kelvin b) => new(a.m_value - b.m_value);
    public static Kelvin operator /(Kelvin a, Kelvin b) => new(a.m_value / b.m_value);
    public static Kelvin operator *(Kelvin a, Kelvin b) => new(a.m_value * b.m_value);
    public static Kelvin operator +(Kelvin a, decimal b) => new(a.m_value + b);
    public static Kelvin operator -(Kelvin a, decimal b) => new(a.m_value - b);
    public static Kelvin operator /(Kelvin a, decimal b) => new(a.m_value / b);
    public static Kelvin operator *(Kelvin a, decimal b) => new(a.m_value * b);
    public static Kelvin operator +(decimal b, Kelvin a) => new(a.m_value + b);
    public static Kelvin operator -(decimal b, Kelvin a) => new(a.m_value - b);
    public static Kelvin operator /(decimal b, Kelvin a) => new(a.m_value / b);
    public static Kelvin operator *(decimal b, Kelvin a) => new(a.m_value * b);
    public static bool operator ==(Kelvin a, Kelvin b) => a.m_value == b.m_value;
    public static bool operator !=(Kelvin a, Kelvin b) => a.m_value != b.m_value;
    public static bool operator ==(Kelvin a, decimal b) => a.m_value == b;
    public static bool operator !=(Kelvin a, decimal b) => a.m_value != b;
    public static bool operator ==(decimal a, Kelvin b) => a == b.m_value;
    public static bool operator !=(decimal a, Kelvin b) => a != b.m_value;
    public static bool operator >=(Kelvin a, Kelvin b) => a.m_value >= b.m_value;
    public static bool operator <=(Kelvin a, Kelvin b) => a.m_value <= b.m_value;
    public static bool operator >=(Kelvin a, decimal b) => a.m_value >= b;
    public static bool operator <=(Kelvin a, decimal b) => a.m_value <= b;
    public static bool operator >=(decimal a, Kelvin b) => a >= b.m_value;
    public static bool operator <=(decimal a, Kelvin b) => a <= b.m_value;
    public static bool operator >(Kelvin a, Kelvin b) => a.m_value > b.m_value;
    public static bool operator <(Kelvin a, Kelvin b) => a.m_value < b.m_value;
    public static bool operator >(Kelvin a, decimal b) => a.m_value > b;
    public static bool operator <(Kelvin a, decimal b) => a.m_value < b;
    public static bool operator >(decimal a, Kelvin b) => a > b.m_value;
    public static bool operator <(decimal a, Kelvin b) => a < b.m_value;

    public static Kelvin operator +(Kelvin value) => value;
    public static Kelvin operator -(Kelvin value) => value;
    public static Kelvin operator --(Kelvin value) => new(value.m_value - 1);
    public static Kelvin operator ++(Kelvin value) => new(value.m_value + 1);

    #endregion

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

    public bool Equals(Kelvin other) => m_value.Equals(other.m_value);
    
    /// <summary>Returns the fully qualified type name of this instance.</summary>
    /// <returns>The fully qualified type name.</returns>
    public override string ToString() => m_value.ToString(CultureInfo.InvariantCulture);

    public string ToString(string? format, IFormatProvider? formatProvider)
        => m_value.ToString(format, formatProvider);

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format,
        IFormatProvider? provider)
        => m_value.TryFormat(destination, out charsWritten, format, provider);


    public static Kelvin Parse(string s, IFormatProvider? provider)
        => decimal.Parse(s, provider);

    public static bool TryParse(string? s, IFormatProvider? provider, out Kelvin result)
    {
        var rs = decimal.TryParse(s, provider, out var f);
        result = new Kelvin(f);
        return rs;
    }

    public static Kelvin Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        => decimal.Parse(s, provider);

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Kelvin result)
    {
        var rs = decimal.TryParse(s, provider, out var f);
        result = new Kelvin(f);
        return rs;
    }
    
    public Kelvin ToKelvin() => this;
    public static Kelvin ToKelvin(Kelvin i) => i;
}