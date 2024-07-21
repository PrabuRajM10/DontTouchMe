using System;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay
{
    public class Enemy : MonoBehaviour , IPoolableObjects , IDamageable
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private float maxHealth = 100;

        private ObjectPooling _pool;
        private Player _targetPlayer;

        private float _currentHealth;
        private bool _canMoveTowardsTarget;
        public void Init(ObjectPooling pool)
        {
            _pool = pool;
            _currentHealth = maxHealth;
        }

        public void BackToPool()
        {
            _pool.AddBackToList(this , PoolObjectTypes.Enemy);        
        }

        private void OnValidate()
        {
            if (agent == null) agent = GetComponent<NavMeshAgent>();
        }

        public void SetTarget(Player target)
        {
            _targetPlayer = target;
            _canMoveTowardsTarget = true;
        }

        private void Update()
        {
            if(!_canMoveTowardsTarget) return;
            agent.SetDestination(_targetPlayer.GetPosition());
        }

        private void OnTriggerEnter(Collider other)
        {
            var bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.Damage());
            }
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                _currentHealth = maxHealth;
                BackToPool();
            }
        }

        public void SetLookTarget()
        {
            transform.LookAt(_targetPlayer.transform);
        }
    }
}