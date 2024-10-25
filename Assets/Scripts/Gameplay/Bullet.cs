using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour , IPoolableObjects
    {
        [SerializeField] private BulletProperties bulletPropertiesSo;
        
        [SerializeField] private Rigidbody rigidBody;

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
            _pool.AddBackToList(this , PoolObjectTypes.Bullet);
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
            DisableBullet();
        }

        private void DisableBullet()
        {
            ResetVelocity();
            gameObject.SetActive(false);
            BackToPool();
        }

        public void ResetVelocity()
        {
            rigidBody.velocity = Vector3.zero;
        }

        public float Damage()
        {
            return bulletPropertiesSo.DamageAmount;
        }
    }
}