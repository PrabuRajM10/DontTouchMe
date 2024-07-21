using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

namespace Gameplay
{
    public class CollectablePrefabHandler : MonoBehaviour
    {
        [SerializeField]private Dictionary<CollectablesType, Collectables> _collectablesDict =
            new Dictionary<CollectablesType, Collectables>();

        private void OnValidate()
        {
            if (transform.childCount <= _collectablesDict.Count) return;
            
            foreach (Transform child in transform)
            {
                var collectable = child.GetComponent<Collectables>();
                if (_collectablesDict.TryAdd(collectable.CollectablesType, collectable))
                {
                    Debug.Log("collectable added " + collectable.name);
                }

            }
        }
    }
}