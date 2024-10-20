using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class LootMagnet : PowerCard
    {
        public override void Execute(GameObject requiredObject)
        {
            base.Execute(requiredObject);
            var player = requiredObject.GetComponent<Player>();
            player.AttractAllCollectables(true);
        }
        public override void OnBeforeCooldown(GameObject requiredObject, MonoBehaviour mono, CallBack onCooldownDone)
        {
            var player = requiredObject.GetComponent<Player>();
            player.AttractAllCollectables(false);
            base.OnBeforeCooldown(requiredObject , mono , onCooldownDone);
        }
    }
}