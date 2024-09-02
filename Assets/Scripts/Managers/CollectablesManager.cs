using System;
using Gameplay;
using UnityEngine;

namespace Managers
{
    public abstract class CollectablesManager : MonoBehaviour
    {
        [SerializeField] protected CollectablesDataHolder collectablesDataHolderSo;
        [SerializeField] protected CollectablesType collectablesType;
        public abstract void OnCollectablesCollected();
    }
}