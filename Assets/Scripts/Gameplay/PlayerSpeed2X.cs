using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class PlayerSpeed2X : PowerCard
    {
        public override void Execute(GameObject requiredObject)
        {
            var player = requiredObject.GetComponent<Player>();
            player.SetPlayerSpeedMultiplier(2);
        }

        public override void OnBeforeCooldown(GameObject requiredObject)
        {
            var player = requiredObject.GetComponent<Player>();
            player.ResetPlayerSpeed();
        }
    }
}