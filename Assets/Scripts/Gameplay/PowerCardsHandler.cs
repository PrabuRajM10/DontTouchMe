using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
   

    [Serializable]
    public class PowerCardReference
    {
        public PowerCardsId cardId;
        public GameObject requiredReference;
    }
    public class PowerCardsHandler : MonoBehaviour
    {
        [SerializeField] private List<PowerCardReference> powerCardReferences = new List<PowerCardReference>();

        private PowerCard _currentActiveCard;

        private PowerCard _powerCard1;
        private PowerCard _powerCard2;
        private PowerCard _powerCard3;

        private float _activeTime;
        private float _cooldownTime;
        private GameObject _requiredRef;

        private void Update()
        {
            if (InputManager.Instance.IsPowerCard1KeyPressed())
            {
                Debug.Log("[PowerCardsHandler] [Update] PowerCard1KeyPressed ");
                _currentActiveCard = _powerCard1;
                ExecutePowerCard();
            }
            if (InputManager.Instance.IsPowerCard2KeyPressed())
            {
                Debug.Log("[PowerCardsHandler] [Update] IsPowerCard2KeyPressed ");
                _currentActiveCard = _powerCard2;
                ExecutePowerCard();
            }
            if (InputManager.Instance.IsPowerCard3KeyPressed())
            {
                Debug.Log("[PowerCardsHandler] [Update] IsPowerCard3KeyPressed ");
                _currentActiveCard = _powerCard3;
                ExecutePowerCard();
            }
        }

        void ExecutePowerCard()
        {
            // if(_currentActiveCard.CardState != PowerCardState.Ready)return;
            _activeTime = _currentActiveCard.ActiveTime;
            _requiredRef = GetReferenceByCardType(_currentActiveCard.CardId);

            if (_requiredRef != null)
            {
                _currentActiveCard.Execute(_requiredRef);
                StartCoroutine(PowerCardActivated());
            }
            else
            {
                Debug.LogError("Cant find required reference || Card id : " + _currentActiveCard.CardId);
            }
        }

        IEnumerator PowerCardActivated()
        {
            yield return new WaitForSeconds(_activeTime);
            _currentActiveCard.OnBeforeCooldown(_requiredRef , this);
        }

        public void SetChosenCurrentCard(PowerCard[] powerCards)
        {
            _powerCard1 = powerCards[0];
            _powerCard2 = powerCards[1];
            _powerCard3 = powerCards[2];
        }

        GameObject GetReferenceByCardType(PowerCardsId cardId)
        {
            foreach (var powerCardReference in powerCardReferences.Where(powerCardReference => powerCardReference.cardId == cardId))
            {
                return powerCardReference.requiredReference;
            }

            return null;
        }
    }
}