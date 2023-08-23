using Yannick.Chemistry.Atoms.Natural;

namespace Yannick.Chemistry.Element.Natural;

public sealed class Dioxygen : Molecular
{
    public override IReadOnlyList<Atom> Struct => new[]
    {
        Oxygen.Instance, Oxygen.Instance
    };


    public override string Name => "Dioxygen";
}