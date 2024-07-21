using System.Collections.Generic;
using Gameplay;
using UnityEngine;

namespace Managers
{
    public class EnemyManager : GenericSingleton<EnemyManager>
    {
        [SerializeField] private List<Enemy> activeEnemyList = new List<Enemy>();
        public void AddToActiveList(Enemy enemy)
        {
            activeEnemyList.Add(enemy);
        }
    }
}