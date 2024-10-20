using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    [CreateAssetMenu]
    public class BulletProperties : ScriptableObject
    {
        [SerializeField] protected float speed = 100f;
        [SerializeField] private float defaultDamage;
        [FormerlySerializedAs("damageAmount")] [SerializeField] protected float currentDamageAmount = 10;

        public float Speed
        {
            get => speed;
            set => speed = value;
        }
        
        public float DamageAmount
        {
            get => currentDamageAmount;
            set => currentDamageAmount = value;
        }

        public void ResetDamage()
        {
            Debug.Log("[BulletProperties] [ResetDamage] _defaultDamage " + defaultDamage);
            currentDamageAmount = defaultDamage;
        }
    }
}