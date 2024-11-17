using Enums;
using Helpers;
using Managers;
using UnityEngine;

namespace Gameplay
{
    public class EnemySpawner : AutoSpawner
    {
        [SerializeField] private float distanceFromPlayer;

        protected override (Enum.PoolObjectTypes, Vector3) OnSpawn()
        {
            var playerPos =  GameManager.Instance.Player.GetPosition();
            var randPos = Utils.GetRandomPointAroundTarget(playerPos , distanceFromPlayer);
            var spawnPos = new Vector3(randPos.x, playerPos.y, randPos.y);

            return (spawnerObjectType , spawnPos);
        }


        protected override void HandleObjectOnReceived(Transform poolObj, Vector3 spawnPos)
        {
            base.HandleObjectOnReceived(poolObj , spawnPos);
            
            var enemy = poolObj.GetComponent<Enemy>();
            enemy.SetLookTarget();
            EnemyManager.Instance.AddToActiveList(enemy);
            
        }
    }

}