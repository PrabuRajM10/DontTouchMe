using Managers;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class SlowEnemies : PowerCard
    {
        public override void Execute(GameObject requiredObject)
        {
            base.Execute(requiredObject);
            var enemyManager = requiredObject.GetComponent<EnemyManager>();
            enemyManager.SetEnemiesSpeedMultiplier(0.5f);
        }

        public override void OnBeforeCooldown(GameObject requiredObject, MonoBehaviour mono)
        {
            var enemyManager = requiredObject.GetComponent<EnemyManager>();
            enemyManager.SetEnemiesSpeedMultiplier(1);
            base.OnBeforeCooldown(requiredObject , mono);
        }
    }
}