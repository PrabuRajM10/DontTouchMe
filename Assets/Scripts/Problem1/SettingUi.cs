using System;
using UnityEngine;
using UnityEngine.UI;

namespace Problem1
{
    public class SettingUi : BaseUi
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
        private void OnClickOkButton()
        {
            ButtonAnimator.Animate(okButton , () =>
            {
                OnOkButtonPressed?.Invoke();
            });
        }
    }
}