using System;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    public enum PowerCardState
    {
        Ready,
        Active,
        Cooldown
    }
    public class PowerCardsHandler : MonoBehaviour
    {
        private PowerCardState _powerCardState;

        private PowerCard _currentActiveCard;

        private PowerCard _powerCard1;
        private PowerCard _powerCard2;
        private PowerCard _powerCard3;

        private float _activeTime;
        private float _cooldownTime;
        private void Update()
        {
            switch (_powerCardState)
            {
                case PowerCardState.Ready:
                    if (InputManager.Instance.IsPowerCard1KeyPressed())
                    {
                        Debug.Log("[PowerCardsHandler] [Update] PowerCard1KeyPressed ");
                        _currentActiveCard = _powerCard1;
                    }
                    if (InputManager.Instance.IsPowerCard2KeyPressed())
                    {
                        Debug.Log("[PowerCardsHandler] [Update] IsPowerCard2KeyPressed ");
                        _currentActiveCard = _powerCard2;
                    }
                    if (InputManager.Instance.IsPowerCard3KeyPressed())
                    {
                        Debug.Log("[PowerCardsHandler] [Update] IsPowerCard3KeyPressed ");
                        _currentActiveCard = _powerCard3;
                    }

                    if (_currentActiveCard == null) return;
                    _activeTime = _currentActiveCard.ActiveTime;

                    _powerCardState = PowerCardState.Active;
                    
                    break;
                case PowerCardState.Active:

                    if (_activeTime > 0)
                    {
                        _activeTime -= Time.deltaTime;
                    }
                    else
                    {
                        _powerCardState = PowerCardState.Cooldown;
                        _cooldownTime = _currentActiveCard.CooldownTime;
                    }
                    
                    break;
                case PowerCardState.Cooldown:
                    if (_cooldownTime > 0)
                    {
                        _cooldownTime -= Time.deltaTime;
                    }
                    else
                    {
                        _powerCardState = PowerCardState.Ready;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SetChosenCurrentCard(PowerCard[] powerCards)
        {
            _powerCard1 = powerCards[0];
            _powerCard2 = powerCards[1];
            _powerCard3 = powerCards[2];
        }
    }
}