using Managers;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class FreezeEnemies : PowerCard
    {
        public override void Execute(GameObject requiredObject)
        {
            base.Execute(requiredObject);
            var enemyManager = requiredObject.GetComponent<EnemyManager>();
            enemyManager.FreezeEnemies(true);
        }
        public override void OnBeforeCooldown(GameObject requiredObject, MonoBehaviour mono, CallBack onCooldownDone)
        {
            var enemyManager = requiredObject.GetComponent<EnemyManager>();
            enemyManager.FreezeEnemies(false);
            base.OnBeforeCooldown(requiredObject , mono , onCooldownDone);
        }
    }
}