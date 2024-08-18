using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class HitMan : PowerCard
    {
        public override void Execute(GameObject requiredObject)
        {
            var drone = requiredObject.GetComponent<Drone>();
            drone.SetBulletDamage(999);
        }
        public override void OnBeforeCooldown(GameObject requiredObject)
        {
            var drone = requiredObject.GetComponent<Drone>();
            drone.ResetBulletDamage();
        }
    }
}