using System;
using Managers;
using UnityEngine;

namespace Gameplay
{
    public abstract class Collectables : MonoBehaviour , IPoolableObjects
    {
        [SerializeField] private CollectablesType collectablesType;
        private ObjectPooling _pool;
        protected CollectablesManager respectiveManager;

        public void SetManager(CollectablesManager manager)
        {
            respectiveManager = manager;
        }

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

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<Player>();
            if (player == null) return;
            OnCollected();
            BackToPool();
        }
    }
}