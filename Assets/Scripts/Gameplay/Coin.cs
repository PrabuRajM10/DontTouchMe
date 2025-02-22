using Enums;
using Managers;

namespace Gameplay
{
    public class Coin : Collectables
    {
        protected override void OnCollected()
        {
            SoundManager.PlaySound(DTMEnum.SoundType.Coin , GetPosition());
            respectiveManager.OnCollectablesCollected();
        }
    }
}