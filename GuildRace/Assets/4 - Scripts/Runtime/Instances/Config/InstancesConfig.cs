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
        [SerializeField] private RewardsParams rewardsParams;
        [SerializeField] private List<UnitCooldownParams> unitCooldownParams;
        [SerializeField] private List<ThreatData> threats;

        private Dictionary<int, SeasonData> seasonsCache;
        private Dictionary<int, InstanceData> instancesCache;
        private Dictionary<int, UnitData> unitsCache;
        private Dictionary<int, InstanceData> unitInstancesCache;
        private Dictionary<ThreatId, ThreatData> threatsCache;
        private Dictionary<InstanceType, InstanceTypeData> instanceTypesCache;
        private Dictionary<int, IReadOnlyCollection<InstanceRewardData>> unitRewardsCache;
        private Dictionary<InstanceType, IReadOnlyCollection<InstanceRewardData>> instanceRewardsCache;
        private Dictionary<int, InstanceRewardData> rewardsCache;

        public IReadOnlyList<SeasonData> Seasons => seasons;
        public IReadOnlyList<InstanceTypeData> InstanceTypes => instanceTypes;

        public SquadParams SquadParams => squadParams;
        public CompleteChanceParams CompleteChanceParams => completeChanceParams;
        public ConsumablesParams ConsumablesParams => consumablesParams;
        public RewardsParams RewardsParams => rewardsParams;
        public IReadOnlyList<ThreatData> Threats => threats;
        public List<UnitCooldownParams> UnitCooldownParams => unitCooldownParams;

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

        public InstanceData GetBossInstance(int bossId)
        {
            unitInstancesCache ??= seasons
                .SelectMany(x => x.Instances)
                .SelectMany(x => x.BoosUnits.Select(b => (instance: x, boss: b)))
                .ToDictionary(x => x.boss.Id, x => x.instance);

            unitInstancesCache.TryGetValue(bossId, out var instance);

            return instance;
        }

        public IReadOnlyCollection<InstanceRewardData> GetUnitRewards(int unitId)
        {
            unitRewardsCache ??= rewardsParams.Rewards
                .GroupBy(x => x.UnitId)
                .ToDictionary(x => x.Key, x => (IReadOnlyCollection<InstanceRewardData>)x.ToList());

            unitRewardsCache.TryGetValue(unitId, out var data);

            return data;
        }

        public IReadOnlyCollection<InstanceRewardData> GetInstanceRewards(InstanceType instanceType)
        {
            instanceRewardsCache ??= seasons
                .SelectMany(x => getBossesByType(x))
                .GroupBy(x => x.type)
                .ToDictionary(x => x.Key, x => getRewards(x.Select(x => x.boss)));

            static IEnumerable<(InstanceType type, UnitData boss)> getBossesByType(SeasonData x)
            {
                return x.Instances.SelectMany(i =>
                {
                    return i.BoosUnits.Select(b => (type: i.Type, boss: b));
                });
            }

            IReadOnlyCollection<InstanceRewardData> getRewards(IEnumerable<UnitData> bosses)
            {
                return bosses
                    .SelectMany(x => GetUnitRewards(x.Id))
                    .ToList();
            }

            instanceRewardsCache.TryGetValue(instanceType, out var data);

            return data;
        }

        public IEnumerable<InstanceRewardData> GetRewards(IEnumerable<int> rewardIds)
        {
            rewardsCache ??= rewardsParams.Rewards.ToDictionary(x => x.Id, x => x);

            return rewardIds
                .Select(x => rewardsCache[x])
                .Where(x => x != null);
        }

        public InstanceRewardData GetReward(int rewardId)
        {
            rewardsCache ??= rewardsParams.Rewards.ToDictionary(x => x.Id, x => x);
            rewardsCache.TryGetValue(rewardId, out var data);

            return data;
        }

        public UnitCooldownParams GetUnitCooldown(InstanceType instanceType)
        {
            return unitCooldownParams.FirstOrDefault(x => x.InstanceType == instanceType);
        }
    }
}