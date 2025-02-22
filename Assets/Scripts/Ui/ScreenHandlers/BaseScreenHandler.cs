using System;
using Enums;
using Ui.Screens;
using UnityEngine;

namespace Ui.ScreenHandlers
{
    public class BaseScreenHandler : MonoBehaviour
    {

        public DTMEnum.GameScreen respectiveScreen;
        public static event Action<DTMEnum.GameScreen> SwitchScreenEvnt;
        
        protected void SwitchScreen(DTMEnum.GameScreen gameScreen)
        {
            SwitchScreenEvnt?.Invoke(gameScreen);
        }
    }
}