using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Instances
{
    [Serializable]
    public class SquadParams
    {
        [SerializeField] private UnitBagData bag;
        [SerializeField] private List<SquadData> squads;

        private Dictionary<InstanceType, SquadData> squadsCache;

        public UnitBagData Bag => bag;

        public SquadData GetSquadParams(InstanceType type)
        {
            squadsCache ??= squads.ToDictionary(x => x.InstanceType, x => x);
            squadsCache.TryGetValue(type, out var data);

            return data;
        }
    }
}