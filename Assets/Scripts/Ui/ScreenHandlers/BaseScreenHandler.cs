using System;
using Ui.Screens;
using UnityEngine;

namespace Ui.ScreenHandlers
{
    public class BaseScreenHandler : MonoBehaviour
    {
        public static event Action<GameScreen> SwitchScreenEvnt;

        protected void SwitchScreen(GameScreen gameScreen)
        {
            SwitchScreenEvnt?.Invoke(gameScreen);
        }
    }
}