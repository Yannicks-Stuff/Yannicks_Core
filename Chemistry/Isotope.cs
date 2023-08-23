using Yannick.Chemistry.Mathematic;
using Yannick.Physic.SI.Temperature;

namespace Yannick.Chemistry;

public abstract class Isotope<T> : Atom where T : Atom, IAtom
{
    public override Kelvin MeltingPoint => T.Instance.MeltingPoint;
    public override Kelvin BoilingPoint => T.Instance.BoilingPoint;
    public override Category Category => T.Instance.Category;
    public override CrystalSystem CrystalSystem => T.Instance.CrystalSystem;
    public override byte Electrons => T.Instance.Electrons;
    public override byte Protons => T.Instance.Protons;
    public override string Symbol => T.Instance.Symbol;
    public override string Name => T.Instance.Name;

    public override IReadOnlyDictionary<PhysicalState, ThermalConductivity> ThermalConductivity =>
        T.Instance.ThermalConductivity;

    public abstract byte Neutron { get; }
    public abstract Dalton Mass { get; }
}