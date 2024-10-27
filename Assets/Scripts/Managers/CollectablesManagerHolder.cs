using System.Collections.Generic;
using Gameplay;
using UnityEngine;

namespace Managers
{
    public class CollectablesManagerHolder : GenericSingleton<CollectablesManagerHolder>
    {
        [SerializeField] private CollectablesManager coinsManager;
        [SerializeField] private CollectablesManager xPManager;

        private Dictionary<CollectablesType, CollectablesManager> collectablesDictionary =
            new Dictionary<CollectablesType, CollectablesManager>();

        public override void Awake()
        {
            base.Awake();
            collectablesDictionary.Add(CollectablesType.Coins , coinsManager);
            collectablesDictionary.Add(CollectablesType.Spell , xPManager);
        }

        public CollectablesManager GetManager(CollectablesType type)
        {
            CollectablesManager value;
            return collectablesDictionary.TryGetValue(type, out value) ? value : null;
        }
    }
}