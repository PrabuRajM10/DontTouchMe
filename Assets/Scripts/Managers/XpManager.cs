using System;
using UnityEngine;

namespace Managers
{
    public class XpManager : CollectablesManager
    {
        [SerializeField] private int currentXpValue;
        [SerializeField] private int xpMultiplier;

        public static event Action<int> OnXpCollected; 
        public override void OnCollectablesCollected()
        {
            currentXpValue++;
            OnXpCollected?.Invoke(currentXpValue * xpMultiplier);
        }
    }
}