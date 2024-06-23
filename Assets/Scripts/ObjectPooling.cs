using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private static Bullet _bulletPrefab;
    private static List<Bullet> _bulletsList = new List<Bullet>();
    private const int TotalBulletCount = 20;

    public static void Init(Bullet bulletPrefab)
    {
        _bulletPrefab = bulletPrefab;
        SpawnBullets(TotalBulletCount);
    }

    static void SpawnBullets(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var bullet = Instantiate(_bulletPrefab);
            bullet.gameObject.SetActive(false);
            _bulletsList.Add(bullet);
        }
    }

    public static Bullet GetBullet()
    {
        if (_bulletsList.Count < 2)
        {
            SpawnBullets(5);
        }            
        var bullet = _bulletsList[0];
        _bulletsList.Remove(bullet);

        return bullet;
    }

    public static void AddBackToList(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        _bulletsList.Add(bullet);
    }
    
}