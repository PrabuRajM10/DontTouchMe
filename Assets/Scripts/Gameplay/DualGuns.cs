using Managers;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class DualGuns : PowerCard
    {
        public override void Execute(GameObject requiredObject)
        {
            base.Execute(requiredObject);
            var drone = requiredObject.GetComponent<Drone>();
            drone.DualGuns(true);
        }

        public override void OnBeforeCooldown(GameObject requiredObject, MonoBehaviour mono, CallBack onCooldownDone)
        {
            var drone = requiredObject.GetComponent<Drone>();
            drone.DualGuns(false);
            base.OnBeforeCooldown(requiredObject , mono , onCooldownDone);
        }
    }
}