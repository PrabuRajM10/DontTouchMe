using System;
using Managers;
using Ui.Screens;
using UnityEngine;

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
            GameManager.Instance.ChangeState(GameState.Gameplay);
            SwitchScreen(GameScreen.Gameplay);
        }

        private void OnHomeButtonPressed()
        {
            GameManager.Instance.ChangeState(GameState.Home);
            SwitchScreen(GameScreen.GameResult);
        }
    }
}