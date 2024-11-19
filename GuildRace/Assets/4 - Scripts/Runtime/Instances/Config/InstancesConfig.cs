using AD.ToolsCollection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Instances
{
    public class InstancesConfig : ScriptableConfig
    {
        [SerializeField] private List<SeasonData> seasons;
        [SerializeField] private List<InstanceTypeData> instanceTypes;

        private Dictionary<int, SeasonData> seasonsCache;
        private Dictionary<int, InstanceData> instancesCache;
        private Dictionary<InstanceType, InstanceTypeData> instanceTypesCache;

        public IReadOnlyList<SeasonData> Seasons => seasons;
        public IReadOnlyList<InstanceTypeData> InstanceTypes => instanceTypes;

        public SeasonData GetSeason(int id)
        {
            seasonsCache ??= seasons.ToDictionary(x => x.Id, x => x);

            return seasonsCache[id];
        }

        public InstanceData GetInstance(int id)
        {
            instancesCache ??= seasons
                .SelectMany(x => x.Instances)
                .ToDictionary(x => x.Id, x => x);

            return instancesCache[id];
        }

        public InstanceTypeData GetInstanceType(InstanceType type)
        {
            instanceTypesCache ??= instanceTypes.ToDictionary(x => (InstanceType)x.Id, x => x);

            return instanceTypesCache[type];
        }
    }
}