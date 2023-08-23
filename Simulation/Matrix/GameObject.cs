using System.Numerics;
using Yannick.Physic.SI.Mass;
using Yannick.Physic.SI.Temperature;
using Yannick.Simulation.Matrix.Physic;

namespace Yannick.Simulation.Matrix;

/// <summary>
/// Diese Klasse ist das Oberhaupt nach <see cref="System.Object"/>
/// </summary>
public abstract class GameObject : object
{
    public readonly GameObject Parent;
    public readonly Server Server;


    protected GameObject(Server server, GameObject parent)
    {
        Server = server;
        Parent = parent;
    }

    public Kelvin Temperature { get; protected set; }
    public Vector3 Position { get; init; }
    public Chunk Chunk { get; init; }
    public Litre Volume { get; init; }
    public IList<GameObject> Children => new List<GameObject>();

    public DateTime Age { get; protected set; }

    public virtual void OnTick()
    {
    }
}