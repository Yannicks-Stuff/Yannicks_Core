using System.Runtime.InteropServices;

namespace Yannick.Physic.SI.Temperature;

/// <summary>
/// Represents a temperature value in Celsius.
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Celsius : ITemperature<Celsius>
{
    public override bool Equals(object? obj)
    {
        return obj is Celsius other && Equals(other);
    }

    public override int GetHashCode()
    {
        return m_value.GetHashCode();
    }

    internal readonly decimal m_value; // Do not rename (binary serialization)

    public Celsius(decimal mValue)
    {
        m_value = mValue;
    }

   #region SIMPLE_OP
   
   public static Celsius operator +(Celsius a, Celsius b) => new(a.m_value + b.m_value);
   public static Celsius operator -(Celsius a, Celsius b) => new(a.m_value - b.m_value);
   public static Celsius operator /(Celsius a, Celsius b) => new(a.m_value / b.m_value);
   public static Celsius operator *(Celsius a, Celsius b) => new(a.m_value * b.m_value);
   public static Celsius operator +(Celsius a, decimal b) => new(a.m_value + b);
   public static Celsius operator -(Celsius a, decimal b) => new(a.m_value - b);
   public static Celsius operator /(Celsius a, decimal b) => new(a.m_value / b);
   public static Celsius operator *(Celsius a, decimal b) => new(a.m_value * b);

    public static Celsius operator +(Celsius a, Delisle b) => new(a.ToKelvin().m_value + b.ToKelvin().m_value);
    public static Celsius operator -(Celsius a, Delisle b) => new(a.ToKelvin().m_value - b.ToKelvin().m_value);
    public static Celsius operator /(Celsius a, Delisle b) => new(a.ToKelvin().m_value / b.ToKelvin().m_value);
    public static Celsius operator *(Celsius a, Delisle b) => new(a.ToKelvin().m_value * b.ToKelvin().m_value);

    public static Celsius operator +(Celsius a, Fahrenheit b) =>
        new(a.ToKelvin().m_value + b.ToKelvin().m_value);

    public static Celsius operator -(Celsius a, Fahrenheit b) =>
        new(a.ToKelvin().m_value - b.ToKelvin().m_value);

    public static Celsius operator /(Celsius a, Fahrenheit b) =>
        new(a.ToKelvin().m_value / b.ToKelvin().m_value);

    public static Celsius operator *(Celsius a, Fahrenheit b) =>
        new(a.ToKelvin().m_value * b.ToKelvin().m_value);

    public static Celsius operator +(Celsius a, Kelvin b) => new(a.ToKelvin().m_value + b.m_value);
    public static Celsius operator -(Celsius a, Kelvin b) => new(a.ToKelvin().m_value - b.m_value);
    public static Celsius operator /(Celsius a, Kelvin b) => new(a.ToKelvin().m_value / b.m_value);
    public static Celsius operator *(Celsius a, Kelvin b) => new(a.ToKelvin().m_value * b.m_value);
    public static Celsius operator +(Celsius a, Newton b) => new(a.ToKelvin().m_value + b.ToKelvin().m_value);
    public static Celsius operator -(Celsius a, Newton b) => new(a.ToKelvin().m_value - b.ToKelvin().m_value);
    public static Celsius operator /(Celsius a, Newton b) => new(a.ToKelvin().m_value / b.ToKelvin().m_value);
    public static Celsius operator *(Celsius a, Newton b) => new(a.ToKelvin().m_value * b.ToKelvin().m_value);
    public static Celsius operator +(Celsius a, Rankine b) => new(a.ToKelvin().m_value + b.ToKelvin().m_value);
    public static Celsius operator -(Celsius a, Rankine b) => new(a.ToKelvin().m_value - b.ToKelvin().m_value);
    public static Celsius operator /(Celsius a, Rankine b) => new(a.ToKelvin().m_value / b.ToKelvin().m_value);
    public static Celsius operator *(Celsius a, Rankine b) => new(a.ToKelvin().m_value * b.ToKelvin().m_value);
    public static Celsius operator +(Celsius a, Reaumur b) => new(a.ToKelvin().m_value + b.ToKelvin().m_value);
    public static Celsius operator -(Celsius a, Reaumur b) => new(a.ToKelvin().m_value - b.ToKelvin().m_value);
    public static Celsius operator /(Celsius a, Reaumur b) => new(a.ToKelvin().m_value / b.ToKelvin().m_value);
    public static Celsius operator *(Celsius a, Reaumur b) => new(a.ToKelvin().m_value * b.ToKelvin().m_value);
    public static Celsius operator +(Celsius a, Romer b) => new(a.ToKelvin().m_value + b.ToKelvin().m_value);
    public static Celsius operator -(Celsius a, Romer b) => new(a.ToKelvin().m_value - b.ToKelvin().m_value);
    public static Celsius operator /(Celsius a, Romer b) => new(a.ToKelvin().m_value / b.ToKelvin().m_value);
    public static Celsius operator *(Celsius a, Romer b) => new(a.ToKelvin().m_value * b.ToKelvin().m_value);

    public static Celsius operator +(Celsius value) => value;
    public static Celsius operator -(Celsius value) => value;
    public static Celsius operator --(Celsius value) => new(value.m_value - 1);
    public static Celsius operator ++(Celsius value) => new(value.m_value + 1);

    public static bool operator ==(Celsius left, Celsius right) => left.m_value == right.m_value;
    public static bool operator !=(Celsius left, Celsius right) => left.m_value != right.m_value;
    public static bool operator ==(Celsius left, decimal right)=> left.m_value == right;
    public static bool operator !=(Celsius left, decimal right)=> left.m_value != right;
    
    #endregion

    public static implicit operator Celsius(decimal b) => new(b);
    public static implicit operator Celsius(Fahrenheit b) => new(((b.m_value - 32) / 1.8000m));
    public static implicit operator Celsius(Kelvin b) => new((b.m_value - 273.15m));
    public static implicit operator Celsius(Delisle b) => new(((b.m_value + 100) / 1.5000m));
    public static implicit operator Celsius(Newton b) => new(b.m_value / 0.33000m);
    public static implicit operator Celsius(Rankine b) => new((b.m_value - 491.67m) / 1.8000m);
    public static implicit operator Celsius(Reaumur b) => new(b.m_value / 0.80000m);
    public static implicit operator Celsius(Romer b) => new((b.m_value - 7.5m) / 0.52500m);

    public static implicit operator decimal(Celsius c) => c.m_value;
    

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


    public static Celsius Parse(string s, IFormatProvider? provider)
        => decimal.Parse(s, provider);

    public static bool TryParse(string? s, IFormatProvider? provider, out Celsius result)
    {
        var rs = decimal.TryParse(s, provider, out var f);
        result = new Celsius(f);
        return rs;
    }

    public static Celsius Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        => decimal.Parse(s, provider);

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Celsius result)
    {
        var rs = decimal.TryParse(s, provider, out var f);
        result = new Celsius(f);
        return rs;
    }
    
    public Kelvin ToKelvin() => new(m_value + 273.15m);
    public static Kelvin ToKelvin(Celsius i) => i;
    public bool Equals(Celsius other) => other.m_value.Equals(m_value);

}