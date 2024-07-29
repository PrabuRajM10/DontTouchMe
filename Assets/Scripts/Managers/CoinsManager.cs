using System;
using UnityEngine;

namespace Managers
{
    public class CoinsManager : CollectablesManager
    {
        [SerializeField] private int coinsCollectedCount;
        [SerializeField] private int coinsMultiplier;

        public static event Action<int> OnCoinCollected;
        public override void OnCollectablesCollected()
        {
            coinsCollectedCount++;
            OnCoinCollected?.Invoke(coinsCollectedCount * coinsMultiplier);
        }
    }
}