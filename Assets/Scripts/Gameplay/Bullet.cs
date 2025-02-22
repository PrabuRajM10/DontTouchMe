using System;
using Enums;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour , IPoolableObjects
    {
        [SerializeField] private BulletProperties bulletPropertiesSo;
        
        [SerializeField] private Rigidbody rigidBody;

        [SerializeField] private ParticleSystem groundHitParticle;
        [SerializeField] private ParticleSystem enemyHitParticle;

        private ObjectPooling _pool;

        private Vector3 _lastFramePosition;
        private bool _recordPosition;

        private void OnValidate()
        {
            if (rigidBody == null) rigidBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if(!_recordPosition)return;
            _lastFramePosition = transform.position;
        }

        public void Init(ObjectPooling pool)
        {
            _pool = pool;
        }

        public void BackToPool()
        {
            _pool.AddBackToList(this , DTMEnum.PoolObjectTypes.Bullet);
        }

        public void SetPositionAndRotation(Transform muzzlePosition)
        {
            transform.position = muzzlePosition.position;
            transform.localRotation = Quaternion.LookRotation(muzzlePosition.forward);
        }

        public void Fire()
        {
            gameObject.SetActive(true);
            _recordPosition = true;
            rigidBody.AddForce(transform.forward * bulletPropertiesSo.Speed , ForceMode.Force);
            Invoke(nameof(DisableBullet) , 10f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            
            if (collision.gameObject.GetComponent<Gun>() || collision.gameObject.GetComponent<CollectablesMagnet>() || collision.gameObject.GetComponent<Bullet>())
            {
                return;
            }

            var contactPoint = collision.contacts[0];
            var enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                var enemyHit = ObjectPooling.Instance.GetEnemyHit();
                var rotation = Quaternion.LookRotation(contactPoint.normal).normalized;
                enemyHit.Play(contactPoint.point , rotation);
            }
            else
            {
                var bulletHit = ObjectPooling.Instance.GetBulletHit();
                bulletHit.Play(contactPoint.point);
            }

            DisableBullet();
        }

        private void DisableBullet()
        {
            ResetVelocity();
            BackToPool();
            _recordPosition = false;
        }

        private void ResetVelocity()
        {
            rigidBody.velocity = Vector3.zero;
        }

        public float Damage()
        {
            return bulletPropertiesSo.DamageAmount;
        }
    }
}