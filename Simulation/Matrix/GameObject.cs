using System.Numerics;
using Yannick.Chemistry;
using Yannick.Physic.SI.AmountSubstance;
using Yannick.Physic.SI.Mass;
using Yannick.Physic.SI.Pressure;
using Yannick.Physic.SI.Temperature;
using Yannick.Simulation.Matrix.Physic;

namespace Yannick.Simulation.Matrix;

public interface IGameObject<T> where T : GameObject
{
    public static abstract GameObject NewCondition { get; }
    public Guid ID { get; }
}

public abstract class GameObject<T> : GameObject where T : GameObject, IGameObject<T>
{
    protected GameObject(Server server, GameObject parent) : base(server, parent)
    {
    }
}

/// <summary>
/// Diese Klasse ist das Oberhaupt nach <see cref="System.Object"/>
/// </summary>
public abstract class GameObject : object, IEquatable<GameObject>
{
    public readonly GameObject Parent;
    public readonly Server Server;


    internal GameObject(Server server, GameObject parent)
    {
        Server = server;
        Parent = parent;
    }

    /// <summary>
    /// Aus was besteht das Object
    /// </summary>
    public abstract IReadOnlyDictionary<Molecular, Mol> ObjectComponents { get; }

    public Kelvin Temperature { get; protected set; }
    public Vector3 Position { get; init; }
    public Chunk? Chunk { get; init; }
    public Litre Volume { get; init; }
    public Guid ID => Server.CreateNewObjectID(this);
    public bool IsStatic { get; init; }

    public Pascal Pressure
        => new(ObjectComponents.Values.Sum(mol => mol.m_value) * Server.R * Temperature / Volume.m_value * 0.001m);

    public IList<GameObject> Children => new List<GameObject>();
    public DateTime Age { get; protected set; } = DateTime.Now;
    public bool Equals(GameObject? other) => !ReferenceEquals(other, null) && other.ID == ID;


    /// <summary>
    /// Wird jedesmal aufgerufen wenn das objekt Aktiv ist
    /// </summary>
    /// <param name="pastTime">Wie viel Zeit ist vergangen</param>
    public virtual void OnTick(TimeSpan pastTime)
    {
    }


    public override int GetHashCode() => ID.GetHashCode();
}