using Managers;
using UnityEngine;

namespace Gameplay
{
    public class SlowEnemies : PowerCard
    {
        public override void Execute(GameObject requiredObject)
        {
            var enemyManager = requiredObject.GetComponent<EnemyManager>();
            enemyManager.SetEnemiesSpeedMultiplier(0.5f);
        }

        public override void OnBeforeCooldown(GameObject requiredObject)
        {
            var enemyManager = requiredObject.GetComponent<EnemyManager>();
            enemyManager.ResetEnemiesSpeed();
        }
    }
}