using System.Numerics;

namespace Yannick.UI;

public partial class Console
{
    public partial class Engine2D
    {
        public class RealPlayer : GameEntity
        {
            public Vector2 Position;

            public RealPlayer(Engine2D server) : base(server)
            {
            }
        }
    }
}