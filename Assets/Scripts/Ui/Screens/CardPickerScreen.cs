using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gameplay;
using Helpers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Ui.Screens
{
    public class CardPickerScreen : BaseUi
    {
        [SerializeField] private Button getCardsButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Transform cardsViewContentParent;
        [SerializeField] private Transform getCardBtnStartPos;
        [SerializeField] private Transform getCardBtnEndPos;
        [SerializeField] private Transform nextBtnStartPos;
        [SerializeField] private Transform nextBtnEndPos;
        
        [SerializeField] private PowerCardUi powerCardUi;

        [SerializeField] private Transform[] cardsStartPositions;
        [SerializeField] private Transform[] cardsEndPositions;

        private List<PowerCardUi> _cardsList = new List<PowerCardUi>();

        public event Action OnGetCardsButtonPressed; 
        public event Action OnNextButtonPressed; 

        private void OnEnable()
        {
            getCardsButton.onClick.AddListener(OnClickGetCardsButton);
            nextButton.onClick.AddListener(OnClickNextButton);
            
            LeanAnimator.Move(getCardsButton.transform , getCardBtnEndPos , LeanTweenType.easeOutElastic);
        }


        protected override void OnDisable()
        {
            base.OnDisable();
            getCardsButton.onClick.RemoveListener(OnClickGetCardsButton);
            nextButton.onClick.RemoveListener(OnClickNextButton);
            getCardsButton.interactable = true;
        }
        private void OnClickNextButton()
        {
            for (var i = 0; i < _cardsList.Count; i++)
            {
                var cardUi = _cardsList[i];
                LeanAnimator.Move(cardUi.transform, cardsStartPositions[i], LeanTweenType.easeOutExpo, 0.6f);
            }

            LeanAnimator.ButtonOnClick(nextButton, () =>
            {
                LeanAnimator.Move(nextButton.transform , nextBtnStartPos , LeanTweenType.easeOutExpo , 0.6f,0,false,() =>
                {
                    OnNextButtonPressed?.Invoke();
                });
            });
        }

        private void OnClickGetCardsButton()
        {
            LeanAnimator.ButtonOnClick(getCardsButton, () =>
            {
                LeanAnimator.Move(getCardsButton.transform , getCardBtnStartPos , LeanTweenType.easeOutExpo , 0.6f);
                OnGetCardsButtonPressed?.Invoke();
                getCardsButton.interactable = false;
            });
        }

        public override void Reset()
        {
            nextButton.gameObject.SetActive(false);
            for (int i = cardsViewContentParent.childCount-1 ; i >= 0; i--)
            {
                Destroy(cardsViewContentParent.GetChild(i).gameObject);
            }
            _cardsList.Clear();
        }

        public async void OnCardsSelected(List<CardData> pickedCard)
        {
            for (int i = 0; i < pickedCard.Count; i++)
            {
                var card = Instantiate(powerCardUi,cardsStartPositions[i].position , quaternion.identity, cardsViewContentParent);
                card.SetData(pickedCard[i] , cardsEndPositions[i]);
                _cardsList.Add(card);
                await Task.Delay(100);
            }
            nextButton.gameObject.SetActive(true);
            LeanAnimator.Move(nextButton.transform , nextBtnEndPos , LeanTweenType.easeOutElastic);
        }
    }
}