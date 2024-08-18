using Managers;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class LootMultiplier : PowerCard
    {
        public override void Execute(GameObject requiredObject)
        {
            var collectablesManager = requiredObject.GetComponent<CollectablesManager>();
            collectablesManager.SetValueMultiplier(2);
        }

        public override void OnBeforeCooldown(GameObject requiredObject)
        {
            var collectablesManager = requiredObject.GetComponent<CollectablesManager>();
            collectablesManager.ResetValues();
        }
    }
}