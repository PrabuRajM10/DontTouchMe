using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class PowerCard : ScriptableObject
    {
        [SerializeField] private PowerCardsId cardId;
        [SerializeField] private CardRarity rarity;

        public virtual void Execute(GameObject requiredObject) {}
        public virtual void OnBeforeCooldown(GameObject requiredObject) {}
    }
}