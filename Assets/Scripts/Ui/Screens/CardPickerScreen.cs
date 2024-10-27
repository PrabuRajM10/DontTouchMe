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
        [SerializeField] private Button backButton;
        
        [SerializeField] private Transform cardsViewContentParent;
        [SerializeField] private Transform info;
        [SerializeField] private Transform getCardBtnStartPos;
        [SerializeField] private Transform getCardBtnEndPos;
        [SerializeField] private Transform nextBtnStartPos;
        [SerializeField] private Transform nextBtnEndPos;
        [SerializeField] private Transform backBtnStartPos;
        [SerializeField] private Transform backBtnEndPos;

        [SerializeField] private PowerCardUi powerCardUi;

        [SerializeField] private Transform[] cardsStartPositions;
        [SerializeField] private Transform[] cardsEndPositions;

        private List<PowerCardUi> _cardsList = new List<PowerCardUi>();

        public event Action OnGetCardsButtonPressed; 
        public event Action OnNextButtonPressed; 
        public event Action OnBackButtonPressed; 

        private void OnEnable()
        {
            getCardsButton.onClick.AddListener(OnClickGetCardsButton);
            nextButton.onClick.AddListener(OnClickNextButton);
            backButton.onClick.AddListener(OnClickBackButton);
            
            LeanAnimator.Move(getCardsButton.transform , getCardBtnEndPos , LeanTweenType.easeOutElastic);
            LeanAnimator.Move(backButton.transform , backBtnEndPos , LeanTweenType.easeOutElastic);
            LeanAnimator.Scale(info , Vector3.zero, Vector3.one, LeanTweenType.easeOutBack , 0.8f);
            LeanAnimator.Fade(info , 1 , LeanTweenType.linear , 0.5f);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            getCardsButton.onClick.RemoveListener(OnClickGetCardsButton);
            nextButton.onClick.RemoveListener(OnClickNextButton);
            backButton.onClick.RemoveListener(OnClickBackButton);
            getCardsButton.interactable = true;
        }
        private void OnClickBackButton()
        {
            LeanAnimator.ButtonOnClick(backButton , () =>
            {
                DisableInfoAnimation();
                LeanAnimator.Move(getCardsButton.transform , getCardBtnStartPos , LeanTweenType.easeOutExpo , 0.6f);
                // LeanAnimator.Move(nextBtnEndPos.transform, nextBtnStartPos , LeanTweenType.easeOutExpo , 0.6f);
                LeanAnimator.Move(backButton.transform , backBtnStartPos , LeanTweenType.easeOutExpo , 0.6f,0,false,() =>
                {
                    OnBackButtonPressed?.Invoke();
                });
            });
        }
        private void OnClickNextButton()
        {
            MoveCardsUp();

            LeanAnimator.ButtonOnClick(nextButton, () =>
            {
                LeanAnimator.Move(nextButton.transform , nextBtnStartPos , LeanTweenType.easeOutExpo , 0.6f,0,false,() =>
                {
                    OnNextButtonPressed?.Invoke();
                });
            });
        }

        private void MoveCardsUp()
        {
            for (var i = 0; i < _cardsList.Count; i++)
            {
                var cardUi = _cardsList[i];
                LeanAnimator.Move(cardUi.transform, cardsStartPositions[i], LeanTweenType.easeOutExpo, 0.6f);
            }
        }

        private void OnClickGetCardsButton()
        {
            LeanAnimator.ButtonOnClick(getCardsButton, () =>
            {
                DisableInfoAnimation();
                LeanAnimator.Move(getCardsButton.transform , getCardBtnStartPos , LeanTweenType.easeOutExpo , 0.6f);
                OnGetCardsButtonPressed?.Invoke();
                getCardsButton.interactable = false;
            });
        }

        private void DisableInfoAnimation()
        {
            LeanAnimator.Scale(info, Vector3.one, Vector3.zero, LeanTweenType.easeOutBack, 0.8f);
            LeanAnimator.Fade(info, 0, LeanTweenType.linear, 0.5f);
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
            LeanAnimator.Move(backButton.transform, backBtnStartPos, LeanTweenType.easeOutExpo , 0.6f);
        }
    }
}