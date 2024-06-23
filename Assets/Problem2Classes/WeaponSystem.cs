using System.Collections.Generic;
using UnityEngine;

namespace Problem2
{
    public class WeaponSystem : MonoBehaviour
    {
        [SerializeField] private UiManager uiManager;

        [SerializeField] private Weapon currentEquippedWeapon;

        private Dictionary<EquippedWeaponType, Weapon> _equippedWeaponTypeDict =
            new Dictionary<EquippedWeaponType, Weapon>();

        private int _currentEquippedWeaponMaxAmmo;
        private int _currentEquippedWeaponCurrentAmmo;
        public void Fire(Transform playerTransform) // player transform can be used for raycast
        {
            if (!currentEquippedWeapon.CanShoot()) return;
            // raycast  
                
            // if player hit

            //uiManager.OnOtherPlayerHit(otherPlayer);  update any changes if needed in ui
                
            //update other systems about hitting other player
                
            // end if
                
            currentEquippedWeapon.Shoot(Vector3.zero); // have to send the position of the other object (can be player or wall) 
            _currentEquippedWeaponCurrentAmmo--;
            
            uiManager.UpdateCurrentWeaponAmmo(_currentEquippedWeaponCurrentAmmo);
                
            if (_currentEquippedWeaponCurrentAmmo <= 0)
            {
                ReloadCurrenWeapon();
            }
        }
        public void SetPrimaryAndSecondaryWeapons(Weapon primary1 , Weapon primary2 , Weapon secondary) // called from inventory or equipment system after player selects the weapons
        {
            _equippedWeaponTypeDict.Clear();
            
            _equippedWeaponTypeDict.Add(EquippedWeaponType.PrimaryWeapon1 , primary1);
            _equippedWeaponTypeDict.Add(EquippedWeaponType.PrimaryWeapon2 , primary2);
            _equippedWeaponTypeDict.Add(EquippedWeaponType.SecondaryWeapon , secondary);

            currentEquippedWeapon = primary1;
            OnWeaponEquipped(currentEquippedWeapon);
        }

        public void SwitchWeapons(EquippedWeaponType equippedWeaponType)
        {
            var newWeapon = _equippedWeaponTypeDict[equippedWeaponType];
            if(newWeapon == currentEquippedWeapon) return;
            
            currentEquippedWeapon.GoBackToHolster();

            currentEquippedWeapon = newWeapon;
            
            currentEquippedWeapon.Equipped();
            OnWeaponEquipped(currentEquippedWeapon);
        }
        
        void OnWeaponEquipped(Weapon weapon)
        {
            _currentEquippedWeaponMaxAmmo = weapon.MaxAmmoCount;
            _currentEquippedWeaponCurrentAmmo = weapon.MaxAmmoCount;
            uiManager.ShowCurrentWeaponData(weapon.WeaponName , weapon.WeaponImage , _currentEquippedWeaponMaxAmmo);
         }

        public void ReloadCurrenWeapon()
        {
            if(_currentEquippedWeaponCurrentAmmo >= _currentEquippedWeaponMaxAmmo) return;
            currentEquippedWeapon.Reload(OnReloadCompletedCallback); // can also show some ui to indicate that weapon is reloading.
        }
        private void OnReloadCompletedCallback() // called after reload animation is done 
        {
            _currentEquippedWeaponCurrentAmmo = _currentEquippedWeaponMaxAmmo;
            uiManager.UpdateCurrentWeaponAmmo(_currentEquippedWeaponCurrentAmmo);
        }
    }


    public enum EquippedWeaponType
    {
        PrimaryWeapon1,
        PrimaryWeapon2,
        SecondaryWeapon
    }
}