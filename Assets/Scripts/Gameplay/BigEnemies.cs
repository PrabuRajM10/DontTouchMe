using Managers;
using UnityEngine;

namespace Gameplay
{
    public class BigEnemies : PowerCard
    {
        public override void Execute(GameObject requiredObject)
        {
            var enemyManager = requiredObject.GetComponent<EnemyManager>();
            enemyManager.SetEnemiesScaleMultiplier(1.5f);
        }

        public override void OnBeforeCooldown(GameObject requiredObject)
        {
            var enemyManager = requiredObject.GetComponent<EnemyManager>();
            enemyManager.ResetEnemiesScale();
        }
    }
}