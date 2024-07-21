using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    public enum PoolObjectTypes
    {
        Enemy,
        Bullet,
        Coin
    }

    [Serializable]
    public class PoolObjectData
    {
        public GameObject prefab;
        public Transform spawnParent;
        public int initialSpawnCount;
        public PoolObjectTypes poolObjectType;
    }

    public class ObjectPooling : GenericSingleton<ObjectPooling>
    {
    
        [SerializeField] private List<PoolObjectData> poolObjectDataList;

        private List<Enemy> _enemyListPool = new List<Enemy>();
        private List<Bullet> _bulletListPool = new List<Bullet>();
        private List<Collectables> _coinListPool = new List<Collectables>();

        private void Start()
        {
            foreach (var poolObjectData in poolObjectDataList)
            {
                Spawn(poolObjectData.prefab , poolObjectData.initialSpawnCount , poolObjectData.poolObjectType , poolObjectData.spawnParent);
            }
            // Spawn(bulletPrefab , minBulletCount , PoolObjectTypes.Bullet , bulletSpawnParent);
            // Spawn(enemyPrefab , minEnemyCount , PoolObjectTypes.Enemy , enemySpawnParent);k
        }

        private void Spawn(GameObject poolableObjects, int count , PoolObjectTypes poolObjectType , Transform parent)
        {
            for (int i = 0; i < count; i++)
            {
                var poolObject = Instantiate(poolableObjects , parent);
                poolObject.gameObject.SetActive(false);
            
                var iPoolObject = poolObject.GetComponent<IPoolableObjects>();
                iPoolObject.Init(this);
            
                AddObjectToPool(poolObjectType , iPoolObject);
            }
        }

        private void AddObjectToPool(PoolObjectTypes poolObjectType, IPoolableObjects poolObject)
        {

            switch (poolObjectType)
            {
                case PoolObjectTypes.Enemy:
                    var enemy = (Enemy)poolObject;
                    enemy.SetTarget(GameManager.Instance.Player);
                    _enemyListPool.Add(enemy);
                    break;
                case PoolObjectTypes.Bullet:
                    _bulletListPool.Add((Bullet)poolObject);
                    break;
                case PoolObjectTypes.Coin:
                    _coinListPool.Add((Collectables)poolObject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(poolObjectType), poolObjectType, null);
            }
        }

        IPoolableObjects GetObjectFromPool(PoolObjectTypes poolObjectType)
        {
            switch (poolObjectType)
            {
                case PoolObjectTypes.Enemy:
                    if (_enemyListPool.Count < 2)
                    {
                        var poolObjectData = GetPoolProjectData(PoolObjectTypes.Enemy);
                        Spawn(poolObjectData.prefab , 10 ,poolObjectData.poolObjectType , poolObjectData.spawnParent);
                    }            
                    var enemy = _enemyListPool[0];
                    _enemyListPool.Remove(enemy);
                    enemy.transform.parent = null;
                    return enemy;
            
                case PoolObjectTypes.Bullet:
                    if (_bulletListPool.Count < 2)
                    {
                        var poolObjectData = GetPoolProjectData(PoolObjectTypes.Bullet);
                        Spawn(poolObjectData.prefab , 10 ,poolObjectData.poolObjectType , poolObjectData.spawnParent);
                    }            
                    var bullet = _bulletListPool[0];
                    _bulletListPool.Remove(bullet);
                    bullet.transform.parent = null;
                    return bullet;

                case PoolObjectTypes.Coin:
                    if (_coinListPool.Count < 2)
                    {
                        var poolObjectData = GetPoolProjectData(PoolObjectTypes.Coin);
                        Spawn(poolObjectData.prefab , 10 ,poolObjectData.poolObjectType , poolObjectData.spawnParent);
                    }            
                    var collectable = _coinListPool[0];
                    _coinListPool.Remove(collectable);
                    collectable.transform.parent = null;
                    return collectable;
                default:
                    return null;
            }
        }

        public Bullet GetBullet()
        {
            return (Bullet)GetObjectFromPool(PoolObjectTypes.Bullet);
        }

        public Enemy GetEnemy()
        {
            return (Enemy)GetObjectFromPool(PoolObjectTypes.Enemy);
        }
        
        public Collectables GetCollectables()
        {
            return (Collectables)GetObjectFromPool(PoolObjectTypes.Coin);
        }

        public void AddBackToList(IPoolableObjects poolable , PoolObjectTypes poolObjectTypes)
        {
            switch (poolObjectTypes)
            {
                case PoolObjectTypes.Enemy:
                    var enemy = (Enemy)poolable;
                    ResetObjects(enemy.gameObject , PoolObjectTypes.Enemy);
                    _enemyListPool.Add(enemy);
                    break;
                case PoolObjectTypes.Bullet:
                    var bullet = (Bullet)poolable;
                    ResetObjects(bullet.gameObject, PoolObjectTypes.Bullet);
                    _bulletListPool.Add(bullet);
                    break;
                case PoolObjectTypes.Coin:
                    var collectable = (Collectables)poolable;
                    ResetObjects(collectable.gameObject, PoolObjectTypes.Coin);
                    _coinListPool.Add(collectable);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(poolObjectTypes), poolObjectTypes, null);
            }
        }

        private void ResetObjects(GameObject poolObj , PoolObjectTypes type)
        {
            poolObj.gameObject.SetActive(false);
            poolObj.transform.SetParent(GetPoolProjectData(type).spawnParent);
            poolObj.transform.position = Vector3.zero;
        }

        PoolObjectData GetPoolProjectData(PoolObjectTypes poolObjectType)
        {
            return poolObjectDataList.FirstOrDefault(poolObjectData => poolObjectData.poolObjectType == poolObjectType);
        }
    }
}