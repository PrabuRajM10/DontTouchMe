using UnityEngine;

namespace Gameplay.Cards
{
    [CreateAssetMenu]
    public class HitMan : PowerCard
    {
        public override void Execute(PowerCardsHandlerEvents handlerEvents)
        {
            base.Execute(handlerEvents);
            // var drone = requiredObject.GetComponent<Drone>();
            // drone.SetBulletDamage(999);
        }
        public override void OnBeforeCooldown(CallBack callBack)
        {
            // var drone = requiredObject.GetComponent<Drone>();
            // drone.ResetBulletDamage();
            base.OnBeforeCooldown(callBack);
        }
    }
}