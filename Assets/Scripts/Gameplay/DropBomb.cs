using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class DropBomb : PowerCard
    {
        public override void Execute(GameObject requiredObject)
        {
            base.Execute(requiredObject);
            var player = requiredObject.GetComponent<Player>();
            player.DropBomb();
        }
    }
}