using UnityEngine;

namespace Problem2
{
    public class EquipSystem : MonoBehaviour // can be inventory or any other system which handles the equipment of the player weapons before the game 
    {
        [SerializeField] private WeaponSystem weaponSystem;
        [SerializeField] private WeaponHolder weaponHolder;

        private void UpdateWeapons()
        {
            weaponSystem.SetPrimaryAndSecondaryWeapons(weaponHolder.GetWeaponByWeaponId(WeaponId.AR1) , 
                weaponHolder.GetWeaponByWeaponId(WeaponId.Lmg1) , 
                weaponHolder.GetWeaponByWeaponId(WeaponId.PS1)); // set the weapon whatever the player selects
        }
    }
}