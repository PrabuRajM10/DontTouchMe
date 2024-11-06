using System;
using Enums;
using Helpers;
using Managers;
using UnityEngine;
using UnityEngine.AI;
using Enum = Enums.Enum;

namespace Gameplay
{
    public class Enemy : MonoBehaviour , IPoolableObjects , IDamageable
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private float maxHealth = 100;
        [SerializeField] private Rigidbody rigidbody;

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
            _pool.AddBackToList(this , Enum.PoolObjectTypes.Enemy);        
        }

        private void OnValidate()
        {
            if (agent == null) agent = GetComponent<NavMeshAgent>();
            if (rigidbody == null) rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            ResetVelocity();
            ResetScale();
            SetSpeed(EnemyManager.Instance.GetEnemySpeed());
            SetScale(EnemyManager.Instance.GetEnemyScale());
        }

        // private void OnDisable()
        // {
        //     ResetScale();
        // }

        private void Update()
        {
            if(agent.speed <= 0) return;
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

        public void SetTarget(Player target)
        {
            _targetPlayer = target;
            _canMoveTowardsTarget = true;
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

        public void SetSpeed(float multiplier)
        {
            agent.speed = multiplier;
        }

        public void SetScale(float scale)
        {
            LeanAnimator.Scale(transform , transform.localScale , Vector3.one * scale, LeanTweenType.easeOutBack , 0.5f);
        }

        public void ResetVelocity()
        {
            rigidbody.velocity = Vector3.zero;
        }

        void ResetScale()
        {
            transform.localScale = Vector3.zero;
        }
    }
}