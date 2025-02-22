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

        private Dictionary<DTMEnum.CollectablesType, CollectablesManager> collectablesDictionary =
            new Dictionary<DTMEnum.CollectablesType, CollectablesManager>();

        public override void Awake()
        {
            base.Awake();
            collectablesDictionary.Add(DTMEnum.CollectablesType.Coins , coinsManager);
            collectablesDictionary.Add(DTMEnum.CollectablesType.Spell , xPManager);
        }

        public CollectablesManager GetManager(DTMEnum.CollectablesType type)
        {
            CollectablesManager value;
            return collectablesDictionary.TryGetValue(type, out value) ? value : null;
        }
    }
}