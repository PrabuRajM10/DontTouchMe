using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public enum PowerCardState
    {
        Ready,
        Active,
        Cooldown
    }
    public class PowerCard : ScriptableObject
    {
        [SerializeField] private PowerCardsId cardId;

        [SerializeField] private float activeTime;
        [SerializeField] private float cooldownTime;

        private PowerCardState _cardState;

        private bool _isOnCooldown;
        public PowerCardsId CardId => cardId;
        public float ActiveTime => activeTime;
        public float CooldownTime => cooldownTime;

        public PowerCardState CardState
        {
            get => _cardState;
            set => _cardState = value;
        }

        public virtual void Execute(GameObject requiredObject)
        {
            _cardState = PowerCardState.Active;
        }

        public virtual void OnBeforeCooldown(GameObject requiredObject, MonoBehaviour mono)
        {
            mono.StartCoroutine(StartCooldown());
        }

        IEnumerator StartCooldown()
        {
            _cardState = PowerCardState.Cooldown;
            yield return new WaitForSeconds(cooldownTime);
            _cardState = PowerCardState.Ready;
        }
    }
}