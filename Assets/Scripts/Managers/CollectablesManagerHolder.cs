using System.Collections.Generic;
using Enums;
using Gameplay;
using UnityEngine;

namespace Managers
{
    public class CollectablesManagerHolder : GenericSingleton<CollectablesManagerHolder>
    {
        [SerializeField] private CollectablesManager coinsManager;
        [SerializeField] private CollectablesManager xPManager;

        private Dictionary<Enum.CollectablesType, CollectablesManager> collectablesDictionary =
            new Dictionary<Enum.CollectablesType, CollectablesManager>();

        public override void Awake()
        {
            base.Awake();
            collectablesDictionary.Add(Enum.CollectablesType.Coins , coinsManager);
            collectablesDictionary.Add(Enum.CollectablesType.Spell , xPManager);
        }

        public CollectablesManager GetManager(Enum.CollectablesType type)
        {
            CollectablesManager value;
            return collectablesDictionary.TryGetValue(type, out value) ? value : null;
        }
    }
}