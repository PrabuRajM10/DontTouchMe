using UnityEngine;

namespace Gameplay.Cards
{
    [CreateAssetMenu]
    public class Ghost : PowerCard
    {
        public override void Execute(PowerCardsHandlerEvents handlerEvents)
        {
            base.Execute(handlerEvents);
            // var player = requiredObject.GetComponent<Player>();
            // player.Immune(true);
        }
        public override void OnBeforeCooldown(CallBack callBack)
        {
            // var player = requiredObject.GetComponent<Player>();
            // // player.Immune(false);
            base.OnBeforeCooldown(callBack);
        }
    }
}