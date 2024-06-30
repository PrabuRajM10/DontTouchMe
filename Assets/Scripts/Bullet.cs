using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour , IPoolableObjects
{
    [SerializeField] private float speed = 100f;
    [SerializeField] private Rigidbody rigidBody;

    private ObjectPooling _pool;

    private void OnValidate()
    {
        if (rigidBody == null) rigidBody = GetComponent<Rigidbody>();
    }

    public void Init(ObjectPooling pool)
    {
        _pool = pool;
    }

    public void SetPositionAndRotation(Transform muzzlePosition)
    {
        transform.position = muzzlePosition.position;
        transform.localRotation = Quaternion.LookRotation(muzzlePosition.forward);
    }

    public void Fire()
    {
        gameObject.SetActive(true);
        rigidBody.AddForce(transform.forward * speed , ForceMode.Force);
        Invoke(nameof(DisableBullet) , 10f);
    }

    private void OnTriggerEnter(Collider other)
    { 
        Debug.Log(" OnTriggerEnter " + other.name);
        if (other.GetComponent<Gun>())
        {
            return;
        }
        DisableBullet();
    }

    private void DisableBullet()
    {
        ResetVelocity();
        gameObject.SetActive(false);
        _pool.AddBackToList(this);
    }

    public void ResetVelocity()
    {
        rigidBody.velocity = Vector3.zero;
    }
        
}