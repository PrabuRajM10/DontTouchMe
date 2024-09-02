using Ui;
using UnityEngine;

namespace Managers
{
    public class XpManager : CollectablesManager
    {
        public override void OnCollectablesCollected()
        {
            var xpValue = (int)collectablesDataHolderSo.GetValueByType(collectablesType);
            UiManager.Instance.OnXpCollected(xpValue);
        }
    }
}