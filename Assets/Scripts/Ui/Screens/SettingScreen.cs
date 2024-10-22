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

        protected override void OnDisable()
        {
            okButton.onClick.RemoveAllListeners();
        }

        private void OnClickOkButton()
        {
            UiAnimator.ButtonOnClick(okButton , () =>
            {
                OnOkButtonPressed?.Invoke();
            });
        }
    }
}