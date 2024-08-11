using Managers;
using Ui.Screens;
using UnityEngine;

namespace Ui.ScreenHandlers
{
    public class GameplayScreenHandler : BaseScreenHandler
    {
        [SerializeField] private GameplayScreen gameplayScreen;
        private void Update()
        {
            if (InputManager.Instance.IsPaused())
            {
                SwitchScreen(GameScreen.Setting);
            }
        }
    }
}