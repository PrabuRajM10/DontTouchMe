using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class HitMan : PowerCard
    {
        public override void Execute(GameObject requiredObject)
        {
            base.Execute(requiredObject);
            var drone = requiredObject.GetComponent<Drone>();
            drone.SetBulletDamage(999);
        }
        public override void OnBeforeCooldown(GameObject requiredObject, MonoBehaviour mono, CallBack onCooldownDone)
        {
            var drone = requiredObject.GetComponent<Drone>();
            drone.ResetBulletDamage();
            base.OnBeforeCooldown(requiredObject , mono , onCooldownDone);
        }
    }
}