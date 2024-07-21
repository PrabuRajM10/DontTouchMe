using System;
using Ui.ScreenHandlers;
using Ui.Screens;
using UnityEngine;

namespace Ui
{
    public class MainCanvas : MonoBehaviour
    {
        [SerializeField] private BaseUi gameplayScreen;
        [SerializeField] private BaseUi settingScreen;

        [SerializeField] private GameScreen initialScreen; 
        

        private BaseUi _currentScreen;
        private void OnEnable()
        {
            BaseScreenHandler.SwitchScreenEvnt += OnSwitchScreenEvnt;
        }
        private void OnDisable()
        {
            BaseScreenHandler.SwitchScreenEvnt -= OnSwitchScreenEvnt;
        }

        private void Start()
        {
            _currentScreen = GetScreen(initialScreen);
            SetScreenVisibility(_currentScreen , true);

        }

        private void OnSwitchScreenEvnt(GameScreen screen)
        {
            if (_currentScreen != null)
            {
                SetScreenVisibility(_currentScreen , false);
            }

            _currentScreen = GetScreen(screen);
            SetScreenVisibility(_currentScreen , true);

        }

        private void SetScreenVisibility(BaseUi screen, bool state)
        {
            screen.gameObject.SetActive(state);
        }

        private BaseUi GetScreen(GameScreen screen)
        {
            switch (screen)
            {
                case GameScreen.Gameplay:
                    return gameplayScreen;
                case GameScreen.Setting:
                    return settingScreen;
                default:
                    throw new ArgumentOutOfRangeException(nameof(screen), screen, null);
            }
        }
    }
}