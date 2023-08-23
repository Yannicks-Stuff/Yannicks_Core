namespace Yannick.Chemistry.Mathematic;

public readonly struct Dalton : IEquatable<Dalton>
{
    private readonly decimal _value;

    public Dalton(decimal value)
    {
        _value = value;
    }

    public static Dalton operator +(Dalton a, decimal v) => new(a._value + v);
    public static Dalton operator -(Dalton a, decimal v) => new(a._value - v);
    public static Dalton operator *(Dalton a, decimal v) => new(a._value * v);
    public static Dalton operator /(Dalton a, decimal v) => new(a._value / v);
    public static Dalton operator %(Dalton a, decimal v) => new(a._value % v);

    public static bool operator ==(Dalton a, Dalton b) => a._value == b._value;
    public static bool operator !=(Dalton a, Dalton b) => a._value != b._value;
    public static bool operator >(Dalton a, Dalton b) => a._value > b._value;
    public static bool operator <(Dalton a, Dalton b) => a._value < b._value;

    public static bool operator ==(Dalton a, decimal b) => a._value == b;
    public static bool operator !=(Dalton a, decimal b) => a._value != b;
    public static bool operator >(Dalton a, decimal b) => a._value > b;
    public static bool operator <(Dalton a, decimal b) => a._value < b;

    public static implicit operator decimal(Dalton d) => d._value;
    public static implicit operator Dalton(decimal d) => new(d);

    public bool Equals(Dalton other)
    {
        return _value == other._value;
    }

    public override bool Equals(object? obj)
    {
        return obj is Dalton other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }
}