using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class LootMagnet : PowerCard
    {
        public override void Execute(GameObject requiredObject)
        {
            var player = requiredObject.GetComponent<Player>();
            player.AttractAllCollectables(true);
        }
        public override void OnBeforeCooldown(GameObject requiredObject)
        {
            var player = requiredObject.GetComponent<Player>();
            player.AttractAllCollectables(false);
        }
    }
}