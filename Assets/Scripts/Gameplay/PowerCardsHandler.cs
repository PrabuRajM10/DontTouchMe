using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Managers;
using Problem2;
using UnityEngine;
using UnityEngine.Serialization;
using Enum = Enums.Enum;
using UiManager = Managers.UiManager;

namespace Gameplay
{
   

    [Serializable]
    public class PowerCardReference
    {
        public Enum.PowerCardsId cardId;
        public GameObject requiredReference;
    }
    public class PowerCardsHandler : MonoBehaviour
    {
        [SerializeField] private List<PowerCardReference> powerCardReferences = new List<PowerCardReference>();
        [FormerlySerializedAs("xpManager")] [SerializeField] private SpellManager spellManager; 


        private Dictionary<int, PowerCard> _powerCards = new Dictionary<int, PowerCard>();

        private float _activeTime;
        private float _cooldownTime;
        private GameObject _requiredRef;

        private int _currentPowerCardIndex;

        private void OnEnable()
        {
            GameManager.OnGameEnd += OnGameEnd;
        }

        private void OnDisable()
        {
            GameManager.OnGameEnd -= OnGameEnd;
        }
        


        private void Update()
        {
            if(IsAnyCardActive()) return;
            if (InputManager.Instance.IsPowerCard1KeyPressed())
            {
                _currentPowerCardIndex = 1;
                HandleOnKeyPressed();
            }
            else if (InputManager.Instance.IsPowerCard2KeyPressed())
            {
                _currentPowerCardIndex = 2;
                HandleOnKeyPressed();
            }
            else if (InputManager.Instance.IsPowerCard3KeyPressed())
            {
                _currentPowerCardIndex = 3;
                HandleOnKeyPressed();
            }
            
        }

        private void OnGameEnd(bool b)
        {
            if (_currentPowerCardIndex <= 0)
            {
                _powerCards.Clear();
                return;
            }
            StopAllCoroutines();
            _requiredRef = GetReferenceByCardType(GetCurrentCard().CardId);
            GetCurrentCard().ResetAbility(_requiredRef);
            Reset();
        }
        private void HandleOnKeyPressed()
        {
            if (_currentPowerCardIndex == 0 || GetCurrentCard().CardState is PowerCardState.UnAvailable or not PowerCardState.Ready or PowerCardState.Cooldown)
            {
                return;
            }

            UiManager.Instance.OnPowerCardActivated(_currentPowerCardIndex, GetCurrentCard().ActiveTime , GetCurrentCard().XpCost);
            spellManager.OnCurrentXpValueUpdate(-GetCurrentCard().XpCost);
            UpdateCardsUnAvailability();
            ExecutePowerCard(_currentPowerCardIndex);
        }

        PowerCard GetCurrentCard()
        {
            return _powerCards[_currentPowerCardIndex];
        }

        void UpdateCardsUnAvailability()
        {
            foreach (var powerCard in _powerCards)
            {
                if(powerCard.Value.CardState is PowerCardState.Cooldown or PowerCardState.Active ) return;
                if (spellManager.GetXpValue() < powerCard.Value.XpCost)
                {
                    powerCard.Value.CardState = PowerCardState.UnAvailable;
                }
            }
        }

        void ExecutePowerCard(int index)
        {
            var card = _powerCards[index];
            _activeTime = card.ActiveTime;
            _requiredRef = GetReferenceByCardType(card.CardId);

            if (_requiredRef != null)
            {
                card.Execute(_requiredRef);
                StartCoroutine(PowerCardActivated(index));
            }
            else
            {
                Debug.LogError("Cant find required reference || Card id : " + card.CardId);
            }
        }

        IEnumerator PowerCardActivated(int index)
        {
            yield return new WaitForSeconds(_activeTime);
            
            _activeTime = 0;
            _currentPowerCardIndex = 0;
            var card = _powerCards[index];
            card.OnBeforeCooldown(_requiredRef , this , OnCooldownDone);
            UiManager.Instance.PowerCardOnCoolDown(index , card.CooldownTime);
        }

        private void OnCooldownDone()
        {
            spellManager.SetCardAvailabilityIfPossible();
        }

        public bool IsAnyCardActive()
        {
            return _activeTime > 0;
        }

        public void SetChosenCurrentCard(PowerCard[] powerCards)
        {
            _powerCards.Add(1,powerCards[0]);
            _powerCards.Add(2,powerCards[1]);
            _powerCards.Add(3,powerCards[2]);
        }

        public void Reset()
        {
            _activeTime = 0;
            _currentPowerCardIndex = 0;
            _powerCards.Clear();
        }

        GameObject GetReferenceByCardType(Enum.PowerCardsId cardId)
        {
            foreach (var powerCardReference in powerCardReferences.Where(powerCardReference => powerCardReference.cardId == cardId))
            {
                return powerCardReference.requiredReference;
            }

            return null;
        }
    }
}