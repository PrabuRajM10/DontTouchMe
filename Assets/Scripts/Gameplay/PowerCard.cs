using System;
using System.Collections;
using Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public enum PowerCardState
    {
        Ready,
        Active,
        Cooldown,
        UnAvailable
    }
    public class PowerCard : ScriptableObject
    {
        [SerializeField] private DTMEnum.PowerCardsId cardId;

        [SerializeField] private float activeTime;
        [SerializeField] private float cooldownTime;
        [SerializeField] private int xpCost;

        [SerializeField] private PowerCardState _cardState = PowerCardState.UnAvailable;

        private bool _isOnCooldown;
        public DTMEnum.PowerCardsId CardId => cardId;
        public float ActiveTime => activeTime;
        public float CooldownTime => cooldownTime;
        public int XpCost => xpCost;
        public delegate void CallBack();

        private CallBack _callBack;

        public event Action<PowerCardState> OnCardStateChanged;


        protected PowerCardsHandlerEvents powerCardsHandlerEvents;

        public PowerCardState CardState
        {
            get => _cardState;
            set
            {
                _cardState = value;
                OnCardStateChanged?.Invoke(_cardState);
            }
        }

        public void Init()
        {
            _cardState = PowerCardState.UnAvailable;
        }
        public virtual void Execute(PowerCardsHandlerEvents powerCardsHandlerEvents)
        {
            this.powerCardsHandlerEvents = powerCardsHandlerEvents;
            this.powerCardsHandlerEvents.OnReceivedEvents(CardId);
            _cardState = PowerCardState.Active;
        }

        public virtual void OnBeforeCooldown(CallBack onCooldownDone)
        {
            _callBack = onCooldownDone;
            powerCardsHandlerEvents.OnReceivedExitEvents(CardId); 
            powerCardsHandlerEvents.StartCoroutine(StartCooldown());
        }

        public void ResetAbility()
        {
            OnBeforeCooldown(() => {_cardState = PowerCardState.UnAvailable;});
            ResetState();
        }

        public void ResetState()
        {
            _cardState = PowerCardState.UnAvailable;
        }

        IEnumerator StartCooldown()
        {
            _cardState = PowerCardState.Cooldown;
            yield return new WaitForSeconds(cooldownTime);
            _cardState = PowerCardState.UnAvailable;
            _callBack?.Invoke();
        }
    }
}