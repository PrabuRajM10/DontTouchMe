using UnityEngine;
using UnityEngine.InputSystem;

namespace Problem1
{
    public class RebindUiManager : MonoBehaviour
    {
        [SerializeField] private InputActionReference movement, jump, fire;

        [SerializeField] private GameObject rebindOverlay;

        private void OnEnable()
        {
            movement.action.Enable();
            jump.action.Enable();
            fire.action.Enable();
        }

        private void OnDisable()
        {
            movement.action.Disable();
            jump.action.Disable();
            fire.action.Disable();
        }
    }
}
