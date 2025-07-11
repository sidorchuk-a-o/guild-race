﻿using System.Collections.Generic;
using System.Linq;
using AD.Services.Localization;
using UnityEngine.AddressableAssets;

namespace Game.Instances
{
    public class InstanceInfo
    {
        public int Id { get; }

        public InstanceType Type { get; }
        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }
        public AssetReference ImageRef { get; }

        public UnitInfo[] BossUnits { get; }

        public InstanceInfo(InstanceData data, IEnumerable<UnitInfo> bossUnits)
        {
            Id = data.Id;
            Type = data.Type;
            NameKey = data.NameKey;
            DescKey = data.DescKey;
            ImageRef = data.ImageRef;
            BossUnits = bossUnits.ToArray();
        }

        public UnitInfo GetBossUnit(int id)
        {
            return BossUnits.FirstOrDefault(x => x.Id == id);
        }
    }
}