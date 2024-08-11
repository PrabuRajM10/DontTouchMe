using System;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens
{
    public class SettingScreen : BaseUi
    {
        [SerializeField] private Button okButton;

        public event Action OnOkButtonPressed; 

        private void OnEnable()
        {
            okButton.onClick.AddListener(OnClickOkButton);
        }
        private void OnDisable()
        {
            okButton.onClick.RemoveAllListeners();
        }

        public override void Reset()
        {
            
        }

        private void OnClickOkButton()
        {
            ButtonAnimator.Animate(okButton , () =>
            {
                OnOkButtonPressed?.Invoke();
            });
        }
    }
}