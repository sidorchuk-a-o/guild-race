using AD.Services.Localization;
using System.Collections.Generic;
using System.Linq;

namespace Game.Instances
{
    public class SeasonInfo
    {
        public int Id { get; }
        public LocalizeKey NameKey { get; }

        public InstanceInfo Raid { get; }
        public IInstancesCollection Dungeons { get; }

        public SeasonInfo(SeasonData data, InstanceInfo raid, IEnumerable<InstanceInfo> dungeons)
        {
            Id = data.Id;
            NameKey = data.NameKey;

            Raid = raid;
            Dungeons = new InstancesCollection(dungeons);
        }

        public InstanceInfo GetInstanceById(int instanceId)
        {
            if (Raid.Id == instanceId)
            {
                return Raid;
            }

            return Dungeons.FirstOrDefault(x => x.Id == instanceId);
        }

        public IEnumerable<InstanceInfo> GetInstances()
        {
            yield return Raid;

            foreach (var instance in Dungeons)
            {
                yield return instance;
            }
        }
    }
}