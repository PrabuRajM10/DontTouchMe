using System;
using System.Collections;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;
using Enum = Enums.Enum;

namespace Gameplay
{
    public class Bomb : MonoBehaviour, IPoolableObjects
    {
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private SphereCollider sc;
     
        [SerializeField] private float timeBeforeExplosion;
        [SerializeField] private float explodeRadius = 2f;
        [SerializeField] private float damageAmount = 200;
        [SerializeField] private float thrust = 15f;

        private ObjectPooling _pool;

        private void OnValidate()
        {
            if (sc == null) sc = GetComponent<SphereCollider>();
        }

        public void Init(ObjectPooling pool)
        {
            _pool = pool;
            rigidBody = GetComponent<Rigidbody>();
        }

        public void BackToPool()
        {
            _pool.AddBackToList(this , Enum.PoolObjectTypes.Bomb);
        }

        public void Deploy(Vector3 position)
        {
            var transform1 = transform;
            transform1.position = position;
            rigidBody.AddForce(transform1.up * thrust);
            StartCoroutine(StartTimer());
        }

        IEnumerator StartTimer()
        {
            float currentTime = 0;
            while (currentTime < timeBeforeExplosion)
            {
                currentTime += Time.deltaTime;
                yield return null;
            }
            
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, explodeRadius);
            foreach (var hitCollider in hitColliders)
            {
                var enemy = hitCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damageAmount);
                }
            }
            
            BackToPool();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Jumpable>() != null)
            {
                sc.isTrigger = false;
            }
        }
    }
}