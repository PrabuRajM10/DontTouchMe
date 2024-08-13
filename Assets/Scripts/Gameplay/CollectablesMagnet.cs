using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(SphereCollider))]
    public class CollectablesMagnet : MonoBehaviour
    {
        [SerializeField] private Player player;

        private void OnValidate()
        {
            if (player == null) player = transform.root.GetComponent<Player>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var collectable = other.GetComponent<Collectables>();
            if (collectable != null)
            {
                collectable.MoveTowardsTarget(player.transform);
            }
        }
    }
}