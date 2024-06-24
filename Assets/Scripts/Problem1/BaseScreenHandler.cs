using System;
using UnityEngine;

namespace Problem1
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