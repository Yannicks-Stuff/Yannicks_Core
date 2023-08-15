namespace Yannick.Extensions.DecimalExtensions;

/// <summary>
/// Include extensions for the class <see cref="decimal"/>
/// </summary>
public static class DecimalExtension
{
    /// <summary>
    /// Raises the decimal base value to the specified exponent power.
    /// </summary>
    /// <param name="baseValue">The base value of the power operation.</param>
    /// <param name="exponent">The exponent value of the power operation.</param>
    /// <returns>The result of the power operation.</returns>
    public static decimal Pow(this decimal baseValue, int exponent)
    {
        if (exponent < 0)
        {
            if (baseValue == 0) throw new ArgumentException("Base value cannot be zero when exponent is negative.");
            return 1.0m / Pow(baseValue, -exponent);
        }

        var result = 1.0m;
        while (exponent > 0)
        {
            if ((exponent & 1) == 1)
            {
                result *= baseValue;
            }

            baseValue *= baseValue;
            exponent >>= 1;
        }

        return result;
    }
}