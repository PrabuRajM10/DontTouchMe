using Gameplay;
using UnityEngine;

namespace Managers
{
    public class GameManager : GenericSingleton<GameManager>
    {
        [SerializeField] private Player player;
        public Player Player => player; 
    }
}
