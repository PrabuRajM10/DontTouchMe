using System;
using System.Collections.Generic;
using System.Globalization;
using Gameplay;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Ui.Screens
{
    public class GameplayScreen : BaseUi
    {
        [SerializeField] private Button pauseButton;
        [SerializeField] private TMP_Text coinsTxt;
        [SerializeField] private TMP_Text timerTxt;
        [SerializeField] private TMP_Text killsTxt;

        [SerializeField] private PowerCardUi[] powerCardUis;

        public event Action OnPauseButtonPressed;
        private void OnEnable()
        {
            pauseButton.onClick.AddListener(OnClickPauseButton);
        }

        private void OnDisable()
        {
            pauseButton.onClick.RemoveListener(OnClickPauseButton);
        }

        public override void Reset()
        {
            timerTxt.text = "";
            killsTxt.text = "";
            coinsTxt.text = "";
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

        public void UpdateKills(int count)
        {
            killsTxt.text = count.ToString();
        }

        public void SetCardData(List<CardData> powerCards)
        {
            for (var i = 0; i < powerCards.Count; i++)
            {
                var powerCard = powerCards[i];
                // powerCardUis[i].SetData(powerCard);
            }
        }
    }
}