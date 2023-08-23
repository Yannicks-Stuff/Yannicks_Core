using System.Collections.Concurrent;
using Yannick.Chemistry;
using Yannick.Simulation.Matrix;

namespace Yannick.Simulation;

public sealed class Server
{
    public const decimal R = 8.314m;
    internal ConcurrentBag<GameObject> ActiveDynamicObjects = new();
    internal ConcurrentDictionary<Guid, GameObject> ActiveObjects = new();
    public DateTime CurrentDateTime => new(0, 0, 0);
    public TimeSpan TickRate { get; init; } = TimeSpan.FromMilliseconds(500);


    internal Guid CreateNewObjectID(GameObject obj) => Guid.NewGuid();

    private void OnTick()
    {
    }
}