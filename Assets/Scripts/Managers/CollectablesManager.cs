using UnityEngine;

namespace Managers
{
    public abstract class CollectablesManager : MonoBehaviour
    {
        public abstract void OnCollectablesCollected();
    }
}