using System;
using System.Collections.Generic;
using Enums;
using Gameplay;
using Helpers;
using Ui.ScreenHandlers;
using Ui.Screens;
using UnityEngine;

namespace Managers
{
    public class UiManager : GenericSingleton<UiManager>
    {
        [SerializeField] private DTMEnum.GameScreen initialScreen; 
        [SerializeField] private GameplayScreen gameplayScreen;
        [SerializeField] private SettingScreen settingScreen;
        [SerializeField] private HomeScreen homeScreen;
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

        private void OnSwitchScreenEvnt(DTMEnum.GameScreen screen)
        {
            if (_currentScreen != null)
            {
                // _currentScreen.Reset();
                SetScreenVisibility(_currentScreen , false);
            }

            _currentScreen = GetScreen(screen);
            SetScreenVisibility(_currentScreen , true);

        }

        private void SetScreenVisibility(BaseUi screen, bool state)
        {
            screen.gameObject.SetActive(state);
        }

        private BaseUi GetScreen(DTMEnum.GameScreen screen)
        {
            switch (screen)
            {
                case DTMEnum.GameScreen.Gameplay:
                    return gameplayScreen;
                case DTMEnum.GameScreen.Setting:
                    return settingScreen;
                case DTMEnum.GameScreen.Home:
                    return homeScreen;
                case DTMEnum.GameScreen.GameResult:
                    return gameResultScreen;
                case DTMEnum.GameScreen.CardPicker:
                    return cardPickerScreen;
                case DTMEnum.GameScreen.Pause:
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

        public void SetGameResult(bool successful, int currentCollectedCoins, int currentKillCount)
        {
            gameResultScreen.ShowResult(successful , currentCollectedCoins , currentKillCount);
            OnSwitchScreenEvnt(DTMEnum.GameScreen.GameResult);
        }
    }
}