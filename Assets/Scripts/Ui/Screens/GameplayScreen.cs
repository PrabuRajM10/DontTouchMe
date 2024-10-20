using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Gameplay;
using Helpers;
using Managers;
using Problem2;
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
        [SerializeField] private TMP_Text xpTxt;
        
        [FormerlySerializedAs("cardUi")] [SerializeField] private CardUi cardUiPrefab;

        [SerializeField] private Transform cardUiParent;
        [SerializeField] private XpManager xpManager;

        [SerializeField] private PowerCardUi[] powerCardUis;

        private Dictionary<int, CardUi> _cardsDict = new Dictionary<int, CardUi>();

        private int _currentXp;

        public event Action OnPauseButtonPressed;
        private void OnEnable()
        {
            pauseButton.onClick.AddListener(OnClickPauseButton);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            pauseButton.onClick.RemoveListener(OnClickPauseButton);
        }

        public override void Reset()
        {
            timerTxt.text = "";
            killsTxt.text = "";
            coinsTxt.text = "";
            xpTxt.text = "";
            _currentXp = 0;
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
        
        public void UpdateXp(int count)
        {
            _currentXp = count;
            xpTxt.text = _currentXp.ToString();
        }

        public void SetCardData(List<CardData> powerCards)
        {
            for (var i = 0; i < powerCards.Count; i++)
            {
                var powerCard = powerCards[i];
                if (_cardsDict.Count < powerCards.Count)
                {
                    var cardUi = Instantiate(cardUiPrefab, cardUiParent);
                    _cardsDict.Add(i+1,cardUi);
                }
                _cardsDict[i+1].SetData(powerCard.cardName , powerCard.cardImg , powerCard.powerCard.XpCost);
            }
        }

        public void SetPowerCardAvailability(int index, bool state)
        {
            _cardsDict[index].SetAvailability(state);
        }

        void ActivateAllCards(bool state)
        {
            foreach (var cardUi in _cardsDict)
            {
                cardUi.Value.SetAvailability(state);
            }
        }

        public void OnPowerCardActivated(int index, float activeTime, int xpCost)
        {
            UpdateXp(_currentXp - xpCost);
            ActivateAllCards(false);
            var card = _cardsDict[index];
            card.SetAvailability(true);
            card.SetActiveTimer(activeTime);            
        }

        public void SetCardOnCooldown(int index, float cardCooldownTime)
        {
            xpManager.SetCardAvailabilityIfPossible();
            // ActivateAllCards(true);
            var card = _cardsDict[index];
            card.SetCooldownTimer(cardCooldownTime);
        }
    }
}