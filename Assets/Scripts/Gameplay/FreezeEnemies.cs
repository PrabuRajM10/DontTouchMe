using Managers;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class FreezeEnemies : PowerCard
    {
        public override void Execute(GameObject requiredObject)
        {
            var enemyManager = requiredObject.GetComponent<EnemyManager>();
            enemyManager.FreezeEnemies(true);
        }
        public override void OnBeforeCooldown(GameObject requiredObject)
        {
            var enemyManager = requiredObject.GetComponent<EnemyManager>();
            enemyManager.FreezeEnemies(false);
        }
    }
}