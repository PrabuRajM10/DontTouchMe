using Managers;
using UnityEngine;

namespace Gameplay.Cards
{
    [CreateAssetMenu]
    public class BigEnemies : PowerCard
    {
        
        public override void Execute(PowerCardsHandlerEvents handlerEvents)
        {
            base.Execute(handlerEvents);
        }
        

        public override void OnBeforeCooldown(CallBack callBack)
        {
            // var enemyManager = requiredObject.GetComponent<EnemyManager>();
            // enemyManager.SetEnemiesScaleMultiplier(1f);
            base.OnBeforeCooldown(callBack);
        }
    }
}