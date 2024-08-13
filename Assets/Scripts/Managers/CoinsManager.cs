using UiManager = Ui.UiManager;

namespace Managers
{
    public class CoinsManager : CollectablesManager
    {

        public override void OnCollectablesCollected()
        {
            value += value;
            UiManager.Instance.OnCoinCollected((int)value);                
        }
    }
}