using System;
using Enums;
using Managers;
using Ui.Screens;
using UnityEngine;
using Enum = Enums.Enum;

namespace Ui.ScreenHandlers
{
    public class GameplayScreenHandler : BaseScreenHandler
    {
        [SerializeField] private GameplayScreen gameplayScreen;

        private void OnEnable()
        {
            SpellManager.OnSpellCollectedForFirstTime += SpellCollectedForFirstTime;
        }

        private void OnDisable()
        {
            SpellManager.OnSpellCollectedForFirstTime -= SpellCollectedForFirstTime;
        }

        private void SpellCollectedForFirstTime()
        {
            gameplayScreen.ShowFirstSpellCollectionAnimation();
        }

        private void Update()
        {
            if (InputManager.Instance.IsPaused() && GameManager.Instance.CurrentState == GameState.Gameplay)
            {
                Time.timeScale = 0;
                SwitchScreen(Enum.GameScreen.Pause);
            }
        }
    }
}