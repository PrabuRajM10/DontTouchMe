using Managers;
using UnityEngine;

namespace Gameplay
{
    public class Heal : Collectables
    {
        [SerializeField] private int value;

        protected override void OnCollected()
        {
            CoinsManager.Instance.OnCoinCollected(value);
        }
    }
}