using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;
using Enum = Enums.Enum;

namespace Gameplay
{
    [Serializable]
    public class CollectablesData
    {
        public Enum.CollectablesType collectableType;
        public float value;
        public float multiplier;
    }
    [CreateAssetMenu(menuName = "Create CollectablesDataHolder", fileName = "CollectablesDataHolder", order = 0)]
    public class CollectablesDataHolder : ScriptableObject
    {
        [SerializeField] private List<CollectablesData> collectablesData;

        public void SetAllMultiplierValue(float multiplier)
        {
            foreach (var data in collectablesData)
            {
                data.multiplier = multiplier;
            }
        }

        public float  GetValueByType(Enum.CollectablesType type)
        {
            foreach (var data in collectablesData.Where(data => data.collectableType == type))
            {
                return data.value * data.multiplier;
            }
            return -1;
        }
    }
}