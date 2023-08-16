namespace Yannick.Extensions.EnumExtensions;

/// <summary>
/// Include extensions for enums
/// </summary>
public static class EnumExtension
{
    /// <summary>
    /// Retrieves the first attribute of the specified type that is attached to the given Enum.
    /// </summary>
    /// <typeparam name="T">The type of the attribute to be retrieved. Must inherit from Attribute.</typeparam>
    /// <param name="enumVal">The Enum from which the attribute should be retrieved.</param>
    /// <returns>The first attribute of the specified type attached to the Enum, or null if no such attribute exists.</returns>
    public static T? Attribute<T>(this Enum enumVal) where T : Attribute
    {
        var type = enumVal.GetType();
        var memInfo = type.GetMember(enumVal.ToString());
        var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
        return (attributes.Length > 0) ? (T)attributes[0] : null;
    }

    /// <summary>
    /// Retrieves all flags from the specified Enum of type T.
    /// </summary>
    /// <typeparam name="T">The type of the Enum. Must inherit from Enum.</typeparam>
    /// <param name="enum">The Enum from which the flags should be retrieved.</param>
    /// <returns>An IEnumerable of type T containing all flags from the specified Enum.</returns>
    public static IEnumerable<T> Flags<T>(this T @enum) where T : Enum
    {
        var c = (Enum)@enum;
        foreach (var a in Enum.GetNames(typeof(T)))
        {
            var v = Enum.Parse(typeof(T), a);
            if (Equals(v, c))
                yield return (T)v;
        }
    }

    /// <summary>
    /// Retrieves the name of the given Enum value as a string.
    /// </summary>
    /// <param name="enum">The Enum value whose name should be retrieved.</param>
    /// <returns>The name of the Enum value as a string.</returns>
    public static string Name(this Enum @enum) => Enum.GetName(@enum.GetType(), @enum)!;
}