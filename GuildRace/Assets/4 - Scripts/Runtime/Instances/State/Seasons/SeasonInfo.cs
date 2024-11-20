using AD.Services.Localization;
using System.Collections.Generic;

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
    }
}