namespace Yannick.Lang.Attribute
{
    /// <summary>
    /// Represents an attribute that can be applied to fields, properties, classes, or structs to specify a suffix.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class |
                    AttributeTargets.Struct)]
    public sealed class SuffixAttribute : System.Attribute, IEquatable<SuffixAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuffixAttribute"/> class with default values.
        /// </summary>
        internal SuffixAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SuffixAttribute"/> class with a specified string suffix.
        /// </summary>
        /// <param name="suffix">The string suffix.</param>
        public SuffixAttribute(string suffix)
        {
            Suffix = suffix;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SuffixAttribute"/> class with a specified raw suffix object.
        /// </summary>
        /// <param name="suffixRaw">The raw suffix object.</param>
        public SuffixAttribute(object suffixRaw)
        {
            SuffixRaw = suffixRaw;
        }

        /// <summary>
        /// Gets the suffix as a string.
        /// </summary>
        public string Suffix { get; init; } = string.Empty;

        /// <summary>
        /// Gets the raw suffix object.
        /// </summary>
        public object? SuffixRaw { get; init; }

        /// <summary>
        /// Determines whether this instance and another specified <see cref="SuffixAttribute"/> object have the same value.
        /// </summary>
        /// <param name="other">The <see cref="SuffixAttribute"/> to compare to this instance.</param>
        /// <returns><c>true</c> if the value of the <paramref name="other"/> parameter is the same as this instance; otherwise, <c>false</c>.</returns>
        public bool Equals(SuffixAttribute? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Suffix == other.Suffix && Equals(SuffixRaw, other.SuffixRaw);
        }

        /// <summary>
        /// Returns a string representation of the suffix.
        /// </summary>
        /// <returns>A string representation of the suffix.</returns>
        public override string ToString() => SuffixRaw?.ToString() ?? Suffix;

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be a <see cref="SuffixAttribute"/> object, have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is a <see cref="SuffixAttribute"/> and its value is the same as this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
            => ReferenceEquals(this, obj) || obj is SuffixAttribute other && Equals(other);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode() => HashCode.Combine(Suffix, SuffixRaw);
    }
}