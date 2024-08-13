using System;
using UnityEngine;

namespace Managers
{
    public abstract class CollectablesManager : MonoBehaviour
    {
        [SerializeField] private int valueMultiplier;
        [SerializeField] protected float value;

        private float _defaultValue;

        private void Awake()
        {
            _defaultValue = value;
        }

        public void SetValueMultiplier(float multiplier)
        {
            value *= multiplier;
        }
        public void ResetValues()
        {
            value = _defaultValue;
        }
        public abstract void OnCollectablesCollected();
    }
}