using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class Ghost : PowerCard
    {
        public override void Execute(GameObject requiredObject)
        {
            var player = requiredObject.GetComponent<Player>();
            player.Immune(true);
        }
        public override void OnBeforeCooldown(GameObject requiredObject)
        {
            var player = requiredObject.GetComponent<Player>();
            player.Immune(false);
        }
    }
}