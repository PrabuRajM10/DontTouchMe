using UnityEngine;

namespace Gameplay
{
    public class Xp : Collectables
    {
        protected override void OnCollected()
        {
            respectiveManager.OnCollectablesCollected();
        }
    }
}