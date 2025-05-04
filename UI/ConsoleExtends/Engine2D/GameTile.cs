namespace Yannick.UI;

public partial class Console
{
    public partial class Engine2D
    {
        public abstract class GameTile : GameObject
        {
            protected GameTile(Engine2D server) : base(server)
            {
            }
        }
    }
}