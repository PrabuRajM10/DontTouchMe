using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    public abstract class Collectables : MonoBehaviour , IPoolableObjects
    {
        [SerializeField] private CollectablesType collectablesType;
        private ObjectPooling _pool;

        protected abstract void OnCollected();

        public CollectablesType CollectablesType => collectablesType;
        public void Init(ObjectPooling pool)
        {
            _pool = pool;
        }

        public void BackToPool()
        {
            _pool.AddBackToList(this , PoolObjectTypes.Coin);
        }
    }
}