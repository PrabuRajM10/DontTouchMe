using Managers;
using UnityEngine;

namespace Gameplay
{
    public class DualGuns : PowerCard
    {
        public override void Execute(GameObject requiredObject)
        {
            var drone = requiredObject.GetComponent<Drone>();
            drone.DualGuns(true);
        }

        public override void OnBeforeCooldown(GameObject requiredObject)
        {
            var drone = requiredObject.GetComponent<Drone>();
            drone.DualGuns(false);
        }
    }
}