using System;
using UnityEngine;

namespace Problem2
{
    public class InputSystem : MonoBehaviour
    {
        private bool _isFireButtonPressed;

        public bool IsFireButtonPressed => _isFireButtonPressed;

        public event Action OnPrimaryWeapon1Pressed;
        public event Action OnPrimaryWeapon2Pressed;
        public event Action OnSecondaryWeaponPressed;
        public event Action OnReloadWeaponPressed;
        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _isFireButtonPressed = true;
            }
            if (Input.GetButtonUp("Fire1"))
            {
                _isFireButtonPressed = false;
            }
            if (Input.GetKeyUp(KeyCode.R))
            {
                OnReloadWeaponPressed?.Invoke();
            }
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                OnPrimaryWeapon1Pressed?.Invoke();
            }
            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                OnPrimaryWeapon2Pressed?.Invoke();
            }
            if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                OnSecondaryWeaponPressed?.Invoke();
            }
        }
    }
}