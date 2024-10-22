using System;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens
{
    public class GameResultScreen : BaseUi
    {
        [SerializeField] private TMP_Text wonTitle;
        [SerializeField] private TMP_Text loseTitle;

        [SerializeField] private Button homeButton;
        [SerializeField] private Button retryButton;

        public event Action OnRetryButtonPressed;
        public event Action OnHomeButtonPressed;
        private void OnEnable()
        {
            homeButton.onClick.AddListener(OnClickHomeButton);
            retryButton.onClick.AddListener(OnClickRetryButton);
        }

        protected override void OnDisable()
        {
            homeButton.onClick.RemoveListener(OnClickHomeButton);
            retryButton.onClick.RemoveListener(OnClickRetryButton);
        }

        private void OnClickRetryButton()
        {
            UiAnimator.ButtonOnClick(retryButton , () =>
            {
                OnRetryButtonPressed?.Invoke();
            });
        }

        private void OnClickHomeButton()
        {
            UiAnimator.ButtonOnClick(homeButton , () =>
            {
                OnHomeButtonPressed?.Invoke();
            });
        }

        public void ShowResult(bool successful)
        {
            wonTitle.gameObject.SetActive(successful);
            loseTitle.gameObject.SetActive(!successful);
            retryButton.gameObject.SetActive(!successful);
        }
    }
}