using System.Numerics;

namespace Yannick.UI;

public partial class Console
{
    public partial class Engine2D
    {
        public class GameChunk
        {
            public static readonly Vector2 Size = new(16, 16);
            public readonly Vector2 Position;

            public readonly Engine2D Server;

            public GameChunk(Engine2D server, Vector2 position)
            {
                Server = server;
                Position = position;
            }

            public void Load()
            {
            }

            public void Unload()
            {
            }
        }
    }
}