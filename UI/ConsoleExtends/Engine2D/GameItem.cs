namespace Yannick.UI;

public partial class Console
{
    public partial class Engine2D
    {
        public abstract class GameItem : GameObject
        {
            protected GameItem(Engine2D server) : base(server)
            {
            }
        }
    }
}