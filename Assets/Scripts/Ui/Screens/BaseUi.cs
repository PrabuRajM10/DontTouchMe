using UnityEngine;

namespace Ui.Screens
{
    public enum GameScreen
    {
        Gameplay,
        Setting,
        Home
    }
    public class BaseUi : MonoBehaviour
    {
        [SerializeField] private GameScreen screen;

        public GameScreen Screen
        {
            get => screen;
            set => screen = value;
        }
    }
}