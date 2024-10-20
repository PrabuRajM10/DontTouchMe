using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class Ghost : PowerCard
    {
        public override void Execute(GameObject requiredObject)
        {
            base.Execute(requiredObject);
            var player = requiredObject.GetComponent<Player>();
            player.Immune(true);
        }
        public override void OnBeforeCooldown(GameObject requiredObject, MonoBehaviour mono, CallBack onCooldownDone)
        {
            var player = requiredObject.GetComponent<Player>();
            player.Immune(false);
            base.OnBeforeCooldown(requiredObject , mono  , onCooldownDone);
        }
    }
}