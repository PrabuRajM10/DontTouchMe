using Enums;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour , IPoolableObjects , IParticleEmitter
    {
        [SerializeField] private BulletProperties bulletPropertiesSo;
        
        [SerializeField] private Rigidbody rigidBody;

        [SerializeField] private ParticleSystem groundHitParticle;
        [SerializeField] private ParticleSystem enemyHitParticle;

        private ObjectPooling _pool;

        private void OnValidate()
        {
            if (rigidBody == null) rigidBody = GetComponent<Rigidbody>();
        }

        public void Init(ObjectPooling pool)
        {
            _pool = pool;
        }

        public void BackToPool()
        {
            _pool.AddBackToList(this , Enum.PoolObjectTypes.Bullet);
        }

        public void SetPositionAndRotation(Transform muzzlePosition)
        {
            transform.position = muzzlePosition.position;
            transform.localRotation = Quaternion.LookRotation(muzzlePosition.forward);
        }

        public void Fire()
        {
            gameObject.SetActive(true);
            rigidBody.AddForce(transform.forward * bulletPropertiesSo.Speed , ForceMode.Force);
            Invoke(nameof(DisableBullet) , 10f);
        }

        private void OnTriggerEnter(Collider other)
        {
            // Debug.Log(" OnTriggerEnter " + other.name);
            if (other.GetComponent<Gun>() || other.GetComponent<CollectablesMagnet>())
            {
                return;
            }

            ResetVelocity();
            var contactPoint = other.ClosestPoint(transform.position);
            Debug.DrawLine(transform.position, contactPoint, Color.blue, 200f);
            PlayParticle(other.GetComponent<Enemy>() != null ? enemyHitParticle : groundHitParticle , contactPoint);
        }

        private void DisableBullet()
        {
            ResetVelocity();
            BackToPool();
        }

        private void ResetVelocity()
        {
            rigidBody.velocity = Vector3.zero;
        }

        public float Damage()
        {
            return bulletPropertiesSo.DamageAmount;
        }

        public void PlayParticle(ParticleSystem particleSystem , Vector3 position)
        {
            Debug.Log("PlayParticle");
            particleSystem.transform.position = position;
            particleSystem.Play();
            Invoke(nameof(OnParticleSpawnDone) , particleSystem.main.duration);
        }

        public void OnParticleSpawnDone()
        {
            BackToPool();
        }
    }
}