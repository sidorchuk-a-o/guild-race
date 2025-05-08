using System.Linq;
using AD.Services.Localization;

namespace Game.Instances
{
    public class InstanceInfo
    {
        public int Id { get; }

        public InstanceType Type { get; }
        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }

        public UnitInfo[] BossUnits { get; }

        public InstanceInfo(InstanceData data)
        {
            Id = data.Id;
            Type = data.Type;
            NameKey = data.NameKey;
            DescKey = data.DescKey;
            BossUnits = data.BoosUnits.Select(x => new UnitInfo(x)).ToArray();
        }

        public UnitInfo GetBossUnit(int id)
        {
            return BossUnits.FirstOrDefault(x => x.Id == id);
        }
    }
}