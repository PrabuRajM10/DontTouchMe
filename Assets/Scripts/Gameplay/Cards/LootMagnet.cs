using UnityEngine;

namespace Gameplay.Cards
{
    [CreateAssetMenu]
    public class LootMagnet : PowerCard
    {
        public override void Execute(PowerCardsHandlerEvents handlerEvents)
        {
            base.Execute(handlerEvents);
            // var player = requiredObject.GetComponent<Player>();
            // player.AttractAllCollectables(true);
        }
        public override void OnBeforeCooldown(CallBack callBack)
        {
            // var player = requiredObject.GetComponent<Player>();
            // player.AttractAllCollectables(false);
            base.OnBeforeCooldown(callBack);
        }
    }
}