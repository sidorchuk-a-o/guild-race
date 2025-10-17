using System.Collections.Generic;
using System.Linq;
using AD.Services.Localization;
using UnityEngine.AddressableAssets;

namespace Game.Instances
{
    public class InstanceInfo
    {
        public int Id { get; }
        public string Title { get; }

        public InstanceType Type { get; }
        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }
        public AssetReference ImageRef { get; }
        public AssetReference LoadingRef { get; }

        public UnitInfo[] BossUnits { get; }

        public InstanceInfo(InstanceData data, IEnumerable<UnitInfo> bossUnits)
        {
            Id = data.Id;
            Type = data.Type;
            Title = data.Title;
            NameKey = data.NameKey;
            DescKey = data.DescKey;
            ImageRef = data.ImageRef;
            LoadingRef = data.LoadingRef;
            BossUnits = bossUnits.ToArray();
        }

        public UnitInfo GetBossUnit(int id)
        {
            return BossUnits.FirstOrDefault(x => x.Id == id);
        }
    }
}