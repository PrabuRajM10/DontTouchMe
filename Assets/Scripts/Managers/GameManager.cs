using System;
using Gameplay;
using Ui;
using Unity.VisualScripting;
using UnityEngine;

namespace Managers
{
    public class GameManager : GenericSingleton<GameManager>
    {
        [SerializeField] private GameState initialState;
        [SerializeField] private Player player;

        [SerializeField] private AutoSpawner enemySpawner;
        [SerializeField] private CollectablesSpawnersHolder collectablesSpawnersHolder;

        [SerializeField] private float maxGameTimer;

        private GameState _currentState;
        private GameState _previousState;

        public Player Player => player;
        public GameState CurrentState => _currentState;
        public GameState PreviousState => _previousState;

        public static event Action<GameState> OnBeforeStateChange; 
        public static event Action<GameState> OnAfterStateChange;

        private void Start()
        {
            ChangeState(initialState);
        }

        private void OnEnable()
        {
            player.Dead += OnPlayerDead;
        }

        private void OnDisable()
        {
            player.Dead -= OnPlayerDead;
        }

        private void OnPlayerDead()
        {
            UiManager.Instance.OnPlayerDead();
            EnemyManager.Instance.OnPlayerDead();
            enemySpawner.StopSpawning();
            collectablesSpawnersHolder.StopSpawning();
        }

        public void ChangeState(GameState nextState)
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
                    HandleOnGameplay();
                    break;
                case GameState.Home:
                    break;
                case GameState.GameResult:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            OnAfterStateChange?.Invoke(_currentState);
            
        }

        private void HandleOnGameStart()
        {
            
        }

        void HandleOnGameplay()
        {
            enemySpawner.StartAutoSpawning();
            collectablesSpawnersHolder.StartSpawning();
            GameTimer.StartTimer(this ,maxGameTimer);
        }
    }
    
    public enum GameState
    {
        GameStart,
        Gameplay,
        Home,
        GameResult
    }
}
