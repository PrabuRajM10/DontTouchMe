using System;
using UnityEngine;
using UnityEngine.Serialization;
using Enum = Enums.Enum;

namespace Gameplay
{
    public class InGameParticles : MonoBehaviour, IPoolableObjects
    {
        [SerializeField] private Enum.InGameParticleType particleType;
        [SerializeField] private ParticleSystem particle;
        private ObjectPooling _pool;

        public void Init(ObjectPooling pool)
        {
            _pool = pool;
        }

        public void BackToPool()
        {
            _pool.AddBackToList(this , GetPoolTypeByParticleType());
        }

        private Enum.PoolObjectTypes GetPoolTypeByParticleType()
        {
            return particleType switch
            {
                Enum.InGameParticleType.BulletHit => Enum.PoolObjectTypes.BulletHitParticle,
                Enum.InGameParticleType.EnemyHit => Enum.PoolObjectTypes.EnemyHitParticle,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void Play(Vector3 newPos, Quaternion rotation = default)
        {
            transform.position = newPos;
            transform.rotation = rotation;
            gameObject.SetActive(true);
            particle.Play();
            Invoke(nameof(BackToPool) , particle.main.duration);
        }
    }
}