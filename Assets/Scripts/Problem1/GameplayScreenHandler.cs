using System;
using UnityEngine;

namespace Problem1
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