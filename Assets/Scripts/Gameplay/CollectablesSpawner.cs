using Enums;
using Helpers;
using Managers;
using UnityEngine;

namespace Gameplay
{
    
    public class CollectablesSpawner : AutoSpawner
    {
        [SerializeField] private float distanceFromPlayer = 50f;

        protected override (DTMEnum.PoolObjectTypes, Vector3) OnSpawn()
        {
            var playerPos =  GameManager.Instance.Player.GetPosition();
            var randPos = Utils.GetRandomPointAroundTarget(playerPos , distanceFromPlayer);
            var spawnPos = new Vector3(randPos.x, playerPos.y, randPos.y);

            return (spawnerObjectType , spawnPos);
        }
    }
}