using System;
using Managers;
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
        private float _defaultSpeed;
        private bool _canMoveTowardsTarget;

        public event Action<Enemy> OnDead;
        public void Init(ObjectPooling pool)
        {
            _pool = pool;
            _currentHealth = maxHealth;
            if (agent == null) agent = GetComponent<NavMeshAgent>();
            _defaultSpeed = agent.speed;
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
                EnemyManager.Instance.EnemyDead(this);
                _currentHealth = maxHealth;
                BackToPool();
            }
        }

        public void SetLookTarget()
        {
            transform.LookAt(_targetPlayer.transform);
        }

        public void SetSpeedMultiplier(float multiplier)
        {
            agent.speed *= multiplier;
        }

        public void ResetSpeed()
        {
            agent.speed = _defaultSpeed;
        }

        public void SetScaleMultiplier(float scale)
        {
            transform.localScale *= scale;
        }
        public void ResetScale()
        {
            transform.localScale = Vector3.one;
        }

        public void FreezeMovement(bool freezeMovement)
        {
            _canMoveTowardsTarget = !freezeMovement;
        }
    }
}