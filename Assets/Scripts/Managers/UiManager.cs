using System;
using System.Collections.Generic;
using Gameplay;
using Helpers;
using Ui.ScreenHandlers;
using Ui.Screens;
using UnityEngine; 

namespace Managers
{
    public class UiManager : GenericSingleton<UiManager>
    {
        [SerializeField] private GameScreen initialScreen; 
        [SerializeField] private GameplayScreen gameplayScreen;
        [SerializeField] private SettingScreen settingScreen;
        [SerializeField] private HomeScreenScreen homeScreen;
        [SerializeField] private GameResultScreen gameResultScreen;
        [SerializeField] private CardPickerScreen cardPickerScreen;
        [SerializeField] private PauseScreen pauseScreen;
        [SerializeField] private PopUpUI popUpUI;

        

        private BaseUi _currentScreen;
        private void OnEnable()
        {
            BaseScreenHandler.SwitchScreenEvnt += OnSwitchScreenEvnt;
            PopUp.Init(popUpUI);
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
                case GameScreen.CardPicker:
                    return cardPickerScreen;
                case GameScreen.Pause:
                    return pauseScreen;
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
            gameplayScreen.UpdateXp(xpValue);
        }

        public void OnPlayerDead()
        {
            SetGameReset(false);
        }

        public void OnPlayerCompletedLevel()
        {
            SetGameReset(true);
        }


        public void SetGameReset(bool state)
        {
            gameResultScreen.ShowResult(state);
            OnSwitchScreenEvnt(GameScreen.GameResult);
        }

        public void SetCurrentGameCards(List<CardData> powerCards)
        {
            gameplayScreen.SetCardData(powerCards);
        }

        public void SetPowerCardAvailability(int index, bool state)
        {
            gameplayScreen.SetPowerCardAvailability(index , state);

        }

        public void OnPowerCardActivated(int index, float activeTime, int xpCost)
        {
            gameplayScreen.OnPowerCardActivated(index , activeTime , xpCost);

        }

        public void PowerCardOnCoolDown(int index, float cardCooldownTime)
        {
            gameplayScreen.SetCardOnCooldown(index, cardCooldownTime);
        }
    }
}