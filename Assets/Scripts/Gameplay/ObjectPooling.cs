using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    

    [Serializable]
    public class PoolObjectData
    {
        public GameObject prefab;
        public Transform spawnParent;
        public int initialSpawnCount;
        public DTMEnum.PoolObjectTypes poolObjectType;
    }

    public class ObjectPooling : GenericSingleton<ObjectPooling>
    {
    
        [SerializeField] private List<PoolObjectData> poolObjectDataList;

        private List<Enemy> _enemyListPool = new List<Enemy>();
        private List<Bullet> _bulletListPool = new List<Bullet>();
        private List<Bomb> _bombListPool = new List<Bomb>();
        private List<Collectables> _coinListPool = new List<Collectables>();
        private List<Collectables> _xpListPool = new List<Collectables>();
        private List<PositionalAudio> _positionalAudiosPool = new List<PositionalAudio>();
        private List<InGameParticles> _bulletHitParticlePool = new List<InGameParticles>();
        private List<InGameParticles> _enemyHitParticlePool = new List<InGameParticles>();

        private void Start()
        {
            foreach (var poolObjectData in poolObjectDataList)
            {
                Spawn(poolObjectData.prefab , poolObjectData.initialSpawnCount , poolObjectData.poolObjectType , poolObjectData.spawnParent);
            }
            // Spawn(bulletPrefab , minBulletCount , PoolObjectTypes.Bullet , bulletSpawnParent);
            // Spawn(enemyPrefab , minEnemyCount , PoolObjectTypes.Enemy , enemySpawnParent);k
        }

        private void Spawn(GameObject poolableObjects, int count , DTMEnum.PoolObjectTypes poolObjectType , Transform parent)
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

        private void AddObjectToPool(DTMEnum.PoolObjectTypes poolObjectType, IPoolableObjects poolObject)
        {

            switch (poolObjectType)
            {
                case DTMEnum.PoolObjectTypes.Enemy:
                    var enemy = (Enemy)poolObject;
                    enemy.SetTarget(GameManager.Instance.Player);
                    _enemyListPool.Add(enemy);
                    break;
                case DTMEnum.PoolObjectTypes.Bullet:
                    _bulletListPool.Add((Bullet)poolObject);
                    break;
                case DTMEnum.PoolObjectTypes.Coin:
                    _coinListPool.Add((Collectables)poolObject);
                    break;
                case DTMEnum.PoolObjectTypes.Spell:
                    _xpListPool.Add((Collectables)poolObject);
                    break;
                case DTMEnum.PoolObjectTypes.Bomb:
                    _bombListPool.Add((Bomb)poolObject);
                    break;
                case DTMEnum.PoolObjectTypes.Audio:
                    _positionalAudiosPool.Add((PositionalAudio)poolObject);
                    break;
                case DTMEnum.PoolObjectTypes.BulletHitParticle:
                    _bulletHitParticlePool.Add((InGameParticles)poolObject);
                    break;
                case DTMEnum.PoolObjectTypes.EnemyHitParticle:
                    _enemyHitParticlePool.Add((InGameParticles)poolObject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(poolObjectType), poolObjectType, null);
            }
        }

        IPoolableObjects GetObjectFromPool(DTMEnum.PoolObjectTypes poolObjectType)
        {
            switch (poolObjectType)
            {
                case DTMEnum.PoolObjectTypes.Enemy:
                    if (_enemyListPool.Count < 2)
                    {
                        var poolObjectData = GetPoolProjectData(DTMEnum.PoolObjectTypes.Enemy);
                        Spawn(poolObjectData.prefab , 10 ,poolObjectData.poolObjectType , poolObjectData.spawnParent);
                    }            
                    var enemy = _enemyListPool[0];
                    _enemyListPool.Remove(enemy);
                    enemy.transform.parent = null;
                    return enemy;
            
                case DTMEnum.PoolObjectTypes.Bullet:
                    if (_bulletListPool.Count < 2)
                    {
                        var poolObjectData = GetPoolProjectData(DTMEnum.PoolObjectTypes.Bullet);
                        Spawn(poolObjectData.prefab , 10 ,poolObjectData.poolObjectType , poolObjectData.spawnParent);
                    }            
                    var bullet = _bulletListPool[0];
                    _bulletListPool.Remove(bullet);
                    bullet.transform.parent = null;
                    return bullet;

                case DTMEnum.PoolObjectTypes.Coin:
                    var coin = GetCollectablesObject(_coinListPool , DTMEnum.PoolObjectTypes.Coin);
                    coin.SetManager(CollectablesManagerHolder.Instance.GetManager(DTMEnum.CollectablesType.Coins));
                    return coin;
                case DTMEnum.PoolObjectTypes.Spell:
                    var xp = GetCollectablesObject(_xpListPool , DTMEnum.PoolObjectTypes.Spell);
                    xp.SetManager(CollectablesManagerHolder.Instance.GetManager(DTMEnum.CollectablesType.Spell));
                    return xp;
                case DTMEnum.PoolObjectTypes.Bomb:
                    if (_bombListPool.Count < 2)
                    {
                        var poolObjectData = GetPoolProjectData(DTMEnum.PoolObjectTypes.Bomb);
                        Spawn(poolObjectData.prefab , 5 ,poolObjectData.poolObjectType , poolObjectData.spawnParent);
                    }            
                    var bomb = _bombListPool[0];
                    _bombListPool.Remove(bomb);
                    bomb.transform.parent = null;
                    return bomb;
                case DTMEnum.PoolObjectTypes.Audio:
                    if (_positionalAudiosPool.Count < 2)
                    {
                        var poolObjectData = GetPoolProjectData(DTMEnum.PoolObjectTypes.Audio);
                        Spawn(poolObjectData.prefab , 10 ,poolObjectData.poolObjectType , poolObjectData.spawnParent);
                    }            
                    var audio = _positionalAudiosPool[0];
                    _positionalAudiosPool.Remove(audio);
                    audio.transform.parent = null;
                    return audio;
                case DTMEnum.PoolObjectTypes.BulletHitParticle:
                    return GetParticleObject(_bulletHitParticlePool , DTMEnum.PoolObjectTypes.BulletHitParticle);
                case DTMEnum.PoolObjectTypes.EnemyHitParticle:
                    return GetParticleObject(_enemyHitParticlePool , DTMEnum.PoolObjectTypes.EnemyHitParticle);
                default:
                    return null;
            }
        }

        private IPoolableObjects GetParticleObject(List<InGameParticles> poolList , DTMEnum.PoolObjectTypes particlePoolType)
        {
            if (poolList.Count < 2)
            {
                var poolObjectData = GetPoolProjectData(particlePoolType);
                Spawn(poolObjectData.prefab, 10, poolObjectData.poolObjectType, poolObjectData.spawnParent);
            }

            var bulletHit = poolList[0];
            poolList.Remove(bulletHit);
            bulletHit.transform.parent = null;
            return bulletHit;
        }

        private Collectables GetCollectablesObject(List<Collectables> collectablesListPool, DTMEnum.PoolObjectTypes collectablesType)
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
            return (Bullet)GetObjectFromPool(DTMEnum.PoolObjectTypes.Bullet);
        }

        public Enemy GetEnemy()
        {
            return (Enemy)GetObjectFromPool(DTMEnum.PoolObjectTypes.Enemy);
        }
        
        public Collectables GetCoins()
        {
            return (Collectables)GetObjectFromPool(DTMEnum.PoolObjectTypes.Coin);
        }
        
        public Collectables GetXp()
        {
            return (Collectables)GetObjectFromPool(DTMEnum.PoolObjectTypes.Spell);
        }

        public Bomb GetBomb()
        {
            return (Bomb)GetObjectFromPool(DTMEnum.PoolObjectTypes.Bomb);
        }

        public PositionalAudio GetAudioPrefab()
        {
            return (PositionalAudio)GetObjectFromPool(DTMEnum.PoolObjectTypes.Audio);
        }

        public InGameParticles GetBulletHit()
        {
            return (InGameParticles)GetObjectFromPool(DTMEnum.PoolObjectTypes.BulletHitParticle);
        }
        
        public InGameParticles GetEnemyHit()
        {
            return (InGameParticles)GetObjectFromPool(DTMEnum.PoolObjectTypes.EnemyHitParticle);
        }
        public void AddBackToList(IPoolableObjects poolable , DTMEnum.PoolObjectTypes poolObjectTypes)
        {
            switch (poolObjectTypes)
            {
                case DTMEnum.PoolObjectTypes.Enemy:
                    var enemy = (Enemy)poolable;
                    ResetObjects(enemy.gameObject , DTMEnum.PoolObjectTypes.Enemy);
                    _enemyListPool.Add(enemy);
                    break;
                case DTMEnum.PoolObjectTypes.Bullet:
                    var bullet = (Bullet)poolable;
                    ResetObjects(bullet.gameObject, DTMEnum.PoolObjectTypes.Bullet);
                    _bulletListPool.Add(bullet);
                    break;
                case DTMEnum.PoolObjectTypes.Coin:
                    var coin = (Collectables)poolable;
                    ResetObjects(coin.gameObject, DTMEnum.PoolObjectTypes.Coin);
                    _coinListPool.Add(coin);
                    break;
                case DTMEnum.PoolObjectTypes.Spell:
                    var xp = (Collectables)poolable;
                    ResetObjects(xp.gameObject, DTMEnum.PoolObjectTypes.Spell);
                    _xpListPool.Add(xp);
                    break;
                case DTMEnum.PoolObjectTypes.Bomb:
                    var bomb = (Bomb)poolable;
                    ResetObjects(bomb.gameObject, DTMEnum.PoolObjectTypes.Bomb);
                    _bombListPool.Add(bomb);
                    break;
                case DTMEnum.PoolObjectTypes.Audio:
                    var audio = (PositionalAudio)poolable;
                    ResetObjects(audio.gameObject, DTMEnum.PoolObjectTypes.Audio);
                    _positionalAudiosPool.Add(audio);
                    break;
                case DTMEnum.PoolObjectTypes.BulletHitParticle:
                    var bulletHit = (InGameParticles)poolable;
                    ResetObjects(bulletHit.gameObject, DTMEnum.PoolObjectTypes.BulletHitParticle);
                    _bulletHitParticlePool.Add(bulletHit);
                    break;
                case DTMEnum.PoolObjectTypes.EnemyHitParticle:
                    var enemyHit = (InGameParticles)poolable;
                    ResetObjects(enemyHit.gameObject, DTMEnum.PoolObjectTypes.EnemyHitParticle);
                    _enemyHitParticlePool.Add(enemyHit);  break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(poolObjectTypes), poolObjectTypes, null);
            }
        }

        private void ResetObjects(GameObject poolObj , DTMEnum.PoolObjectTypes type)
        {
            poolObj.gameObject.SetActive(false);
            poolObj.transform.SetParent(GetPoolProjectData(type).spawnParent);
            poolObj.transform.position = Vector3.zero;
        }

        PoolObjectData GetPoolProjectData(DTMEnum.PoolObjectTypes poolObjectType)
        {
            return poolObjectDataList.FirstOrDefault(poolObjectData => poolObjectData.poolObjectType == poolObjectType);
        }
    }
}