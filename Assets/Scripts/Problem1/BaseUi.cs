using UnityEngine;

namespace Problem1
{
    public enum GameScreen
    {
        Gameplay,
        Setting
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