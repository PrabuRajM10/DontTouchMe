using System;
using UnityEngine;
using UnityEngine.UI;

namespace Problem2
{
    public enum WeaponType
    {
        Primary,
        Secondary
    }
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected WeaponType weaponType;
        [SerializeField] protected WeaponId weaponId;

        [SerializeField] protected string weaponName;
        [SerializeField] protected Sprite image;
        [SerializeField] protected int maxAmmoCount;
        [SerializeField] protected float fireRate;
        [SerializeField] protected float maxDistance;
        [SerializeField] protected Ammo ammo;
        [SerializeField] protected AudioClip sound;
        [SerializeField] protected ParticleSystem muzzleFlash;
        [SerializeField] protected Animator animator;
        [SerializeField] protected Transform muzzlePosition;
        

        protected bool isReloading;
        protected bool isBeingEquipped;
        protected float timeSinceLastShot;

        public int MaxAmmoCount => maxAmmoCount;
        public string WeaponName => weaponName;
        public Sprite WeaponImage => image;
        public WeaponId WeaponId => weaponId;
        public abstract void Shoot(Vector3 target);
        public abstract void Reload(OnReloadCompleted onReloadCompleted);
        public abstract void GoBackToHolster();
        public abstract void Equipped();
        
        public delegate void OnReloadCompleted();

        public OnReloadCompleted onReloadCompleted;
        
        public bool CanShoot() => !isReloading && timeSinceLastShot > 1f / (fireRate / 60f) && !isBeingEquipped;
    }
}
