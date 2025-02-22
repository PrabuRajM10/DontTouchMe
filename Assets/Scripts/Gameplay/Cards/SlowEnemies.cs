using Managers;
using UnityEngine;

namespace Gameplay.Cards
{
    [CreateAssetMenu]
    public class SlowEnemies : PowerCard
    {
        public override void Execute(PowerCardsHandlerEvents handlerEvents)
        {
            base.Execute(handlerEvents);
            // var enemyManager = requiredObject.GetComponent<EnemyManager>();
            // enemyManager.SetEnemiesSpeedMultiplier(0.5f);
        }

        public override void OnBeforeCooldown(CallBack callBack)
        {
            // var enemyManager = requiredObject.GetComponent<EnemyManager>();
            // enemyManager.SetEnemiesSpeedMultiplier(1);
            base.OnBeforeCooldown(callBack);
        }
    }
}