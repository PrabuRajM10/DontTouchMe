using UnityEngine;

namespace Managers
{
    public class CoinsManager : GenericSingleton<CoinsManager>
    {
        public void OnCoinCollected(int coinValue)
        {
            Debug.Log("[CoinsManager] [OnCoinCollected] ++ coinValue " + coinValue);
        }
    }
}