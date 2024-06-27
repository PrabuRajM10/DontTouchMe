using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 100f;
    [SerializeField] private Rigidbody rigidBody;

    private void OnValidate()
    {
        if (rigidBody == null) rigidBody = GetComponent<Rigidbody>();
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
        // Debug.Log(" OnTriggerEnter " + other.name);
        if (other.GetComponent<Gun>())
        {
            return;
        }
        DisableBullet();
    }

    private void DisableBullet()
    {
        rigidBody.velocity = Vector3.zero;
        gameObject.SetActive(false);
        ObjectPooling.AddBackToList(this);
    }
        
}