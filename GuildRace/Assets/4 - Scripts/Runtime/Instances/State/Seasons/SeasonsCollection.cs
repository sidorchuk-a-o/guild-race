using System.Collections.Generic;
using System.Linq;
using AD.States;

namespace Game.Instances
{
    public class SeasonsCollection : ReactiveCollectionInfo<SeasonInfo>, ISeasonsCollection
    {
        private Dictionary<int, InstanceInfo> instancesCache;
        private Dictionary<int, UnitInfo> bossUnitsCache;

        public SeasonsCollection(IEnumerable<SeasonInfo> values) : base(values)
        {
        }

        public SeasonInfo GetSeason(int id)
        {
            return this.FirstOrDefault(x => x.Id == id);
        }

        public InstanceInfo GetInstance(int id)
        {
            instancesCache = Values
                .SelectMany(x => x.GetInstances())
                .ToDictionary(x => x.Id, x => x);

            instancesCache.TryGetValue(id, out var instance);

            return instance;
        }

        public UnitInfo GetBossUnit(int id)
        {
            bossUnitsCache = Values
                .SelectMany(x => x.GetInstances())
                .SelectMany(x => x.BossUnits)
                .ToDictionary(x => x.Id, x => x);

            bossUnitsCache.TryGetValue(id, out var unit);

            return unit;
        }
    }
}