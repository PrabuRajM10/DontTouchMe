using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    [CreateAssetMenu]
    public class EnemyProperties : ScriptableObject
    {
        [SerializeField] float enemySpeed;
        [SerializeField] float enemyScale;
        [SerializeField] float enemySpeedMultiplier;
        [SerializeField] float enemyScaleMultiplier;

        public float EnemySpeedMultiplier
        {
            get => enemySpeedMultiplier;
            set => enemySpeedMultiplier = value;
        }

        public float EnemyScaleMultiplier
        {
            get => enemyScaleMultiplier;
            set => enemyScaleMultiplier = value;
        }

        public float EnemySpeed
        {
            get => enemySpeed * enemySpeedMultiplier;
            set => enemySpeed = value;
        }

        public float EnemyScale
        {
            get => enemyScale * enemyScaleMultiplier;
            set => enemyScale = value;
        }

    }
}