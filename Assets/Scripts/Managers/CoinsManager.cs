using System;
using Problem2;
using UnityEngine;
using UiManager = Ui.UiManager;

namespace Managers
{
    public class CoinsManager : CollectablesManager
    {
        [SerializeField] private int coinsCollectedCount;
        [SerializeField] private int coinsMultiplier;

        public override void OnCollectablesCollected()
        {
            coinsCollectedCount++;
            UiManager.Instance.OnCoinCollected(coinsCollectedCount);                
        }
    }
}