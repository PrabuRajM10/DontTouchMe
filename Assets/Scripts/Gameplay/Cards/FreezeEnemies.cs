using Managers;
using UnityEngine;

namespace Gameplay.Cards
{
    [CreateAssetMenu]
    public class FreezeEnemies : PowerCard
    {
        public override void Execute(PowerCardsHandlerEvents handlerEvents)
        {
            base.Execute(handlerEvents);
            // var enemyManager = requiredObject.GetComponent<EnemyManager>();
            // enemyManager.FreezeEnemies(true);
        }
        public override void OnBeforeCooldown(CallBack callBack)
        {
            // var enemyManager = requiredObject.GetComponent<EnemyManager>();
            // enemyManager.FreezeEnemies(false);
            base.OnBeforeCooldown(callBack);
        }
    }
}