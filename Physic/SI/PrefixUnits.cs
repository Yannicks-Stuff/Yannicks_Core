namespace Yannick.Physic.SI;

using Unit = Converter;

/// <summary>
/// Represents the SI unit system, providing a common interface for different SI prefixes.
/// </summary>
public interface SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    public static Prefix Prefix { get; }

    /// <summary>
    /// Converts a value with the specified prefix to a value with the target prefix (e.g., converting meters to centimeters).
    /// </summary>
    /// <param name="current">The value to convert.</param>
    /// <param name="target">The target prefix for the conversion.</param>
    /// <returns>The value converted to the target prefix.</returns>
    public static decimal Convert(decimal current, Prefix target) =>
        Unit.Convert(current, Prefix, target);

    /// <summary>
    /// Converts a value with the specified prefix to a value with the target prefix (e.g., converting meters to centimeters).
    /// </summary>
    /// <param name="target">The target prefix for the conversion.</param>
    /// <returns>The value converted to the target prefix.</returns>
    public decimal Convert(Prefix target);
}

/// <summary>
/// A wrapper for operator overloading <see cref="SI"/>
/// </summary>
public interface SI<T> where T : SI
{
}

/// <summary>
/// Represents the Yotta prefix in the SI unit system.
/// </summary>
public interface IYotta : SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    new static Prefix Prefix => Prefix.Yotta;
}

/// <summary>
/// Represents the Zetta prefix in the SI unit system.
/// </summary>
public interface IZetta : SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    new static Prefix Prefix => Prefix.Zetta;
}

/// <summary>
/// Represents the Exa prefix in the SI unit system.
/// </summary>
public interface IExa : SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    new static Prefix Prefix => Prefix.Exa;
}

/// <summary>
/// Represents the Peta prefix in the SI unit system.
/// </summary>
public interface IPeta : SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    new static Prefix Prefix => Prefix.Peta;
}

/// <summary>
/// Represents the Tera prefix in the SI unit system.
/// </summary>
public interface ITera : SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    new static Prefix Prefix => Prefix.Tera;
}

/// <summary>
/// Represents the Giga prefix in the SI unit system.
/// </summary>
public interface IGiga : SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    new static Prefix Prefix => Prefix.Giga;
}

/// <summary>
/// Represents the Mega prefix in the SI unit system.
/// </summary>
public interface IMega : SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    new static Prefix Prefix => Prefix.Mega;
}

/// <summary>
/// Represents the Kilo prefix in the SI unit system.
/// </summary>
public interface IKilo : SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    new static Prefix Prefix => Prefix.Kilo;
}

/// <summary>
/// Represents the Hecto prefix in the SI unit system.
/// </summary>
public interface IHecto : SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    new static Prefix Prefix => Prefix.Hecto;
}

/// <summary>
/// Represents the Deca prefix in the SI unit system.
/// </summary>
public interface IDeca : SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    new static Prefix Prefix => Prefix.Deca;
}

/// <summary>
/// Represents the base prefix for <see cref="BaseUnit"/>
/// </summary>
public interface INone : SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    new static Prefix Prefix => Prefix.None;
}

/// <summary>
/// Represents the Deci prefix in the SI unit system.
/// </summary>
public interface IDeci : SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    new static Prefix Prefix => Prefix.Deci;
}

/// <summary>
/// Represents the Centi prefix in the SI unit system.
/// </summary>
public interface ICenti : SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    new static Prefix Prefix => Prefix.Centi;
}

/// <summary>
/// Represents the Milli prefix in the SI unit system.
/// </summary>
public interface IMilli : SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    new static Prefix Prefix => Prefix.Milli;
}

/// <summary>
/// Represents the Micro prefix in the SI unit system.
/// </summary>
public interface IMicro : SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    new static Prefix Prefix => Prefix.Micro;
}

/// <summary>
/// Represents the Nano prefix in the SI unit system.
/// </summary>
public interface INano : SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    new static Prefix Prefix => Prefix.Nano;
}

/// <summary>
/// Represents the Pico prefix in the SI unit system.
/// </summary>
public interface IPico : SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    new static Prefix Prefix => Prefix.Pico;
}

/// <summary>
/// Represents the Femto prefix in the SI unit system.
/// </summary>
public interface IFemto : SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    new static Prefix Prefix => Prefix.Femto;
}

/// <summary>
/// Represents the Atto prefix in the SI unit system.
/// </summary>
public interface IAtto : SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    new static Prefix Prefix => Prefix.Atto;
}

/// <summary>
/// Represents the Zepto prefix in the SI unit system.
/// </summary>
public interface IZepto : SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    new static Prefix Prefix => Prefix.Zepto;
}

/// <summary>
/// Represents the Yocto prefix in the SI unit system.
/// </summary>
public interface IYocto : SI
{
    /// <summary>
    /// Gets the prefix associated with the SI unit.
    /// </summary>
    new static Prefix Prefix => Prefix.Yocto;
}