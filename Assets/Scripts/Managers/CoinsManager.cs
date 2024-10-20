using UnityEngine;
using UiManager = Managers.UiManager;

namespace Managers
{
    public class CoinsManager : CollectablesManager
    {
        [SerializeField] private int coinCount;
        public override void OnCollectablesCollected()
        {
            var coinValue = (int)collectablesDataHolderSo.GetValueByType(collectablesType);
            coinCount += coinValue;

            UiManager.Instance.OnCoinCollected(coinCount);                
        }

        public override void Reset()
        {
            coinCount = 0;
        }
    }
}