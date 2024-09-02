using UnityEngine;

namespace Gameplay
{
    public class LootValueHandler : MonoBehaviour
    {
        [SerializeField] protected CollectablesDataHolder collectablesDataHolderSo;
        public void SetValueMultiplier(float multiplier)
        {
            collectablesDataHolderSo.SetAllMultiplierValue(multiplier);
        }
        public void ResetValues()
        {
            collectablesDataHolderSo.SetAllMultiplierValue(1);

        }
    }
}