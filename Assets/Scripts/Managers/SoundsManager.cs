using UnityEngine;

namespace Managers
{
    public class SoundsManager : GenericSingleton<SoundsManager>
    {
        [SerializeField] private int currentScore;
        public void OnCoinCollected()
        {
            
        }
    }
}