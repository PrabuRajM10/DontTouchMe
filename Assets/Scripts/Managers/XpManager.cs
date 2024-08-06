using System;
using Ui;
using UnityEngine;

namespace Managers
{
    public class XpManager : CollectablesManager
    {
        [SerializeField] private int currentXpValue;
        [SerializeField] private int xpMultiplier;

        public override void OnCollectablesCollected()
        {
            currentXpValue++;
            UiManager.Instance.OnXpCollected(currentXpValue * xpMultiplier);
        }
    }
}