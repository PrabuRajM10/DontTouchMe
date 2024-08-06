using System;
using System.Globalization;
using Gameplay;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Ui.Screens
{
    public class GameplayUi : BaseUi
    {
        [SerializeField] private Button pauseButton;
        [SerializeField] private TMP_Text coinsTxt;
        [SerializeField] private TMP_Text timerTxt;

        public event Action OnPauseButtonPressed;
        private void OnEnable()
        {
            pauseButton.onClick.AddListener(OnClickPauseButton);
        }

        private void OnDisable()
        {
            pauseButton.onClick.RemoveListener(OnClickPauseButton);
        }

        private void OnClickPauseButton()
        {
            ButtonAnimator.Animate(pauseButton , () =>
            {
                OnPauseButtonPressed?.Invoke();
            });
        }

        public void UpdateTimer(string time)
        {
            timerTxt.text = time;
        }

        public void UpdateScore(int score)
        {
            coinsTxt.text = score.ToString();
        }
    }
}