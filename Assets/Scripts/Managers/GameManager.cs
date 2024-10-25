using System;
using System.Collections.Generic;
using Gameplay;
using Ui;
using Ui.Screens;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class GameManager : GenericSingleton<GameManager>
    {
        [SerializeField] private GameState initialState;
        [SerializeField] private Player player;

        [SerializeField] private AutoSpawner enemySpawner;
        [SerializeField] private CollectablesSpawnersHolder collectablesSpawnersHolder;

        [SerializeField] private PowerCardsHandler powerCardsHandler;        

        [SerializeField] private float maxGameTimer;

        private GameState _currentState;
        private GameState _previousState;
        private List<CardData> _currentGamePowerCards;
        private int _currentCollectedCoins;
        private int _currentKillCount;

        public Player Player => player;
        public GameState CurrentState => _currentState;
        public GameState PreviousState => _previousState;

        public static event Action<GameState> OnBeforeStateChange; 
        public static event Action<GameState> OnAfterStateChange;

        public static event Action<bool> OnGameEnd; 

        private void Start()
        {
            ChangeState(initialState);
        }

        private void OnEnable()
        {
            player.Dead += OnPlayerDead;
            GameTimer.OnGameTimerDone += GameTimerDone;
        }


        private void OnDisable()
        {
            player.Dead -= OnPlayerDead;
            GameTimer.OnGameTimerDone -= GameTimerDone;
        }
        private void GameTimerDone()
        {
            EndGame(true);
        }

        private void OnPlayerDead()
        {
            EndGame(false);
        }

        void EndGame(bool successful)
        {
            ChangeState(GameState.GameResult);
            UiManager.Instance.SetGameReset(successful , _currentCollectedCoins , _currentKillCount);
            GameEnd(successful);
        }

        public void GameEnd(bool successful)
        {
            OnGameEnd?.Invoke(successful);
            GameTimer.StopTimer();
            EnemyManager.Instance.DisableAllEnemies();
            StopSpawning();
            _currentCollectedCoins = 0;
            _currentKillCount = 0;
        }
        

        void StopSpawning()
        {
            StopEnemySpawning();
            collectablesSpawnersHolder.StopSpawning();
        }

        public void StopEnemySpawning()
        {
            enemySpawner.StopSpawning();
        }
        
        public void StartEnemySpawning()
        {
            enemySpawner.StartAutoSpawning();
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
                case GameState.CardPicker:
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
            Debug.Log("[HandleOnGameplay]");
            StartEnemySpawning();
            collectablesSpawnersHolder.StartSpawning();
            GameTimer.StartTimer(this ,maxGameTimer);
        }

        public void SetPowerCardsForTheGame(List<CardData> powerCards)
        {
            _currentGamePowerCards = powerCards;
            UiManager.Instance.SetCurrentGameCards(powerCards);
        }

        public List<CardData> GetCurrentPowerCards()
        {
            return _currentGamePowerCards;
        }

        public bool IsAnyCardActive()
        {
            return powerCardsHandler.IsAnyCardActive();
        }

        public void UpdateCollectedCoins(int value)
        {
            _currentCollectedCoins = value;
        }
        public void UpdateKills(int value)
        {
            _currentKillCount = value;
        }
    }
    
    public enum GameState
    {
        GameStart,
        Gameplay,
        Home,
        GameResult,
        CardPicker
    }
}
