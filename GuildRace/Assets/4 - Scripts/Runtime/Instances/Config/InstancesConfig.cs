using AD.ToolsCollection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Instances
{
    public class InstancesConfig : ScriptableConfig
    {
        // seasons
        [SerializeField] private List<SeasonData> seasons;
        [SerializeField] private List<InstanceTypeData> instanceTypes;
        // params
        [SerializeField] private SquadParams squadParams;
        [SerializeField] private CompleteChanceParams completeChanceParams;
        [SerializeField] private ConsumablesParams consumablesParams;
        [SerializeField] private List<ThreatData> threats;

        private Dictionary<int, SeasonData> seasonsCache;
        private Dictionary<int, InstanceData> instancesCache;
        private Dictionary<int, UnitData> unitsCache;
        private Dictionary<ThreatId, ThreatData> threatsCache;
        private Dictionary<InstanceType, InstanceTypeData> instanceTypesCache;

        public IReadOnlyList<SeasonData> Seasons => seasons;
        public IReadOnlyList<InstanceTypeData> InstanceTypes => instanceTypes;

        public SquadParams SquadParams => squadParams;
        public CompleteChanceParams CompleteChanceParams => completeChanceParams;
        public ConsumablesParams ConsumablesParams => consumablesParams;
        public IReadOnlyList<ThreatData> Threats => threats;

        public SeasonData GetSeason(int id)
        {
            seasonsCache ??= seasons.ToDictionary(x => x.Id, x => x);
            seasonsCache.TryGetValue(id, out var data);

            return data;
        }

        public InstanceData GetInstance(int id)
        {
            instancesCache ??= seasons
                .SelectMany(x => x.Instances)
                .ToDictionary(x => x.Id, x => x);

            instancesCache.TryGetValue(id, out var data);

            return data;
        }

        public InstanceTypeData GetInstanceType(InstanceType type)
        {
            instanceTypesCache ??= instanceTypes.ToDictionary(x => (InstanceType)x.Id, x => x);
            instanceTypesCache.TryGetValue(type, out var data);

            return data;
        }

        public ThreatData GetThreat(ThreatId id)
        {
            threatsCache ??= threats.ToDictionary(x => (ThreatId)x.Id, x => x);
            threatsCache.TryGetValue(id, out var data);

            return data;
        }

        public UnitData GetBossUnit(int id)
        {
            unitsCache ??= seasons
                .SelectMany(x => x.Instances)
                .SelectMany(x => x.BoosUnits)
                .ToDictionary(x => x.Id, x => x);

            unitsCache.TryGetValue(id, out var data);

            return data;
        }
    }
}