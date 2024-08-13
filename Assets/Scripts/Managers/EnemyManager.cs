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

        public void DisableAllEnemies()
        {
            foreach (var enemy in activeEnemyList)
            {
                enemy.BackToPool();
            }
            activeEnemyList.Clear();
        }

        public void SetEnemiesSpeedMultiplier(float multiplier)
        {
            foreach (var enemy in activeEnemyList)
            {
                enemy.SetSpeedMultiplier(multiplier);
            }
        }

        public void ResetEnemiesSpeed()
        {
            foreach (var enemy in activeEnemyList)
            {
                enemy.ResetSpeed();
            }
        }

        public void SetEnemiesScaleMultiplier(float scale)
        {
            foreach (var enemy in activeEnemyList)
            {
                enemy.SetScaleMultiplier(scale);
            }
        }

        public void ResetEnemiesScale()
        {
            foreach (var enemy in activeEnemyList)
            {
                enemy.ResetScale();
            }
        }

        public void FreezeEnemies(bool freeze)
        {
            foreach (var enemy in activeEnemyList)
            {
                enemy.FreezeMovement(freeze);
            }
        }
    }
}