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
        Coin,
        Xp,
        Bomb
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
        private List<Bomb> _bombListPool = new List<Bomb>();
        private List<Collectables> _coinListPool = new List<Collectables>();
        private List<Collectables> _xpListPool = new List<Collectables>();

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
                case PoolObjectTypes.Xp:
                    _xpListPool.Add((Collectables)poolObject);
                    break;
                case PoolObjectTypes.Bomb:
                    _bombListPool.Add((Bomb)poolObject);
                    break;
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
                    var coin = GetCollectablesObject(_coinListPool , PoolObjectTypes.Coin);
                    coin.SetManager(CollectablesManagerHolder.Instance.GetManager(CollectablesType.Coins));
                    return coin;
                case PoolObjectTypes.Xp:
                    var xp = GetCollectablesObject(_xpListPool , PoolObjectTypes.Xp);
                    xp.SetManager(CollectablesManagerHolder.Instance.GetManager(CollectablesType.Xp));
                    return xp;
                case PoolObjectTypes.Bomb:
                    if (_bombListPool.Count < 2)
                    {
                        var poolObjectData = GetPoolProjectData(PoolObjectTypes.Bomb);
                        Spawn(poolObjectData.prefab , 5 ,poolObjectData.poolObjectType , poolObjectData.spawnParent);
                    }            
                    var bomb = _bombListPool[0];
                    _bombListPool.Remove(bomb);
                    bomb.transform.parent = null;
                    return bomb;
                default:
                    return null;
            }
        }

        private Collectables GetCollectablesObject(List<Collectables> collectablesListPool, PoolObjectTypes collectablesType)
        {
            if (collectablesListPool.Count < 2)
            {
                var poolObjectData = GetPoolProjectData(collectablesType);
                Spawn(poolObjectData.prefab, 10, poolObjectData.poolObjectType, poolObjectData.spawnParent);
            }

            var collectable = collectablesListPool[0];
            collectablesListPool.Remove(collectable);
            collectable.transform.parent = null;
            return collectable;
            
        }

        public Bullet GetBullet()
        {
            return (Bullet)GetObjectFromPool(PoolObjectTypes.Bullet);
        }

        public Enemy GetEnemy()
        {
            return (Enemy)GetObjectFromPool(PoolObjectTypes.Enemy);
        }
        
        public Collectables GetCoins()
        {
            return (Collectables)GetObjectFromPool(PoolObjectTypes.Coin);
        }
        
        public Collectables GetXp()
        {
            return (Collectables)GetObjectFromPool(PoolObjectTypes.Xp);
        }

        public Bomb GetBomb()
        {
            return (Bomb)GetObjectFromPool(PoolObjectTypes.Bomb);
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
                    var coin = (Collectables)poolable;
                    ResetObjects(coin.gameObject, PoolObjectTypes.Coin);
                    _coinListPool.Add(coin);
                    break;
                case PoolObjectTypes.Xp:
                    var xp = (Collectables)poolable;
                    ResetObjects(xp.gameObject, PoolObjectTypes.Xp);
                    _xpListPool.Add(xp);
                    break;
                case PoolObjectTypes.Bomb:
                    var bomb = (Bomb)poolable;
                    ResetObjects(bomb.gameObject, PoolObjectTypes.Bomb);
                    _bombListPool.Add(bomb);
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