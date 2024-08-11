using Managers;
using Ui.Screens;
using UnityEngine;

namespace Ui.ScreenHandlers
{
    public class HomeScreenHandler : BaseScreenHandler
    {
        [SerializeField] private HomeScreenScreen homeScreen;

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
            SwitchScreen(GameScreen.CardPicker);
            GameManager.Instance.ChangeState(GameState.CardPicker);
        }
    }
}