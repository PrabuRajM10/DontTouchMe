using UnityEngine;

namespace Gameplay
{
    public interface IDamageable
    {
        public void TakeDamage(float damage);
    }

    public interface IParticleEmitter
    {
        public void PlayParticle(ParticleSystem particleSystem , Vector3 position);

        public void OnParticleSpawnDone();
    }
}