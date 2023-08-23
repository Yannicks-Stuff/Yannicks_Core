using Yannick.Physic.SI.Length;
using Yannick.Physic.SI.Pressure;
using Yannick.Physic.SI.Temperature;

namespace Yannick.Chemistry;

public readonly struct ThermalConductivity
{
    public Kelvin Temperature { get; init; }
    public PhysicalState Aggregate { get; init; }
    public Atom Atom { get; init; }
    public Pascal Pressure { get; init; }
    public Meter Length { get; init; }
    public decimal Value { get; init; }
}