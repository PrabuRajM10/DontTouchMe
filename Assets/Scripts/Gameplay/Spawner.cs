using System;
using Enums;
using UnityEngine;
using Enum = Enums.Enum;

namespace Gameplay
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] protected Transform postSpawnParent;

        [SerializeField] protected Enum.PoolObjectTypes spawnerObjectType;
        protected void Spawn(Enum.PoolObjectTypes type, Vector3 spawnPos)
        {
            switch (type)
            {
                case Enum.PoolObjectTypes.Enemy:
                    var enemyObj = ObjectPooling.Instance.GetEnemy();
                    HandleObjectOnReceived(enemyObj.transform , spawnPos);
                    break;
                case Enum.PoolObjectTypes.Bullet:
                    var bulletObj = ObjectPooling.Instance.GetBullet();
                    HandleObjectOnReceived(bulletObj.transform, spawnPos);
                    break;
                case Enum.PoolObjectTypes.Coin:
                    var coinCollectableObj = ObjectPooling.Instance.GetCoins();
                    HandleObjectOnReceived(coinCollectableObj.transform, spawnPos);
                    break;
                case Enum.PoolObjectTypes.Spell:
                    var xpCollectableObj = ObjectPooling.Instance.GetXp();
                    HandleObjectOnReceived(xpCollectableObj.transform, spawnPos);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        protected virtual void HandleObjectOnReceived(Transform poolObj, Vector3 spawnPos)
        {
            poolObj.position = spawnPos;
            poolObj.SetParent(postSpawnParent);
            // poolObj.localScale = Vector3.one;
            poolObj.gameObject.SetActive(true);
        }
    }
    
}