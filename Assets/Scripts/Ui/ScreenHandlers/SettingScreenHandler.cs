using Enums;
using Ui.Screens;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ui.ScreenHandlers
{
    public class SettingScreenHandler : BaseScreenHandler
    {
        [SerializeField] private SettingScreen settingScreen;

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
            SwitchScreen(Enum.GameScreen.Gameplay);
        }
    }
}