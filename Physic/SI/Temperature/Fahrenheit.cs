using System.Runtime.InteropServices;

namespace Yannick.Physic.SI.Temperature;

/// <summary>
/// Represents a temperature value in Fahrenheit.
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Fahrenheit : ITemperature<Fahrenheit>
{
    public override bool Equals(object? obj)
    {
        return obj is Fahrenheit other && Equals(other);
    }

    public override int GetHashCode()
    {
        return m_value.GetHashCode();
    }

    internal readonly decimal m_value; // Do not rename (binary serialization)

    public Fahrenheit(decimal mValue)
    {
        m_value = mValue < 0 ? 0 : mValue;
    }

    public Fahrenheit(Kelvin kelvin)
    {
        m_value = kelvin.m_value - -273.15m;
    }

    public static Fahrenheit operator +(Fahrenheit a, Fahrenheit b) => new(a.m_value + b.m_value);
    public static Fahrenheit operator -(Fahrenheit a, Fahrenheit b) => new(a.m_value - b.m_value);
    public static Fahrenheit operator /(Fahrenheit a, Fahrenheit b) => new(a.m_value / b.m_value);
    public static Fahrenheit operator *(Fahrenheit a, Fahrenheit b) => new(a.m_value * b.m_value);
    public static Fahrenheit operator +(Fahrenheit a, decimal b) => new(a.m_value + b);
    public static Fahrenheit operator -(Fahrenheit a, decimal b) => new(a.m_value - b);
    public static Fahrenheit operator /(Fahrenheit a, decimal b) => new(a.m_value / b);
    public static Fahrenheit operator *(Fahrenheit a, decimal b) => new(a.m_value * b);

    #region SIMPLE_OP

    public static Fahrenheit operator +(Fahrenheit a, Celsius b) => a.ToKelvin().m_value + b.ToKelvin().m_value;
    public static Fahrenheit operator -(Fahrenheit a, Celsius b) => a.ToKelvin().m_value - b.ToKelvin().m_value;
    public static Fahrenheit operator /(Fahrenheit a, Celsius b) => a.ToKelvin().m_value / b.ToKelvin().m_value;
    public static Fahrenheit operator *(Fahrenheit a, Celsius b) => a.ToKelvin().m_value * b.ToKelvin().m_value;
    public static Fahrenheit operator +(Fahrenheit a, Delisle b) => a.ToKelvin().m_value + b.ToKelvin().m_value;
    public static Fahrenheit operator -(Fahrenheit a, Delisle b) => a.ToKelvin().m_value - b.ToKelvin().m_value;
    public static Fahrenheit operator /(Fahrenheit a, Delisle b) => a.ToKelvin().m_value / b.ToKelvin().m_value;
    public static Fahrenheit operator *(Fahrenheit a, Delisle b) => a.ToKelvin().m_value * b.ToKelvin().m_value;
    public static Fahrenheit operator +(Fahrenheit a, Kelvin b) => a.ToKelvin().m_value + b.m_value;
    public static Fahrenheit operator -(Fahrenheit a, Kelvin b) => a.ToKelvin().m_value - b.m_value;
    public static Fahrenheit operator /(Fahrenheit a, Kelvin b) => a.ToKelvin().m_value / b.m_value;
    public static Fahrenheit operator *(Fahrenheit a, Kelvin b) => a.ToKelvin().m_value * b.m_value;
    public static Fahrenheit operator +(Fahrenheit a, Newton b) => a.ToKelvin().m_value + b.ToKelvin().m_value;
    public static Fahrenheit operator -(Fahrenheit a, Newton b) => a.ToKelvin().m_value - b.ToKelvin().m_value;
    public static Fahrenheit operator /(Fahrenheit a, Newton b) => a.ToKelvin().m_value / b.ToKelvin().m_value;
    public static Fahrenheit operator *(Fahrenheit a, Newton b) => a.ToKelvin().m_value * b.ToKelvin().m_value;
    public static Fahrenheit operator +(Fahrenheit a, Rankine b) => a.ToKelvin().m_value + b.ToKelvin().m_value;
    public static Fahrenheit operator -(Fahrenheit a, Rankine b) => a.ToKelvin().m_value - b.ToKelvin().m_value;
    public static Fahrenheit operator /(Fahrenheit a, Rankine b) => a.ToKelvin().m_value / b.ToKelvin().m_value;
    public static Fahrenheit operator *(Fahrenheit a, Rankine b) => a.ToKelvin().m_value * b.ToKelvin().m_value;
    public static Fahrenheit operator +(Fahrenheit a, Reaumur b) => a.ToKelvin().m_value + b.ToKelvin().m_value;
    public static Fahrenheit operator -(Fahrenheit a, Reaumur b) => a.ToKelvin().m_value - b.ToKelvin().m_value;
    public static Fahrenheit operator /(Fahrenheit a, Reaumur b) => a.ToKelvin().m_value / b.ToKelvin().m_value;
    public static Fahrenheit operator *(Fahrenheit a, Reaumur b) => a.ToKelvin().m_value * b.ToKelvin().m_value;
    public static Fahrenheit operator +(Fahrenheit a, Romer b) => a.ToKelvin().m_value + b.ToKelvin().m_value;
    public static Fahrenheit operator -(Fahrenheit a, Romer b) => a.ToKelvin().m_value - b.ToKelvin().m_value;
    public static Fahrenheit operator /(Fahrenheit a, Romer b) => a.ToKelvin().m_value / b.ToKelvin().m_value;
    public static Fahrenheit operator *(Fahrenheit a, Romer b) => a.ToKelvin().m_value * b.ToKelvin().m_value;

    public static Fahrenheit operator +(Fahrenheit value) => value;
    public static Fahrenheit operator -(Fahrenheit value) => value;
    public static Fahrenheit operator --(Fahrenheit value) => new(value.m_value - 1);
    public static Fahrenheit operator ++(Fahrenheit value) => new(value.m_value + 1);

    public static bool operator ==(Fahrenheit left, Fahrenheit right) => left.m_value == right.m_value;
    public static bool operator !=(Fahrenheit left, Fahrenheit right) => left.m_value != right.m_value;
    public static bool operator ==(Fahrenheit left, decimal right)=> left.m_value == right;
    public static bool operator !=(Fahrenheit left, decimal right)=> left.m_value != right; 
    
    #endregion

    public static implicit operator Fahrenheit(decimal b) => new(b);
    public static implicit operator Fahrenheit(Delisle b) => new(((decimal)b.m_value + 100) * 1.2000m + 32.00m);
    public static implicit operator Fahrenheit(Kelvin b) => new(((decimal)b.m_value - 273.15m) * 1.8000m + 32.00m);
    public static implicit operator Fahrenheit(Celsius b) => new((decimal)b.m_value * 1.8000m + 32.00m);
    public static implicit operator Fahrenheit(Newton b) => new((decimal)b.m_value * 5.4545m + 32.00m);
    public static implicit operator Fahrenheit(Rankine b) => new(((decimal)b.m_value - 491.67m) + 32.00m);
    public static implicit operator Fahrenheit(Reaumur b) => new((decimal)b.m_value * 2.2500m + 32.00m);
    public static implicit operator Fahrenheit(Romer b) => new(((decimal)b.m_value - 7.5m) * 3.4286m + 32.00m);
    
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


    public static Fahrenheit Parse(string s, IFormatProvider? provider)
        => decimal.Parse(s, provider);

    public static bool TryParse(string? s, IFormatProvider? provider, out Fahrenheit result)
    {
        var rs = decimal.TryParse(s, provider, out var f);
        result = new Fahrenheit(f);
        return rs;
    }

    public static Fahrenheit Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        => decimal.Parse(s, provider);

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Fahrenheit result)
    {
        var rs = decimal.TryParse(s, provider, out var f);
        result = new Fahrenheit(f);
        return rs;
    }
    
    public Kelvin ToKelvin() => new(((m_value - 32) / 1.8000m) + 273.15m);
    public static Kelvin ToKelvin(Fahrenheit i) => i;
    public bool Equals(Fahrenheit other) => other.m_value.Equals(m_value);
}