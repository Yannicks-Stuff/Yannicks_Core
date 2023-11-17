using Yannick.Physic.SI.Temperature;

namespace Yannick.Chemistry;

public interface IAtom
{
    public static abstract Atom Instance { get; }
}

public abstract class Atom
{
    public abstract Kelvin MeltingPoint { get; }
    public abstract Kelvin BoilingPoint { get; }
    public abstract Category Category { get; }
    public abstract CrystalSystem CrystalSystem { get; }
    public abstract string Symbol { get; }
    public abstract string Name { get; }
    public abstract byte Electrons { get; }
    public abstract byte Protons { get; }
    public abstract IReadOnlyDictionary<PhysicalState, ThermalConductivity> ThermalConductivity { get; }
}

public abstract class Atom<T> : Atom where T : IAtom
{
}