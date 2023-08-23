namespace Yannick.Simulation.Matrix.Physic;

public sealed class Chunk
{
    public bool ForceActive { get; set; } = false;
    public bool Active { get; internal set; }
}