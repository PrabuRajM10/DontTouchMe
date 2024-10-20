using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class PlayerSpeed2X : PowerCard
    {
        public override void Execute(GameObject requiredObject)
        {
            base.Execute(requiredObject);
            var player = requiredObject.GetComponent<Player>();
            player.SetPlayerSpeedMultiplier(2);
        }

        public override void OnBeforeCooldown(GameObject requiredObject, MonoBehaviour mono, CallBack onCooldownDone)
        {
            var player = requiredObject.GetComponent<Player>();
            player.ResetPlayerSpeed();
            base.OnBeforeCooldown(requiredObject , mono , onCooldownDone);
        }
    }
}