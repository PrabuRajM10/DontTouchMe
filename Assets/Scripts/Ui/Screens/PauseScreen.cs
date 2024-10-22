using System;
using Helpers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Ui.Screens
{
    public class PauseScreen : BaseUi
    {
        [SerializeField] private Button resumeButton;
        [FormerlySerializedAs("mainMenuButton")] [SerializeField] private Button homeButton;
        [SerializeField] private Button quitButton;

        public event Action OnResumeButtonPressed;
        public event Action OnHomeButtonPressed;
        public event Action OnQuitButtonPressed;

        private void OnEnable()
        {
            resumeButton.onClick.AddListener(OnClickResumeButton);
            homeButton.onClick.AddListener(OnClickHomeButton);
            quitButton.onClick.AddListener(OnClickQuitButton);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            resumeButton.onClick.RemoveListener(OnClickResumeButton);
            homeButton.onClick.RemoveListener(OnClickHomeButton);
            quitButton.onClick.RemoveListener(OnClickQuitButton);
        }

        private void OnClickQuitButton()
        {
            UiAnimator.ButtonOnClick(quitButton, () =>
            {
                OnQuitButtonPressed?.Invoke();
            });
        }

        private void OnClickHomeButton()
        {
            UiAnimator.ButtonOnClick(homeButton, () =>
            {
                OnHomeButtonPressed?.Invoke();
            });
        }

        private void OnClickResumeButton()
        {
            UiAnimator.ButtonOnClick(resumeButton, () =>
            {
                OnResumeButtonPressed?.Invoke();
            });
        }
    }
}