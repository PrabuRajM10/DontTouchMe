using UnityEngine;

namespace Gameplay
{
    public abstract class AutoSpawner : Spawner
    {
        [SerializeField] private float minSpawnInterval;
        [SerializeField] private float maxSpawnInterval;

        private float _spawnInterval;
        private bool _canSpawn;
        private float _currentTime;

        private void Start()
        {
            _spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        }

        public void StartAutoSpawning()
        {
            _canSpawn = true;
            _currentTime = _spawnInterval;
        }
    
        public void StopSpawning()
        {
            _canSpawn = false;
            _currentTime = 0;
        }
        private void Update()
        {
            if(!_canSpawn) return;

            if (_currentTime <= _spawnInterval)
            {
                _currentTime += Time.deltaTime;
            }
            else
            {
                Spawn(OnSpawn().type, OnSpawn().position);
                _currentTime = 0;
                _spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            }
        }

        protected abstract (PoolObjectTypes type ,Vector3 position) OnSpawn(); // return spawn pos
    }
}