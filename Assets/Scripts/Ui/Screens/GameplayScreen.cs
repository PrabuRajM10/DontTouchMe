using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Gameplay;
using Helpers;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Ui.Screens
{
    public class GameplayScreen : BaseUi
    {
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button infoExitButton;
        [SerializeField] private Button controlsInfoExitButton;
        [SerializeField] private TMP_Text coinsTxt;
        [SerializeField] private TMP_Text timerTxt;
        [SerializeField] private TMP_Text killsTxt;
        [SerializeField] private TMP_Text xpTxt;
        
        [FormerlySerializedAs("cardUi")] [SerializeField] private CardUi cardUiPrefab;

        [SerializeField] private Transform cardUiParent;
        [SerializeField] private Transform info;
        [SerializeField] private Transform board;
        [FormerlySerializedAs("controlInfoBg")] [SerializeField] private Transform controlInfoParent;
        [SerializeField] private Transform controlInfo;
        [FormerlySerializedAs("xpManager")] [SerializeField] private SpellManager spellManager;

        [SerializeField] private PowerCardUi[] powerCardUis;

        private Dictionary<int, CardUi> _cardsDict = new Dictionary<int, CardUi>();

        private int _currentXp;

        public event Action OnPauseButtonPressed;
        bool firstGame = true;
        private void OnEnable()
        {
            pauseButton.onClick.AddListener(OnClickPauseButton);
            infoExitButton.onClick.AddListener(OnClickInfoExitButton);
            controlsInfoExitButton.onClick.AddListener(OnClickControlsInfoExitButton);
            ShowControlInfoAnimation();
        }


        protected override void OnDisable()
        {
            base.OnDisable();
            pauseButton.onClick.RemoveListener(OnClickPauseButton);
            infoExitButton.onClick.RemoveListener(OnClickInfoExitButton);
            controlsInfoExitButton.onClick.RemoveListener(OnClickControlsInfoExitButton);
        }

        private void OnClickControlsInfoExitButton()
        {
            LeanAnimator.Fade(controlInfoParent , 0 , LeanTweenType.easeInCubic , 0.1f , 0 , true);
            LeanAnimator.Scale(controlInfo , Vector3.one, Vector3.zero, LeanTweenType.easeOutExpo , 0.5f , 0 , true , () =>
            {
                controlInfoParent.gameObject.SetActive(false);
                Time.timeScale = 1;
            });
        }

        private void OnClickInfoExitButton()
        {
            LeanAnimator.Fade(info , 0 , LeanTweenType.easeInCubic , 0.1f , 0 , true);
            LeanAnimator.Scale(info , Vector3.one, Vector3.zero, LeanTweenType.easeOutExpo , 0.5f , 0 , true , () =>
            {
                info.gameObject.SetActive(false);
                Time.timeScale = 1;
            });
            LeanAnimator.Fade(board , 1 , LeanTweenType.easeInBack , 0.2f , 0 , true);
        }

        public override void Reset()
        {
            timerTxt.text = "";
            killsTxt.text = "";
            coinsTxt.text = "";
            xpTxt.text = "";
            _currentXp = 0;
            // foreach (var cardUi in _cardsDict)
            // {
            //     cardUi.Value.Reset();
            // }  
        }

        private void OnClickPauseButton()
        {
            LeanAnimator.ButtonOnClick(pauseButton , () =>
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
            SetPowerCardAvailability(index, false);
            // ActivateAllCards(false);
            spellManager.SetCardAvailabilityIfPossible();
            var card = _cardsDict[index];
            card.SetCooldownTimer(cardCooldownTime);
        }

        public void ShowFirstSpellCollectionAnimation()
        {
            info.gameObject.SetActive(true);
            Time.timeScale = 0;
            LeanAnimator.Fade(board , 0 , LeanTweenType.easeInBack , 0.2f , 0 , true);
            LeanAnimator.Fade(info , 1 , LeanTweenType.easeInBack , 0.2f , 0 , true);
            LeanAnimator.Scale(info , Vector3.zero, Vector3.one, LeanTweenType.easeOutElastic , 0.5f , 0 , true);
        }

        private void ShowControlInfoAnimation()
        {
            if(!firstGame)return;
            controlInfoParent.gameObject.SetActive(true);
            Time.timeScale = 0;
            LeanAnimator.Fade(controlInfoParent , 1 , LeanTweenType.easeInBack , 0.2f , 0 , true);
            LeanAnimator.Scale(controlInfo , Vector3.zero, Vector3.one, LeanTweenType.easeOutElastic , 0.5f , 0 , true);
            firstGame = false;
        }
    }
}