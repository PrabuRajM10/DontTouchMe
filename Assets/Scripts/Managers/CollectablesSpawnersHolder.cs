using System;
using Gameplay;
using UnityEngine;

namespace Managers
{
    public class CollectablesSpawnersHolder : MonoBehaviour
    {
        [SerializeField] private AutoSpawner[] collectablesSpawners;

        public void StartSpawning()
        {
            foreach (var collectablesSpawner in collectablesSpawners)
            {
                collectablesSpawner.StartAutoSpawning();
            }
        }

        public void StopSpawning()
        {
            foreach (var spawner in collectablesSpawners)
            {
                spawner.StopSpawning();
            }
        }
    }
}