using System.Runtime.InteropServices;

namespace Yannick.Physic.SI.Temperature;

/// <summary>
/// Represents a temperature value in Newton.
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Newton  : ITemperature<Newton>
{
    public override bool Equals(object? obj)
    {
        return obj is Newton other && Equals(other);
    }

    public override int GetHashCode()
    {
        return m_value.GetHashCode();
    }

    internal readonly decimal m_value; // Do not rename (binary serialization)

    public Newton(decimal mValue)
    {
        m_value = mValue < 0 ? 0 : mValue;
    }


    public static Newton operator +(Newton a, Newton b) => new(a.m_value + b.m_value);
    public static Newton operator -(Newton a, Newton b) => new(a.m_value - b.m_value);
    public static Newton operator /(Newton a, Newton b) => new(a.m_value / b.m_value);
    public static Newton operator *(Newton a, Newton b) => new(a.m_value * b.m_value);
    public static Newton operator +(Newton a, decimal b) => new(a.m_value + b);
    public static Newton operator -(Newton a, decimal b) => new(a.m_value - b);
    public static Newton operator /(Newton a, decimal b) => new(a.m_value / b);
    public static Newton operator *(Newton a, decimal b) => new(a.m_value * b);

    #region SIMPLE_OP

    public static Newton operator +(Newton a, Celsius b) => a.ToKelvin().m_value + b.ToKelvin().m_value;
    public static Newton operator -(Newton a, Celsius b) => a.ToKelvin().m_value - b.ToKelvin().m_value;
    public static Newton operator /(Newton a, Celsius b) => a.ToKelvin().m_value / b.ToKelvin().m_value;
    public static Newton operator *(Newton a, Celsius b) => a.ToKelvin().m_value * b.ToKelvin().m_value;
    public static Newton operator +(Newton a, Delisle b) => a.ToKelvin().m_value + b.ToKelvin().m_value;
    public static Newton operator -(Newton a, Delisle b) => a.ToKelvin().m_value - b.ToKelvin().m_value;
    public static Newton operator /(Newton a, Delisle b) => a.ToKelvin().m_value / b.ToKelvin().m_value;
    public static Newton operator *(Newton a, Delisle b) => a.ToKelvin().m_value * b.ToKelvin().m_value;
    public static Newton operator +(Newton a, Kelvin b) => a.ToKelvin().m_value + b.m_value;
    public static Newton operator -(Newton a, Kelvin b) => a.ToKelvin().m_value - b.m_value;
    public static Newton operator /(Newton a, Kelvin b) => a.ToKelvin().m_value / b.m_value;
    public static Newton operator *(Newton a, Kelvin b) => a.ToKelvin().m_value * b.m_value;
    public static Newton operator +(Newton a, Fahrenheit b) => a.ToKelvin().m_value + b.ToKelvin().m_value;
    public static Newton operator -(Newton a, Fahrenheit b) => a.ToKelvin().m_value - b.ToKelvin().m_value;
    public static Newton operator /(Newton a, Fahrenheit b) => a.ToKelvin().m_value / b.ToKelvin().m_value;
    public static Newton operator *(Newton a, Fahrenheit b) => a.ToKelvin().m_value * b.ToKelvin().m_value;
    public static Newton operator +(Newton a, Rankine b) => a.ToKelvin().m_value + b.ToKelvin().m_value;
    public static Newton operator -(Newton a, Rankine b) => a.ToKelvin().m_value - b.ToKelvin().m_value;
    public static Newton operator /(Newton a, Rankine b) => a.ToKelvin().m_value / b.ToKelvin().m_value;
    public static Newton operator *(Newton a, Rankine b) => a.ToKelvin().m_value * b.ToKelvin().m_value;
    public static Newton operator +(Newton a, Reaumur b) => a.ToKelvin().m_value + b.ToKelvin().m_value;
    public static Newton operator -(Newton a, Reaumur b) => a.ToKelvin().m_value - b.ToKelvin().m_value;
    public static Newton operator /(Newton a, Reaumur b) => a.ToKelvin().m_value / b.ToKelvin().m_value;
    public static Newton operator *(Newton a, Reaumur b) => a.ToKelvin().m_value * b.ToKelvin().m_value;
    public static Newton operator +(Newton a, Romer b) => a.ToKelvin().m_value + b.ToKelvin().m_value;
    public static Newton operator -(Newton a, Romer b) => a.ToKelvin().m_value - b.ToKelvin().m_value;
    public static Newton operator /(Newton a, Romer b) => a.ToKelvin().m_value / b.ToKelvin().m_value;
    public static Newton operator *(Newton a, Romer b) => a.ToKelvin().m_value * b.ToKelvin().m_value;
    
    public static Newton operator +(Newton value) => value;
    public static Newton operator -(Newton value) => value;
    public static Newton operator --(Newton value) => new(value.m_value - 1);
    public static Newton operator ++(Newton value) => new(value.m_value + 1);

    public static bool operator ==(Newton left, Newton right) => left.m_value == right.m_value;
    public static bool operator !=(Newton left, Newton right) => left.m_value != right.m_value;
    public static bool operator ==(Newton left, decimal right)=> left.m_value == right;
    public static bool operator !=(Newton left, decimal right)=> left.m_value != right;

    #endregion

    public static implicit operator Newton(decimal b) => new(b);
    public static implicit operator Newton(Delisle b) => new((b.m_value + 100) * 0.22000m);
    public static implicit operator Newton(Kelvin b) => new((b.m_value - 273.15m) * 0.33000m);
    public static implicit operator Newton(Celsius b) => new(b.m_value * 0.33000m);
    public static implicit operator Newton(Fahrenheit b) => new((b.m_value - 32) * 0.18333m);
    public static implicit operator Newton(Rankine b) => new((b.m_value - 491.67m) * 0.18333m);
    public static implicit operator Newton(Reaumur b) => new(b.m_value * 0.41250m);
    public static implicit operator Newton(Romer b) => new((b.m_value - 7.5m) * 0.62857m);


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


    public static Newton Parse(string s, IFormatProvider? provider)
        => decimal.Parse(s, provider);

    public static bool TryParse(string? s, IFormatProvider? provider, out Newton result)
    {
        var rs = decimal.TryParse(s, provider, out var f);
        result = new Newton(f);
        return rs;
    }

    public static Newton Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        => decimal.Parse(s, provider);

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Newton result)
    {
        var rs = decimal.TryParse(s, provider, out var f);
        result = new Newton(f);
        return rs;
    }
    
    public Kelvin ToKelvin() => new((m_value / 0.33000m) + 273.15m);
    public static Kelvin ToKelvin(Newton i) => i;
    public bool Equals(Newton other) => other.m_value.Equals(m_value);
}