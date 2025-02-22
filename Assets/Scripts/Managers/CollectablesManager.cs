using System;
using Enums;
using Gameplay;
using UnityEngine;

namespace Managers
{
    public abstract class CollectablesManager : MonoBehaviour
    {
        [SerializeField] protected CollectablesDataHolder collectablesDataHolderSo;
        [SerializeField] protected DTMEnum.CollectablesType collectablesType;
        public abstract void OnCollectablesCollected();
        public abstract void Reset();

        private void OnEnable()
        {
            GameManager.OnGameEnd += OnGameEnd;
        }

        private void OnDisable()
        {
            GameManager.OnGameEnd -= OnGameEnd;
        }

        private void OnGameEnd(bool obj)
        {
            Reset();
        }
    }
}