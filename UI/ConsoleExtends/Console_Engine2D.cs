using System.Numerics;
using System.Runtime.CompilerServices;

namespace Yannick.UI;

public partial class Console
{
    public partial class Engine2D
    {
        public static readonly Dictionary<Engine2D, List<GameChunk>> ActiveChunks = new();
        public static readonly Dictionary<Engine2D, List<GameEntity>> ActiveEntities = new();
        private static readonly object L = new();

        public readonly RealPlayer Player;

        public readonly Guid ServerID = Guid.NewGuid();
        private byte _renderDistance = 16;
        public TimeSpan Ticks = TimeSpan.FromMilliseconds(20);

        public Engine2D()
        {
            Player = new RealPlayer(this);

            lock (L)
            {
                ActiveChunks.Add(this, new List<GameChunk>());
                ActiveEntities.Add(this, new List<GameEntity>());
            }
        }

        public byte RenderDistance
        {
            get => _renderDistance;
            set => _renderDistance = value == 0 ? byte.MaxValue : value;
        }

        public void LoadChunks(Vector2 area)
        {
            var chunks = ActiveChunks[this];

            var chunksToKeep = new List<GameChunk>();
            var entitiesToKeep = new HashSet<Vector2>();

            foreach (var entity in ActiveEntities[this])
                if (entity.NeedActive)
                    entitiesToKeep.Add(entity.Position);

            var minX = area.X - _renderDistance;
            var maxX = area.X + _renderDistance;
            var minY = area.Y - _renderDistance;
            var maxY = area.Y + _renderDistance;

            for (var x = minX; x <= maxX; x++)
            {
                for (var y = minY; y <= maxY; y++)
                {
                    var chunkPosition = new Vector2(x, y);
                    if (Vector2.Distance(chunkPosition, area) <= _renderDistance)
                    {
                        var existingChunk = chunks.FirstOrDefault(c => c.Position == chunkPosition);
                        if (existingChunk == null)
                        {
                            existingChunk = new GameChunk(this, chunkPosition);
                            existingChunk.Load();
                        }

                        chunksToKeep.Add(existingChunk);
                    }
                }
            }

            foreach (var chunk in chunks)
                if (!chunksToKeep.Contains(chunk) && !entitiesToKeep.Contains(chunk.Position))
                    chunk.Unload();

            chunks.Clear();
            chunks.AddRange(chunksToKeep);
        }

        public void UnloadChunks(GameEntity entity)
        {
            var chunks = ActiveChunks[this];

            var chunksToKeep = new List<GameChunk>();
            var entitiesToKeep = new HashSet<Vector2>();


            if (ActiveEntities.TryGetValue(this, out var entities))
            {
                foreach (var activeEntity in entities)
                {
                    if (activeEntity != entity && activeEntity.NeedActive)
                    {
                        entitiesToKeep.Add(activeEntity.Position);
                    }
                }
            }

            foreach (var chunk in chunks)
            {
                if (entitiesToKeep.Contains(chunk.Position))
                {
                    chunksToKeep.Add(chunk);
                }
                else
                {
                    chunk.Unload();
                }
            }

            chunks.Clear();
            chunks.AddRange(chunksToKeep);
        }

        public void RemoveEntity(GameEntity entity, bool unloadChunks = false)
        {
            if (unloadChunks)
                UnloadChunks(entity);

            ActiveEntities[this].Remove(entity);
        }

        public void Start()
        {
        }
    }
}