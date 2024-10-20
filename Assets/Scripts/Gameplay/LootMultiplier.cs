using Managers;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class LootMultiplier : PowerCard
    {
        public override void Execute(GameObject requiredObject)
        {
            base.Execute(requiredObject);
            var collectablesManager = requiredObject.GetComponent<LootValueHandler>();
            collectablesManager.SetValueMultiplier(2);
        }

        public override void OnBeforeCooldown(GameObject requiredObject, MonoBehaviour mono, CallBack onCooldownDone)
        {
            var collectablesManager = requiredObject.GetComponent<LootValueHandler>();
            collectablesManager.ResetValues();
            base.OnBeforeCooldown(requiredObject , mono , onCooldownDone);
        }
    }
}