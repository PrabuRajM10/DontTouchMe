using System;
using Problem1;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform muzzlePosition;
    [SerializeField] private bool isReloading;
    private float _timeSinceLastShot;
    [SerializeField] private float fireRate = 10;
    private Vector3 _lookTarget;
    [SerializeField]private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        _timeSinceLastShot += Time.deltaTime;
        Debug.Log("[Gun] [Update] InputManager.Instance.MousePosition " + InputManager.Instance.MousePosition);
        _lookTarget.z = 20;
        _lookTarget = _camera.ScreenToWorldPoint(InputManager.Instance.MousePosition);
        _lookTarget.z = 0;
        Debug.DrawLine(transform.position , _lookTarget , Color.blue , 10);
    }

    private void LateUpdate()
    {
        transform.LookAt(_lookTarget);
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