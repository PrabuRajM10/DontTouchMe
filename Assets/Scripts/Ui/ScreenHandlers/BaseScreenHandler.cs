using System;
using Enums;
using Ui.Screens;
using UnityEngine;
using Enum = Enums.Enum;

namespace Ui.ScreenHandlers
{
    public class BaseScreenHandler : MonoBehaviour
    {

        public Enum.GameScreen respectiveScreen;
        public static event Action<Enum.GameScreen> SwitchScreenEvnt;
        
        protected void SwitchScreen(Enum.GameScreen gameScreen)
        {
            SwitchScreenEvnt?.Invoke(gameScreen);
        }
    }
}