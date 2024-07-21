using UnityEngine;

namespace Managers
{
    public class ManagersHolder : GenericSingleton<ManagersHolder>
    {
        [SerializeField] private GameManager gameManager;
    }
}