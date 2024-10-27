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
        [SerializeField] private PowerCardsHandler powerCardsHandler;
        private void OnEnable()
        {
            cardPickerScreen.OnGetCardsButtonPressed += GetCardsButtonPressed;
            cardPickerScreen.OnNextButtonPressed += OnNextButtonPressed;
            cardPickerScreen.OnBackButtonPressed += OnBackButtonPressed;
        }



        private void OnDisable()
        {
            cardPickerScreen.OnGetCardsButtonPressed -= GetCardsButtonPressed;
            cardPickerScreen.OnNextButtonPressed -= OnNextButtonPressed;
            cardPickerScreen.OnBackButtonPressed -= OnBackButtonPressed;
        }
        private void OnBackButtonPressed()
        {
            GameManager.Instance.ChangeState(GameState.Home);
            SwitchScreen(GameScreen.Home);
            
        }
        private void OnNextButtonPressed()
        {
            GameManager.Instance.ChangeState(GameState.Gameplay);
            SwitchScreen(GameScreen.Gameplay);
        }

        private void GetCardsButtonPressed()
        {
            List<CardData> pickedCard = PowerCardPicker.Instance.GetPowerCards();
            cardPickerScreen.OnCardsSelected(pickedCard);

            PowerCard[] powerCards = { pickedCard[0].powerCard , pickedCard[1].powerCard , pickedCard[2].powerCard};
            
            powerCardsHandler.SetChosenCurrentCard(powerCards);
        }
    }
}