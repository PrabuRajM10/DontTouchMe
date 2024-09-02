using System.Collections;
using Managers;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class BigEnemies : PowerCard
    {
        
        public override void Execute(GameObject requiredObject)
        {
            base.Execute(requiredObject);
            var enemyManager = requiredObject.GetComponent<EnemyManager>();
            enemyManager.SetEnemiesScaleMultiplier(1.5f);
        }
        

        public override void OnBeforeCooldown(GameObject requiredObject, MonoBehaviour mono)
        {
            var enemyManager = requiredObject.GetComponent<EnemyManager>();
            enemyManager.SetEnemiesScaleMultiplier(1f);
            base.OnBeforeCooldown(requiredObject , mono);
        }
    }
}