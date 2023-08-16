namespace Yannick.Lang.Attribute;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class SuffixAttribute : System.Attribute, IEquatable<SuffixAttribute>
{
    internal SuffixAttribute()
    {
    }

    public SuffixAttribute(string suffix)
    {
        Suffix = suffix;
    }

    public SuffixAttribute(object suffixRaw)
    {
        SuffixRaw = suffixRaw;
    }

    public string Suffix { get; init; } = string.Empty;
    public object? SuffixRaw { get; init; } = null;


    public bool Equals(SuffixAttribute? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return base.Equals(other) && Suffix == other.Suffix && Equals(SuffixRaw, other.SuffixRaw);
    }

    public override string ToString() => SuffixRaw?.ToString() ?? Suffix;

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is SuffixAttribute other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Suffix, SuffixRaw);
    }
}