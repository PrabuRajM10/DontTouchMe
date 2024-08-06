using System;
using UnityEngine;

namespace Ui.Screens
{
    public enum GameScreen
    {
        Gameplay,
        Setting,
        Home,
        GameResult
    }
    public abstract class BaseUi : MonoBehaviour
    {
        [SerializeField] private GameScreen screen;

        private void OnDisable()
        {
            Reset();
        }

        public GameScreen Screen
        {
            get => screen;
            set => screen = value;
        }

        public abstract void Reset();
    }
}