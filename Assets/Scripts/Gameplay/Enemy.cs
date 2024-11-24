using System;
using Enums;
using Helpers;
using Managers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Enum = Enums.Enum;

namespace Gameplay
{
    public class Enemy : MonoBehaviour , IPoolableObjects , IDamageable
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private float maxHealth = 100;
        [FormerlySerializedAs("rigidbody")] [SerializeField] private Rigidbody rb;

        private ObjectPooling _pool;
        private Player _targetPlayer;

        private float _currentHealth;
        private float _defaultSpeed;

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
            if (rb == null) rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
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

        private void OnCollisionEnter(Collision collision)
        {
            var bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.Damage());
            }
        }

        public void SetTarget(Player target)
        {
            _targetPlayer = target;
        }
        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                EnemyManager.Instance.EnemyDead(this);
                _currentHealth = maxHealth;
                SoundManager.PlaySound(Enum.SoundType.EnemyDeath , GetPosition());
                BackToPool();
                return;
            }
            
            SoundManager.PlaySound(Enum.SoundType.EnemyHit , GetPosition());
        }

        Vector3 GetPosition()
        {
            return transform.position;
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
            rb.velocity = Vector3.zero;
        }

        void ResetScale()
        {
            transform.localScale = Vector3.zero;
        }
    }
}