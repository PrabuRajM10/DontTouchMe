using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Problem1
{
    public class RebindUiManager : MonoBehaviour
    {
        [SerializeField] private InputActionReference movement, jump, fire;
        [SerializeField] private GameObject overlay;
        [SerializeField] private Text overlayText;

        [SerializeField] private Button resetButton;
        [SerializeField] private Button okButton;

        [SerializeField] private List<RebindActionUI>  rebindUi;

        private void OnEnable()
        {
            movement.action.Disable();
            jump.action.Disable();
            fire.action.Disable();
            foreach (var actionUI in rebindUi)
            {
                resetButton.onClick.AddListener(actionUI.ResetToDefault);
            }
        }

        private void Start()
        {
            foreach (var actionUI in rebindUi)
            {
                actionUI.rebindOverlay = overlay;
                actionUI.rebindPrompt = overlayText;
            }
        }

        private void OnDisable()
        {
            movement.action.Enable();
            jump.action.Enable();
            fire.action.Enable();
            resetButton.onClick.RemoveAllListeners();
        }
    }
    
}
