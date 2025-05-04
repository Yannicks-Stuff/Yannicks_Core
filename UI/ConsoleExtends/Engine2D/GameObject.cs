using System.Numerics;

namespace Yannick.UI;

public partial class Console
{
    public partial class Engine2D
    {
        public abstract class GameObject
        {
            public readonly Engine2D Server;
            public Guid ID = Guid.NewGuid();
            public bool IsStatic;
            public bool NeedActive;
            public Vector2 Position;

            protected GameObject(Engine2D server)
            {
                Server = server;
            }

            /// <summary>
            /// Wird pro Loop aufgerufen wenn das Spiel läuft
            /// </summary>
            public abstract void OnTick();

            /// <summary>
            /// Wird aufgerufen wenn der Spieler aus den Sichtfeld ist.
            /// </summary>
            public abstract void OnHide();

            /// <summary>
            /// Wird aufgerufen wenn der Spieler im Sichtbereich ist.
            /// </summary>
            public abstract void OnShow();

            /// <summary>
            /// Wenn das GameObject zu static oder dynamic wird.
            /// </summary>
            public abstract void OnStaticChange();

            /// <summary>
            /// Wird 1 mal aufgerufen pro Spiel Instanz. Wenn das Spiel gestartet wird.
            /// </summary>
            public abstract void OnCreate();

            /// <summary>
            /// Wird 1 mal aufgerufen pro Spiel Instanz. Wenn das Spiel beendet wird.
            /// </summary>
            public abstract void OnRemove();
        }
    }
}