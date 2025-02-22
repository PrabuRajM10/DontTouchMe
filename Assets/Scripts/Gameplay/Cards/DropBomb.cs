using UnityEngine;

namespace Gameplay.Cards
{
    [CreateAssetMenu]
    public class DropBomb : PowerCard
    {
        public override void Execute(PowerCardsHandlerEvents handlerEvents)
        {
            base.Execute(handlerEvents);
            // var player = requiredObject.GetComponent<Player>();
            // player.DropBomb();
        }
    }
}