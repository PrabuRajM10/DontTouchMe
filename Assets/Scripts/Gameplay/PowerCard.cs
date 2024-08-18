using UnityEngine;

namespace Gameplay
{
    public class PowerCard : ScriptableObject
    {
        [SerializeField] private PowerCardsId cardId;

        [SerializeField] private float activeTime;
        [SerializeField] private float cooldownTime;

        public float ActiveTime => activeTime;
        public float CooldownTime => cooldownTime;

        public virtual void Execute(GameObject requiredObject) {}
        public virtual void OnBeforeCooldown(GameObject requiredObject) {}
    }
}