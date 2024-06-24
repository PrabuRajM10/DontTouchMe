using System;
using UnityEngine;

namespace Problem1
{
    public class SettingScreenHandler : BaseScreenHandler
    {
        [SerializeField] private SettingUi settingScreen;

        private void OnEnable()
        {
            settingScreen.OnOkButtonPressed += OkButtonPressed;
        }

        private void OnDisable()
        {
            settingScreen.OnOkButtonPressed -= OkButtonPressed;
        }

        private void OkButtonPressed()
        {
            SwitchScreen(GameScreen.Gameplay);
        }
    }
}