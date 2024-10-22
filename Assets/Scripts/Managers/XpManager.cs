using System.Collections.Generic;
using Gameplay;
using Ui;
using UnityEngine;

namespace Managers
{
    public class XpManager : CollectablesManager
    {
        [SerializeField] private int totalXpValue;
        private List<CardData> _powerCardsData = new List<CardData>();
        private CardData _powerCard1Data;
        private CardData _powerCard2Data;
        private CardData _powerCard3Data;
        
        public override void OnCollectablesCollected()
        {
            totalXpValue += (int)collectablesDataHolderSo.GetValueByType(collectablesType);
            UiManager.Instance.OnXpCollected(totalXpValue);

            if (_powerCardsData.Count <= 0)
            {
                _powerCardsData = GameManager.Instance.GetCurrentPowerCards();

                _powerCard1Data = _powerCardsData[0];
                _powerCard2Data = _powerCardsData[1];
                _powerCard3Data = _powerCardsData[2];
            }

            if(GameManager.Instance.IsAnyCardActive()) return;
            Debug.Log("[XpManager] [OnCollectablesCollected] _powerCard1Data " + (_powerCard1Data.cardName , _powerCard2Data.cardName , _powerCard3Data.cardName));
            SetCardAvailabilityIfPossible();
        }

        public void SetCardAvailabilityIfPossible()
        {
            if (_powerCard1Data.powerCard.XpCost <= totalXpValue && _powerCard1Data.powerCard.CardState == PowerCardState.UnAvailable)
            {
                _powerCard1Data.powerCard.CardState = PowerCardState.Ready;
                UiManager.Instance.SetPowerCardAvailability(1, true);
            }

            if (_powerCard2Data.powerCard.XpCost <= totalXpValue && _powerCard2Data.powerCard.CardState == PowerCardState.UnAvailable)
            {
                _powerCard2Data.powerCard.CardState = PowerCardState.Ready;
                UiManager.Instance.SetPowerCardAvailability(2, true);
            }

            if (_powerCard3Data.powerCard.XpCost <= totalXpValue && _powerCard3Data.powerCard.CardState == PowerCardState.UnAvailable)
            {
                _powerCard3Data.powerCard.CardState = PowerCardState.Ready;
                UiManager.Instance.SetPowerCardAvailability(3, true);
            }
        }

        public void OnCurrentXpValueUpdate(int valueDifference)
        {
            totalXpValue += valueDifference;
        }

        public int GetXpValue()
        {
            return totalXpValue;
        }

        public override void Reset()
        {
            totalXpValue = 0;
            _powerCardsData.Clear();
        }
    }
}