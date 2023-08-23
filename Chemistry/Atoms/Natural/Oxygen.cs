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
    public static Atom Instance => new Oxygen();
}