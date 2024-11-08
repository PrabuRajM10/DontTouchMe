using Enums;
using UnityEngine;

namespace Gameplay
{
    public class Xp : Collectables
    {
        protected override void OnCollected()
        {
            SoundManager.PlaySound(Enum.SoundType.Spell , GetPosition());
            respectiveManager.OnCollectablesCollected();
        }
    }
}