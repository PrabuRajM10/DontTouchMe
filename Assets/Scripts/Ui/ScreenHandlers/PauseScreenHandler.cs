using System;
using Helpers;
using Managers;
using Ui.Screens;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ui.ScreenHandlers
{
    public class PauseScreenHandler : BaseScreenHandler
    {
        [FormerlySerializedAs("_pauseScreen")] [SerializeField] private PauseScreen pauseScreen;

        private void OnEnable()
        {
            pauseScreen.OnHomeButtonPressed += OnHomeButtonPressed;
            pauseScreen.OnResumeButtonPressed += OnResumeButtonPressed;
            pauseScreen.OnQuitButtonPressed += OnQuitButtonPressed;
        }

        private void OnDisable()
        {
            pauseScreen.OnHomeButtonPressed -= OnHomeButtonPressed;
            pauseScreen.OnResumeButtonPressed -= OnResumeButtonPressed;
            pauseScreen.OnQuitButtonPressed -= OnQuitButtonPressed;
        }

        private void OnQuitButtonPressed()
        {
            Debug.Log("[PauseScreenHandler] [OnQuitButtonPressed] ");
            Utils.QuitGame();
        }

        private void OnResumeButtonPressed()
        {
            Time.timeScale = 1;
            SwitchScreen(GameScreen.Gameplay);
        }

        private void OnHomeButtonPressed()
        {
            Time.timeScale = 1;
            GameManager.Instance.GameEnd(false);
            SwitchScreen(GameScreen.Home);
            GameManager.Instance.ChangeState(GameState.Home);
        }
    }
}