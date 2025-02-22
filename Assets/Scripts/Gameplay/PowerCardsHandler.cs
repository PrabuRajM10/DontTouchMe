using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UiManager = Managers.UiManager;

namespace Gameplay
{
    [Serializable]
    public class PowerCardReference
    {
        public DTMEnum.PowerCardsId cardId;
        public GameObject requiredReference;
    }
    public class PowerCardsHandler : MonoBehaviour
    {
        [FormerlySerializedAs("xpManager")] [SerializeField] private SpellManager spellManager;
        [SerializeField] private PowerCardsHandlerEvents powerCardsHandlerEvents;


        private Dictionary<int, PowerCard> _powerCards = new Dictionary<int, PowerCard>();

        private float _activeTime;
        private float _cooldownTime;

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
            GetCurrentCard().ResetAbility();
            Reset();
        }
        private void HandleOnKeyPressed()
        {
            if (GetCurrentCard().CardState is not PowerCardState.Ready)
            {
                return;
            }

            UiManager.Instance.OnPowerCardActivated(_currentPowerCardIndex, GetCurrentCard().ActiveTime , GetCurrentCard().XpCost);
            spellManager.OnCurrentXpValueUpdate(-GetCurrentCard().XpCost);
            ExecutePowerCard(_currentPowerCardIndex);
            UpdateCardsUnAvailability();
        }

        PowerCard GetCurrentCard()
        {
            return _powerCards[_currentPowerCardIndex];
        }

        void UpdateCardsUnAvailability()
        {
            foreach (var powerCard in _powerCards.Where(powerCard => powerCard.Value.CardState is not (PowerCardState.Cooldown or PowerCardState.Active or PowerCardState.UnAvailable)))
            {
                powerCard.Value.CardState = PowerCardState.UnAvailable;
            }
        }

        void ExecutePowerCard(int index)
        {
            var card = _powerCards[index];
            _activeTime = card.ActiveTime;
            card.Execute(powerCardsHandlerEvents);
            StartCoroutine(PowerCardActivated(index));
        }
        

        IEnumerator PowerCardActivated(int index)
        {
            yield return new WaitForSeconds(_activeTime);
            
            _activeTime = 0;
            _currentPowerCardIndex = 0;
            var card = _powerCards[index];
            card.OnBeforeCooldown(OnCooldownDone);
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
    }
}