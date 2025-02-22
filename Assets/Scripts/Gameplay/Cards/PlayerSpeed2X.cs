using UnityEngine;

namespace Gameplay.Cards
{
    [CreateAssetMenu]
    public class PlayerSpeed2X : PowerCard
    {
        public override void Execute(PowerCardsHandlerEvents handlerEvents)
        {
            base.Execute(handlerEvents);
            // var player = requiredObject.GetComponent<Player>();
            // player.SetPlayerSpeedMultiplier(2);
        }

        public override void OnBeforeCooldown(CallBack callBack)
        {
            // var player = requiredObject.GetComponent<Player>();
            // player.ResetPlayerSpeed();
            base.OnBeforeCooldown(callBack);
        }
    }
}