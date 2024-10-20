using System;
using System.Collections;
using UnityEngine;

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
        [SerializeField] private PowerCardsId cardId;

        [SerializeField] private float activeTime;
        [SerializeField] private float cooldownTime;
        [SerializeField] private int xpCost;

        [SerializeField] private PowerCardState _cardState = PowerCardState.UnAvailable;

        private bool _isOnCooldown;
        public PowerCardsId CardId => cardId;
        public float ActiveTime => activeTime;
        public float CooldownTime => cooldownTime;
        public int XpCost => xpCost;
        public delegate void CallBack();

        private CallBack _callBack;

        public PowerCardState CardState
        {
            get => _cardState;
            set => _cardState = value;
        }

        public void Init()
        {
            _cardState = PowerCardState.UnAvailable;
        }
        public virtual void Execute(GameObject requiredObject)
        {
            _cardState = PowerCardState.Active;
        }

        public virtual void OnBeforeCooldown(GameObject requiredObject, MonoBehaviour mono , CallBack onCooldownDone)
        {
            _callBack = onCooldownDone;
            if(mono != null)mono.StartCoroutine(StartCooldown());
        }

        public void ResetAbility(GameObject requiredObject)
        {
            OnBeforeCooldown(requiredObject, null , () => {_cardState = PowerCardState.UnAvailable;});
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