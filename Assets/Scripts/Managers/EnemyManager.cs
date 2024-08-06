using System;
using System.Collections.Generic;
using Gameplay;
using Ui;
using UnityEngine;

namespace Managers
{
    public class EnemyManager : GenericSingleton<EnemyManager>
    {
        [SerializeField] private List<Enemy> activeEnemyList = new List<Enemy>();

        [SerializeField] private int deadEnemiesCount;

        public void AddToActiveList(Enemy enemy)
        {
            activeEnemyList.Add(enemy);
        }

        public void EnemyDead(Enemy enemy)
        {
            deadEnemiesCount++;
            UiManager.Instance.UpdateKillCount(deadEnemiesCount);
        }

        public void OnPlayerDead()
        {
            foreach (var enemy in activeEnemyList)
            {
                enemy.BackToPool();
            }
            activeEnemyList.Clear();
        }
    }
}