using System;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class BulletProperties : ScriptableObject
    {
        [SerializeField] protected float speed = 100f;
        [SerializeField] protected float damageAmount = 10;

        private float _defaultDamage;

        private void Awake()
        {
            _defaultDamage = damageAmount;
        }

        public float Speed
        {
            get => speed;
            set => speed = value;
        }
        
        public float DamageAmount
        {
            get => damageAmount;
            set => damageAmount = value;
        }

        public void ResetDamage()
        {
            damageAmount = _defaultDamage;
        }
    }
}