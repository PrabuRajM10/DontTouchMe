using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform muzzlePosition;
    [SerializeField] private bool isReloading;
    private float _timeSinceLastShot;
    [SerializeField] private float fireRate = 10;
    private Vector3 _lookTarget;

    private void Update()
    {
        _timeSinceLastShot += Time.deltaTime;
    }

    bool CanShoot() => !isReloading && _timeSinceLastShot > 1f / (fireRate / 60f);

    public void Shoot()
    {
        if(!CanShoot())return;
        var bullet = ObjectPooling.GetBullet();
        bullet.SetPositionAndRotation(muzzlePosition);
        bullet.Fire();
        _timeSinceLastShot = 0;
    }
}