using System;
using System.Collections.Generic;
using Gameplay;
using Helpers;
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
        
        [SerializeField] private PowerCardUi powerCardUi;

        public event Action OnGetCardsButtonPressed; 
        public event Action OnNextButtonPressed; 

        private void OnEnable()
        {
            getCardsButton.onClick.AddListener(OnClickGetCardsButton);
            nextButton.onClick.AddListener(OnClickNextButton);
        }


        protected override void OnDisable()
        {
            base.OnDisable();
            getCardsButton.onClick.RemoveListener(OnClickGetCardsButton);
            nextButton.onClick.RemoveListener(OnClickNextButton);
        }
        private void OnClickNextButton()
        {
            ButtonAnimator.Animate(nextButton, () =>
            {
                OnNextButtonPressed?.Invoke();
            });
        }

        private void OnClickGetCardsButton()
        {
            ButtonAnimator.Animate(getCardsButton, () =>
            {
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
        }

        public void OnCardsSelected(List<CardData> pickedCard)
        {
            for (int i = 0; i < pickedCard.Count; i++)
            {
                var card = Instantiate(powerCardUi, cardsViewContentParent);
                card.SetData(pickedCard[i]); ;
            }
            nextButton.gameObject.SetActive(true);
        }
    }
}