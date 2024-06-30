using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum PoolObjectTypes
{
    Bullet,
    Enemy
}

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance;
    
    [FormerlySerializedAs("_bulletPrefab")] [SerializeField]private GameObject bulletPrefab;
    [FormerlySerializedAs("enemy")] [SerializeField]private GameObject enemyPrefab;

    private Dictionary<PoolObjectTypes, IPoolableObjects> poolsDictionary =
        new Dictionary<PoolObjectTypes, IPoolableObjects>();

    private List<Bullet> _bulletsListPool = new List<Bullet>();
    private List<Enemy> _enemyListPool = new List<Enemy>();
    
    private const int MinBulletCount = 20;
    private const int MinEnemyCount = 20;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this);
        }
        Spawn(bulletPrefab , MinBulletCount , PoolObjectTypes.Bullet);
        Spawn(enemyPrefab , MinEnemyCount , PoolObjectTypes.Enemy);
    }

    private void Spawn(GameObject poolableObjects, int count , PoolObjectTypes poolObjectType)
    {
        for (int i = 0; i < count; i++)
        {
            var poolObject = Instantiate(poolableObjects);
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
            case PoolObjectTypes.Bullet:
                _bulletsListPool.Add((Bullet)poolObject);
                break;
            case PoolObjectTypes.Enemy:
                _enemyListPool.Add((Enemy)poolObject);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(poolObjectType), poolObjectType, null);
        }
    }

    IPoolableObjects GetObjectFromPool(PoolObjectTypes poolObjectType)
    {
        switch (poolObjectType)
        {
            case PoolObjectTypes.Bullet:
                if (_bulletsListPool.Count < 2)
                {
                    Spawn(bulletPrefab , 10 , PoolObjectTypes.Bullet);
                }            
                var bullet = _bulletsListPool[0];
                _bulletsListPool.Remove(bullet);
                bullet.ResetVelocity();
                return bullet;
            
            case PoolObjectTypes.Enemy:
                if (_enemyListPool.Count < 2)
                {
                    Spawn(enemyPrefab , 10 , PoolObjectTypes.Enemy);
                }            
                var enemy = _enemyListPool[0];
                _enemyListPool.Remove(enemy);
                return enemy;
        }
        return null;
    }

    public Bullet GetBullet()
    {
        return (Bullet)GetObjectFromPool(PoolObjectTypes.Bullet);
    }

    public Enemy GetEnemy()
    {
        return (Enemy)GetObjectFromPool(PoolObjectTypes.Enemy);
    }

    public void AddBackToList(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        _bulletsListPool.Add(bullet);
    }
    
}