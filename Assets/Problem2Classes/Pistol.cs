using UnityEngine;

namespace Problem2
{
    public class Pistol : Weapon
    {
        private void Update()
        {
            timeSinceLastShot += Time.deltaTime;
        }

        public override void Shoot(Vector3 target)
        {
            var ammo = Instantiate(base.ammo, muzzlePosition.position, Quaternion.identity);
            ammo.MoveToTarget(target);
            //play sound
            //play particle effect - muzzle flash
        }

        public override void Reload(OnReloadCompleted onReloadCompleted)
        {
            //play reload animation from the animator.
            isReloading = true;
            this.onReloadCompleted = onReloadCompleted;
        }

        public override void GoBackToHolster()
        {
            // play "keeping the gun back in the holster" animation or re-setting the gun's position  
        }

        public override void Equipped()
        {
            //play equipping animation 
            isBeingEquipped = true;
            //enable the weapon
        }

        public void OnReloadDone()
        {
            // function called by animation event after reload animation is done
            isReloading = false;
            onReloadCompleted?.Invoke();
        }

        public void OnEquippingDone()
        {
            // function called by animation event after equipping animation is done
            isBeingEquipped = false;
        }
    }
}