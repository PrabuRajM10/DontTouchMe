using System;
using Enums;
using Managers;
using Ui.Screens;
using UnityEngine;
using Enum = Enums.Enum;

namespace Ui.ScreenHandlers
{
    public class GameResultScreenHandler : BaseScreenHandler
    {
        [SerializeField] private GameResultScreen gameResultScreen;
        
        private void OnEnable()
        {
            gameResultScreen.OnHomeButtonPressed += OnHomeButtonPressed;
            gameResultScreen.OnRetryButtonPressed += OnRetryButtonPressed;
        }


        private void OnDisable()
        {
            gameResultScreen.OnHomeButtonPressed -= OnHomeButtonPressed;
            gameResultScreen.OnRetryButtonPressed -= OnRetryButtonPressed;
        }
        private void OnRetryButtonPressed()
        {
            GameManager.Instance.ChangeState(GameState.CardPicker);
            SwitchScreen(Enum.GameScreen.CardPicker);
        }

        private void OnHomeButtonPressed()
        {
            GameManager.Instance.ChangeState(GameState.Home);
            SwitchScreen(Enum.GameScreen.Home);
        }
    }
}