using Managers;
using Ui.Screens;
using UnityEngine;

namespace Ui.ScreenHandlers
{
    public class HomeScreenHandler : BaseScreenHandler
    {
        [SerializeField] private HomeScreenUi homeScreen;

        private void OnEnable()
        {
            homeScreen.onPlayButtonPressed += PlayButtonPressed;
        }

        private void OnDisable()
        {
            homeScreen.onPlayButtonPressed -= PlayButtonPressed;
        }

        private void PlayButtonPressed()
        {
            SwitchScreen(GameScreen.Gameplay);
            GameManager.Instance.ChangeState(GameState.Gameplay);
        }
    }
}