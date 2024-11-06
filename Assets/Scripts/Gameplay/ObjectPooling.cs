using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using Enum = Enums.Enum;

namespace Gameplay
{
    

    [Serializable]
    public class PoolObjectData
    {
        public GameObject prefab;
        public Transform spawnParent;
        public int initialSpawnCount;
        public Enum.PoolObjectTypes poolObjectType;
    }

    public class ObjectPooling : GenericSingleton<ObjectPooling>
    {
    
        [SerializeField] private List<PoolObjectData> poolObjectDataList;

        private List<Enemy> _enemyListPool = new List<Enemy>();
        private List<Bullet> _bulletListPool = new List<Bullet>();
        private List<Bomb> _bombListPool = new List<Bomb>();
        private List<Collectables> _coinListPool = new List<Collectables>();
        private List<Collectables> _xpListPool = new List<Collectables>();
        private List<PositionalAudio> _positionalAudios = new List<PositionalAudio>();

        private void Start()
        {
            foreach (var poolObjectData in poolObjectDataList)
            {
                Spawn(poolObjectData.prefab , poolObjectData.initialSpawnCount , poolObjectData.poolObjectType , poolObjectData.spawnParent);
            }
            // Spawn(bulletPrefab , minBulletCount , PoolObjectTypes.Bullet , bulletSpawnParent);
            // Spawn(enemyPrefab , minEnemyCount , PoolObjectTypes.Enemy , enemySpawnParent);k
        }

        private void Spawn(GameObject poolableObjects, int count , Enum.PoolObjectTypes poolObjectType , Transform parent)
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

        private void AddObjectToPool(Enum.PoolObjectTypes poolObjectType, IPoolableObjects poolObject)
        {

            switch (poolObjectType)
            {
                case Enum.PoolObjectTypes.Enemy:
                    var enemy = (Enemy)poolObject;
                    enemy.SetTarget(GameManager.Instance.Player);
                    _enemyListPool.Add(enemy);
                    break;
                case Enum.PoolObjectTypes.Bullet:
                    _bulletListPool.Add((Bullet)poolObject);
                    break;
                case Enum.PoolObjectTypes.Coin:
                    _coinListPool.Add((Collectables)poolObject);
                    break;
                case Enum.PoolObjectTypes.Xp:
                    _xpListPool.Add((Collectables)poolObject);
                    break;
                case Enum.PoolObjectTypes.Bomb:
                    _bombListPool.Add((Bomb)poolObject);
                    break;
                case Enum.PoolObjectTypes.Audio:
                    _positionalAudios.Add((PositionalAudio)poolObject);
                    break;
            }
        }

        IPoolableObjects GetObjectFromPool(Enum.PoolObjectTypes poolObjectType)
        {
            switch (poolObjectType)
            {
                case Enum.PoolObjectTypes.Enemy:
                    if (_enemyListPool.Count < 2)
                    {
                        var poolObjectData = GetPoolProjectData(Enum.PoolObjectTypes.Enemy);
                        Spawn(poolObjectData.prefab , 10 ,poolObjectData.poolObjectType , poolObjectData.spawnParent);
                    }            
                    var enemy = _enemyListPool[0];
                    _enemyListPool.Remove(enemy);
                    enemy.transform.parent = null;
                    return enemy;
            
                case Enum.PoolObjectTypes.Bullet:
                    if (_bulletListPool.Count < 2)
                    {
                        var poolObjectData = GetPoolProjectData(Enum.PoolObjectTypes.Bullet);
                        Spawn(poolObjectData.prefab , 10 ,poolObjectData.poolObjectType , poolObjectData.spawnParent);
                    }            
                    var bullet = _bulletListPool[0];
                    _bulletListPool.Remove(bullet);
                    bullet.transform.parent = null;
                    return bullet;

                case Enum.PoolObjectTypes.Coin:
                    var coin = GetCollectablesObject(_coinListPool , Enum.PoolObjectTypes.Coin);
                    coin.SetManager(CollectablesManagerHolder.Instance.GetManager(Enum.CollectablesType.Coins));
                    return coin;
                case Enum.PoolObjectTypes.Xp:
                    var xp = GetCollectablesObject(_xpListPool , Enum.PoolObjectTypes.Xp);
                    xp.SetManager(CollectablesManagerHolder.Instance.GetManager(Enum.CollectablesType.Spell));
                    return xp;
                case Enum.PoolObjectTypes.Bomb:
                    if (_bombListPool.Count < 2)
                    {
                        var poolObjectData = GetPoolProjectData(Enum.PoolObjectTypes.Bomb);
                        Spawn(poolObjectData.prefab , 5 ,poolObjectData.poolObjectType , poolObjectData.spawnParent);
                    }            
                    var bomb = _bombListPool[0];
                    _bombListPool.Remove(bomb);
                    bomb.transform.parent = null;
                    return bomb;
                case Enum.PoolObjectTypes.Audio:
                    if (_positionalAudios.Count < 2)
                    {
                        var poolObjectData = GetPoolProjectData(Enum.PoolObjectTypes.Audio);
                        Spawn(poolObjectData.prefab , 10 ,poolObjectData.poolObjectType , poolObjectData.spawnParent);
                    }            
                    var audio = _positionalAudios[0];
                    _positionalAudios.Remove(audio);
                    audio.transform.parent = null;
                    return audio;
                default:
                    return null;
            }
        }

