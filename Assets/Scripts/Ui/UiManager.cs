using System;
using Gameplay;
using Helpers;
using Managers;
using Ui.ScreenHandlers;
using Ui.Screens;
using UnityEngine;

namespace Ui
{
    public class UiManager : GenericSingleton<UiManager>
    {
        [SerializeField] private GameScreen initialScreen; 
        [SerializeField] private GameplayUi gameplayScreen;
        [SerializeField] private SettingUi settingScreen;
        [SerializeField] private HomeScreenUi homeScreen;
        [SerializeField] private GameResultScreen gameResultScreen;

        

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

        private void Update()
        {
            if(_currentScreen != gameplayScreen)return;
            UpdateGameTimer(GameTimer.GetTimeString());
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
                case GameScreen.Home:
                    return homeScreen;
                case GameScreen.GameResult:
                    return gameResultScreen;
                default:
                    throw new ArgumentOutOfRangeException(nameof(screen), screen, null);
            }
        }

        public void OnCoinCollected(int coinsCount)
        {
            Debug.Log("[UiManager] [OnCoinCollected] coinsCount " + coinsCount);
            gameplayScreen.UpdateScore(coinsCount);
        }

        private void UpdateGameTimer(string time)
        {
            gameplayScreen.UpdateTimer(time);
        }

        public void UpdateKillCount(int killCount)
        {
            gameplayScreen.UpdateKills(killCount);
        }

        public void OnXpCollected(int xpValue)
        {
            Debug.Log("[UiManager] [OnXpCollected] xpValue " + xpValue);
        }

        public void OnPlayerDead()
        {
            gameResultScreen.ShowResult(false);
            OnSwitchScreenEvnt(GameScreen.GameResult);
        }
    }
}