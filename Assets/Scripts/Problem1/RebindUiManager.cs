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

        // public void RebindStop(RebindActionUI actionUI,string s1 , string s2 , string s3)
        // {
        //     Debug.LogFormat("[] [] s1 {0} s2 {1} s3 {2} " , s1 , s2 , s3);
        // }
        
        public void RebindStop(RebindActionUI actionUI,InputActionRebindingExtensions.RebindingOperation operation)
        {
            Debug.LogFormat("[] [RebindStop] s1 {0}" , operation.OnComplete(
                delegate(InputActionRebindingExtensions.RebindingOperation rebindingOperation)
                {
                    Debug.Log("rebind compplete");
                }));
        }
    }
    
}
