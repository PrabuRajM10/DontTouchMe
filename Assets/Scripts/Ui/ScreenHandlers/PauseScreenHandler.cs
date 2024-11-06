using System;
using Enums;
using Helpers;
using Managers;
using Ui.Screens;
using UnityEngine;
using UnityEngine.Serialization;
using Enum = Enums.Enum;

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
            PopUp.ShowPopUp("Attention" , "Do you sure you want to quit" , Enum.PopUpType.YES_NO , () =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else           
            Application.Quit();
#endif
            } , () =>
            {
                PopUp.ClosePopUp();
                SwitchScreen(Enum.GameScreen.Pause);
            });
        }

        private void OnResumeButtonPressed()
        {
            Time.timeScale = 1;
            SwitchScreen(Enum.GameScreen.Gameplay);
        }

        private void OnHomeButtonPressed()
        {
            Time.timeScale = 1;
            GameManager.Instance.GameEnd(false);
            SwitchScreen(Enum.GameScreen.Home);
            GameManager.Instance.ChangeState(GameState.Home);
        }
    }
}