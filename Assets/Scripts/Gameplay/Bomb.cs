using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class Bomb : MonoBehaviour, IPoolableObjects
    {
        [SerializeField] private Rigidbody rigidBody;
     
        [SerializeField] private float timeBeforeExplosion;
        [SerializeField] private float explodeRadius = 2f;
        [SerializeField] private float damageAmount = 20;

        private ObjectPooling _pool;
        private float _thrust = 5f;

        public void Init(ObjectPooling pool)
        {
            _pool = pool;
            rigidBody = GetComponent<Rigidbody>();
        }

        public void BackToPool()
        {
            _pool.AddBackToList(this , PoolObjectTypes.Bomb);
        }

        public void Deploy(Vector3 position)
        {
            var transform1 = transform;
            transform1.position = position;
            rigidBody.AddForce(transform1.up * _thrust);
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
    }
}