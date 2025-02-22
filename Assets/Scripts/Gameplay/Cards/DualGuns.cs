using UnityEngine;

namespace Gameplay.Cards
{
    [CreateAssetMenu]
    public class DualGuns : PowerCard
    {
        public override void Execute(PowerCardsHandlerEvents handlerEvents)
        {
            base.Execute(handlerEvents);
            // var drone = requiredObject.GetComponent<Drone>();
            // drone.DualGuns(true);
        }

        public override void OnBeforeCooldown(CallBack callBack)
        {
            // var drone = requiredObject.GetComponent<Drone>();
            // drone.DualGuns(false);
            base.OnBeforeCooldown(callBack);
        }
    }
}