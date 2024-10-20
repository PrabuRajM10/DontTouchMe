using System;
using Gameplay;
using Managers;
using UnityEngine;

namespace Helpers
{
    public class PutChildrenBackToPool : MonoBehaviour
    {
        private void OnEnable()
        {
            GameManager.OnGameEnd += BackToPool;
        }

        private void OnDisable()
        {
            GameManager.OnGameEnd -= BackToPool;
        }

        private void BackToPool(bool b)
        {
            foreach (Transform child in transform)
            {
                var poolObj = child.GetComponent<Collectables>();
                poolObj.BackToPool();
            }
        }
    }
}
