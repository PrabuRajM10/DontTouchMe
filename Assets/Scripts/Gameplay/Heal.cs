using Managers;
using UnityEngine;

namespace Gameplay
{
    public class Heal : Collectables
    {
        protected override void OnCollected()
        {
            respectiveManager.OnCollectablesCollected();
        }
    }
}