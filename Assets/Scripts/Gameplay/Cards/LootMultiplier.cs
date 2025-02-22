using UnityEngine;

namespace Gameplay.Cards
{
    [CreateAssetMenu]
    public class LootMultiplier : PowerCard
    {
        public override void Execute(PowerCardsHandlerEvents handlerEvents)
        {
            base.Execute(handlerEvents);
            // var collectablesManager = requiredObject.GetComponent<LootValueHandler>();
            // collectablesManager.SetValueMultiplier(2);
        }

        public override void OnBeforeCooldown(CallBack callBack)
        {
            // var collectablesManager = requiredObject.GetComponent<LootValueHandler>();
            // collectablesManager.ResetValues();
            base.OnBeforeCooldown(callBack);
        }
    }
}