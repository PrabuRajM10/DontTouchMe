using Enums;
using Managers;

namespace Gameplay
{
    public class Coin : Collectables
    {
        protected override void OnCollected()
        {
            SoundManager.PlaySound(Enum.SoundType.Coin , GetPosition());
            respectiveManager.OnCollectablesCollected();
        }
    }
}