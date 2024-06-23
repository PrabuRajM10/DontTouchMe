using System;
using UnityEngine;

namespace Problem2
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private InputSystem inputSystem;
        [SerializeField] private WeaponSystem weaponSystem;

        private void OnEnable()
        {
            inputSystem.OnPrimaryWeapon1Pressed += OnPrimaryWeapon1Pressed;
            inputSystem.OnPrimaryWeapon2Pressed += OnPrimaryWeapon2Pressed;
            inputSystem.OnSecondaryWeaponPressed += OnSecondaryWeaponPressed;
            inputSystem.OnReloadWeaponPressed += OnReloadWeaponPressed;
        }
        private void OnDisable()
        {
            inputSystem.OnPrimaryWeapon1Pressed -= OnPrimaryWeapon1Pressed;
            inputSystem.OnPrimaryWeapon2Pressed -= OnPrimaryWeapon2Pressed;
            inputSystem.OnSecondaryWeaponPressed -= OnSecondaryWeaponPressed;
            inputSystem.OnReloadWeaponPressed -= OnReloadWeaponPressed;
        }
        private void OnReloadWeaponPressed()
        {
            weaponSystem.ReloadCurrenWeapon();
        }

        private void OnSecondaryWeaponPressed()
        {
            SwitchWeapon(EquippedWeaponType.SecondaryWeapon);
        }

        private void OnPrimaryWeapon2Pressed()
        {
            SwitchWeapon(EquippedWeaponType.PrimaryWeapon2);
        }

        private void OnPrimaryWeapon1Pressed()
        {
            SwitchWeapon(EquippedWeaponType.PrimaryWeapon1);
        }

        void SwitchWeapon(EquippedWeaponType equippedWeaponType)
        {
            weaponSystem.SwitchWeapons(equippedWeaponType);
        }

        private void Update()
        {
            if (inputSystem.IsFireButtonPressed)
            {
                weaponSystem.Fire(transform);
            }
        }
    }
}