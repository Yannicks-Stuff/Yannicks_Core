using Yannick.Chemistry.Mathematic;
using Yannick.Physic.SI.Temperature;

namespace Yannick.Chemistry.Atoms.Natural;

public sealed class Oxygen : Atom<Oxygen>, IAtom
{
    public override Kelvin MeltingPoint => new Kelvin(54.36m);
    public override Kelvin BoilingPoint => new Kelvin(90.20m);
    public override Category Category => Category.NonMetals;
    public override CrystalSystem CrystalSystem => CrystalSystem.Cubic;
    public override string Symbol => "O";
    public override string Name => "Oxygen";
    public override byte Electrons => 8;
    public override byte Protons => 8;

    public override IReadOnlyDictionary<PhysicalState, ThermalConductivity> ThermalConductivity =>
        new Dictionary<PhysicalState, ThermalConductivity>
        {
            {
                PhysicalState.Gas, new ThermalConductivity
                {
                    Temperature = 273,
                    Aggregate = PhysicalState.Gas,
                    Atom = Instance,
                    Pressure = 101325,
                    Length = 1,
                    Value = (decimal)(26.58 * 10E-3)
                }
            },
            {
                PhysicalState.Fluid, new ThermalConductivity
                {
                    Temperature = new Kelvin(90.20m),
                    Aggregate = PhysicalState.Fluid,
                    Atom = Instance,
                    Pressure = 101325,
                    Length = 1,
                    Value = 0.152M
                }
            },
            {
                PhysicalState.Solid, new ThermalConductivity
                {
                    Temperature = new Kelvin(54.36m),
                    Aggregate = PhysicalState.Solid,
                    Atom = Instance,
                    Pressure = 101325,
                    Length = 1,
                    Value = 0.036M
                }
            }
        };

    public static Atom Instance => new Oxygen();
}