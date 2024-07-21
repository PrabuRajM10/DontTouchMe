using Managers;
using Ui.Screens;

namespace Ui.ScreenHandlers
{
    public class GameplayScreenHandler : BaseScreenHandler
    {
        private void Update()
        {
            if (InputManager.Instance.IsPaused())
            {
                SwitchScreen(GameScreen.Setting);
            }
        }
    }
}