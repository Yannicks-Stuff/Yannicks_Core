using Yannick.Chemistry.Atoms.Natural;
using Yannick.Chemistry.Mathematic;

namespace Yannick.Chemistry.Isotopes.Natural;

public sealed class Oxygen16 : Isotope<Oxygen>
{
    public override byte Neutron => 8;
    public override Dalton Mass => 15.999m;
}