        private Collectables GetCollectablesObject(List<Collectables> collectablesListPool, Enum.PoolObjectTypes collectablesType)
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
            return (Bullet)GetObjectFromPool(Enum.PoolObjectTypes.Bullet);
        }

        public Enemy GetEnemy()
        {
            return (Enemy)GetObjectFromPool(Enum.PoolObjectTypes.Enemy);
        }
        
        public Collectables GetCoins()
        {
            return (Collectables)GetObjectFromPool(Enum.PoolObjectTypes.Coin);
        }
        
        public Collectables GetXp()
        {
            return (Collectables)GetObjectFromPool(Enum.PoolObjectTypes.Xp);
        }

        public Bomb GetBomb()
        {
            return (Bomb)GetObjectFromPool(Enum.PoolObjectTypes.Bomb);
        }

        public PositionalAudio GetAudioPrefab()
        {
            return (PositionalAudio)GetObjectFromPool(Enum.PoolObjectTypes.Audio);

        }
        public void AddBackToList(IPoolableObjects poolable , Enum.PoolObjectTypes poolObjectTypes)
        {
            switch (poolObjectTypes)
            {
                case Enum.PoolObjectTypes.Enemy:
                    var enemy = (Enemy)poolable;
                    ResetObjects(enemy.gameObject , Enum.PoolObjectTypes.Enemy);
                    _enemyListPool.Add(enemy);
                    break;
                case Enum.PoolObjectTypes.Bullet:
                    var bullet = (Bullet)poolable;
                    ResetObjects(bullet.gameObject, Enum.PoolObjectTypes.Bullet);
                    _bulletListPool.Add(bullet);
                    break;
                case Enum.PoolObjectTypes.Coin:
                    var coin = (Collectables)poolable;
                    ResetObjects(coin.gameObject, Enum.PoolObjectTypes.Coin);
                    _coinListPool.Add(coin);
                    break;
                case Enum.PoolObjectTypes.Xp:
                    var xp = (Collectables)poolable;
                    ResetObjects(xp.gameObject, Enum.PoolObjectTypes.Xp);
                    _xpListPool.Add(xp);
                    break;
                case Enum.PoolObjectTypes.Bomb:
                    var bomb = (Bomb)poolable;
                    ResetObjects(bomb.gameObject, Enum.PoolObjectTypes.Bomb);
                    _bombListPool.Add(bomb);
                    break;
                case Enum.PoolObjectTypes.Audio:
                    var audio = (PositionalAudio)poolable;
                    ResetObjects(audio.gameObject, Enum.PoolObjectTypes.Audio);
                    _positionalAudios.Add(audio);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(poolObjectTypes), poolObjectTypes, null);
            }
        }

        private void ResetObjects(GameObject poolObj , Enum.PoolObjectTypes type)
        {
            poolObj.gameObject.SetActive(false);
            poolObj.transform.SetParent(GetPoolProjectData(type).spawnParent);
            poolObj.transform.position = Vector3.zero;
        }

        PoolObjectData GetPoolProjectData(Enum.PoolObjectTypes poolObjectType)
        {
            return poolObjectDataList.FirstOrDefault(poolObjectData => poolObjectData.poolObjectType == poolObjectType);
        }
    }
}