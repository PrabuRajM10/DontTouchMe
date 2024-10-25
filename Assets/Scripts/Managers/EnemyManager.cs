using System;
using System.Collections.Generic;
using Gameplay;
using Ui;
using UnityEngine;

namespace Managers
{
    public class EnemyManager : GenericSingleton<EnemyManager>
    {
        [SerializeField] private EnemyProperties enemyPropertiesSo;
        [SerializeField] private List<Enemy> activeEnemyList = new List<Enemy>();
        [SerializeField] private int maxEnemiesCountOnGame;
        [SerializeField] private int minEnemiesCountOnGame;

        [SerializeField] private int deadEnemiesCount;

        private void OnEnable()
        {
            GameManager.OnGameEnd += OnGameEnd;
        }

        private void OnDisable()
        {
            GameManager.OnGameEnd -= OnGameEnd;
        }

        private void OnGameEnd(bool obj)
        {
            deadEnemiesCount = 0;
        }

        public void AddToActiveList(Enemy enemy)
        {
            activeEnemyList.Add(enemy);
            
            if(activeEnemyList.Count >= maxEnemiesCountOnGame) GameManager.Instance.StopEnemySpawning();
        }

        public void EnemyDead(Enemy enemy)
        {
            deadEnemiesCount++;
            GameManager.Instance.UpdateKills(deadEnemiesCount);
            UiManager.Instance.UpdateKillCount(deadEnemiesCount);
            activeEnemyList.Remove(enemy);
            if(activeEnemyList.Count <= minEnemiesCountOnGame) GameManager.Instance.StartEnemySpawning();
        }

        public void DisableAllEnemies()
        {
            foreach (var enemy in activeEnemyList)
            {
                enemy.BackToPool();
            }
            activeEnemyList.Clear();
        }

        public float GetEnemySpeed()
        {
            return enemyPropertiesSo.EnemySpeed;
        }
        public float GetEnemyScale()
        {
            return enemyPropertiesSo.EnemyScale;
        }

        public void SetEnemiesSpeedMultiplier(float multiplier)
        {
            enemyPropertiesSo.EnemySpeedMultiplier = multiplier;
            UpdateEnemiesSpeed();
        }

        void UpdateEnemiesSpeed()
        {
            foreach (var enemy in activeEnemyList)
            {
                enemy.SetSpeed(GetEnemySpeed());    
            }
        }

        public void SetEnemiesScaleMultiplier(float multiplier)
        {
            enemyPropertiesSo.EnemyScaleMultiplier = multiplier;
            UpdateEnemiesScale();
        }

        void UpdateEnemiesScale()
        {
            foreach (var enemy in activeEnemyList)
            {
                enemy.SetScale(GetEnemyScale());
            }
        }

        public void FreezeEnemies(bool freeze)
        {
            Debug.LogFormat("[FE] [PowerCardsHandler] [FreezeEnemies] freeze {0}" ,freeze);
            SetEnemiesSpeedMultiplier(freeze ? 0 : 1);
        }
    }
}