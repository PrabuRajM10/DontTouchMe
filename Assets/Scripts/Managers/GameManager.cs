using System;
using Gameplay;
using Ui;
using Unity.VisualScripting;
using UnityEngine;

namespace Managers
{
    public class GameManager : GenericSingleton<GameManager>
    {
        [SerializeField] private Player player;

        private GameState _currentState;
        private GameState _previousState;

        public Player Player => player;
        public GameState CurrentState => _currentState;
        public GameState PreviousState => _previousState;

        public static event Action<GameState> OnBeforeStateChange; 
        public static event Action<GameState> OnAfterStateChange;

        private void OnEnable()
        {
            CoinsManager.OnCoinCollected += OnCoinCollected;
            XpManager.OnXpCollected += OnXpCollected;
        }

        private void OnDisable()
        {
            CoinsManager.OnCoinCollected -= OnCoinCollected;
            XpManager.OnXpCollected -= OnXpCollected;
        }

        private void OnCoinCollected(int coinsCount)
        {
            UiManager.Instance.OnCoinCollected(coinsCount);
        }
        
        private void OnXpCollected(int xpValue)
        {
            UiManager.Instance.OnXpCollected(xpValue);
        }

        private void Start()
        {
            ChangeState(GameState.GameStart);
        }
        void ChangeState(GameState nextState)
        {
            if(nextState == _currentState ) return;

            _previousState = _currentState;
            OnBeforeStateChange?.Invoke(_currentState);
            _currentState = nextState;

            switch (_currentState)
            {
                case GameState.GameStart:
                    HandleOnGameStart();
                    break;
                case GameState.Gameplay:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            OnAfterStateChange?.Invoke(_currentState);
            
        }

        private void HandleOnGameStart()
        {
            
        }
    }
    
    public enum GameState
    {
        GameStart,
        Gameplay
    }
}
