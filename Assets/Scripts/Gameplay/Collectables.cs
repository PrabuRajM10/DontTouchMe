using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Helpers;
using Managers;
using UnityEngine;
using Enum = Enums.Enum;

namespace Gameplay
{
    public abstract class Collectables : MonoBehaviour , IPoolableObjects
    {
        [SerializeField] private Enum.CollectablesType collectablesType;
        private ObjectPooling _pool;
        protected CollectablesManager respectiveManager;
        private float _speed = 25;

        private void OnEnable()
        {
            LeanAnimator.Scale(transform , Vector3.zero, Vector3.one, LeanTweenType.easeOutBack , 0.3f);
        }

        public void SetManager(CollectablesManager manager)
        {
            respectiveManager = manager;
        }

        protected abstract void OnCollected();

        public Enum.CollectablesType CollectablesType => collectablesType;
        public void Init(ObjectPooling pool)
        {
            _pool = pool;
        }

        public void BackToPool()
        {
            _pool.AddBackToList(this , GetPoolTypeByCollectableType());
        }

        Enum.PoolObjectTypes GetPoolTypeByCollectableType()
        {
            switch (CollectablesType)
            {
                case Enum.CollectablesType.Coins:
                    return Enum.PoolObjectTypes.Coin;
                case Enum.CollectablesType.Spell:
                    return Enum.PoolObjectTypes.Xp;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<Player>();
            if (player == null) return;
            OnCollected();
            BackToPool();
        }

        public void MoveTowardsTarget(Transform target)
        {
            StartCoroutine(MoveToTarget(target));
        }

        IEnumerator MoveToTarget(Transform target)
        {
            while (Vector3.Distance(transform.position , target.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}