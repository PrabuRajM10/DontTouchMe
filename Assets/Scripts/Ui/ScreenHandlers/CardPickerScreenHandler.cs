using System;
using System.Collections.Generic;
using Gameplay;
using Managers;
using Ui.Screens;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ui.ScreenHandlers
{
    public class CardPickerScreenHandler : BaseScreenHandler
    {
        [FormerlySerializedAs("cardPickerUi")] [SerializeField] private CardPickerScreen cardPickerScreen;
        private void OnEnable()
        {
            cardPickerScreen.OnGetCardsButtonPressed += GetCardsButtonPressed;
            cardPickerScreen.OnNextButtonPressed += OnNextButtonPressed;
        }


        private void OnDisable()
        {
            cardPickerScreen.OnGetCardsButtonPressed -= GetCardsButtonPressed;
            cardPickerScreen.OnNextButtonPressed -= OnNextButtonPressed;
        }
        private void OnNextButtonPressed()
        {
            GameManager.Instance.ChangeState(GameState.Gameplay);
            SwitchScreen(GameScreen.Gameplay);
        }

        private void GetCardsButtonPressed()
        {
            List<CardData> pickedCard = PowerCardManager.Instance.GetPowerCards();
            cardPickerScreen.ShowSelectedCards(pickedCard);
        }
    }
}