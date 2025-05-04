namespace Yannick.UI;

public partial class Console
{
    public partial class Engine2D
    {
        public abstract class GameEntity : GameObject
        {
            public GameEntity(Engine2D server) : base(server)
            {
                IsStatic = false;
            }

            public override void OnTick()
            {
            }

            public override void OnHide()
            {
            }

            public override void OnShow()
            {
            }

            public override void OnStaticChange()
            {
            }

            public override void OnCreate()
            {
            }

            public override void OnRemove()
            {
            }
        }
    }
}