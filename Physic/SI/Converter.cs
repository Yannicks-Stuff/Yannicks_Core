using Yannick.Extensions.DecimalExtensions;

namespace Yannick.Physic.SI;

using Unit = Converter;

/// <summary>
/// Provides methods for converting between different SI prefixes.
/// </summary>
public static class Converter
{
    /// <summary>
    /// Converts a value with the specified prefix to the corresponding base value (e.g., converting kilometers to meters).
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="prefix">The prefix of the input value (e.g., Kilo for kilometers).</param>
    /// <returns>The base value corresponding to the input value and prefix.</returns>
    public static decimal BaseValue(decimal value, Prefix prefix)
        => value * 10m.Pow((int)prefix);

    /// <summary>
    /// Converts a value with the specified prefix to a value with the target prefix (e.g., converting meters to centimeters).
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="current">The current prefix for the conversion</param>
    /// <param name="target">The target prefix for the conversion.</param>
    /// <returns>The value converted to the target prefix.</returns>
    public static decimal Convert(decimal value, Prefix current, Prefix target)
        => BaseValue(value, current) / 10m.Pow((int)target);
}