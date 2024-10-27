using System;
using System.Collections.Generic;
using Gameplay;
using PlayerPrefs;
using Ui;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class SpellManager : CollectablesManager
    {
        [FormerlySerializedAs("totalXpValue")] [SerializeField] private int totalSpellValue;
        private List<CardData> _powerCardsData = new List<CardData>();
        private CardData _powerCard1Data;
        private CardData _powerCard2Data;
        private CardData _powerCard3Data;

        public static Action OnSpellCollectedForFirstTime;
        
        public override void OnCollectablesCollected()
        {
            if (PlayerPrefManager.GetIsSpellCollectedForFirstTime() < 1 && totalSpellValue <= 0)
            {
                Debug.Log("Spell collected for first time");
                OnSpellCollectedForFirstTime?.Invoke();
                PlayerPrefManager.UpdateIsSpellCollectedForFirstTime(1);
            }
            totalSpellValue += (int)collectablesDataHolderSo.GetValueByType(collectablesType);
            UiManager.Instance.OnXpCollected(totalSpellValue);

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
            if (_powerCard1Data.powerCard.XpCost <= totalSpellValue && _powerCard1Data.powerCard.CardState == PowerCardState.UnAvailable)
            {
                _powerCard1Data.powerCard.CardState = PowerCardState.Ready;
                UiManager.Instance.SetPowerCardAvailability(1, true);
            }

            if (_powerCard2Data.powerCard.XpCost <= totalSpellValue && _powerCard2Data.powerCard.CardState == PowerCardState.UnAvailable)
            {
                _powerCard2Data.powerCard.CardState = PowerCardState.Ready;
                UiManager.Instance.SetPowerCardAvailability(2, true);
            }

            if (_powerCard3Data.powerCard.XpCost <= totalSpellValue && _powerCard3Data.powerCard.CardState == PowerCardState.UnAvailable)
            {
                _powerCard3Data.powerCard.CardState = PowerCardState.Ready;
                UiManager.Instance.SetPowerCardAvailability(3, true);
            }
        }

        public void OnCurrentXpValueUpdate(int valueDifference)
        {
            totalSpellValue += valueDifference;
        }

        public int GetXpValue()
        {
            return totalSpellValue;
        }

        public override void Reset()
        {
            totalSpellValue = 0;
            _powerCardsData.Clear();
        }
    }
}