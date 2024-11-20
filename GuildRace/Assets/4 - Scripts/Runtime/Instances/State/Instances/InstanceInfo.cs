using AD.Services.Localization;
using UnityEngine.AddressableAssets;

namespace Game.Instances
{
    public class InstanceInfo
    {
        public int Id { get; }
        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }
        public AssetReference MapRef { get; }

        public InstanceInfo(InstanceData data)
        {
            Id = data.Id;
            NameKey = data.NameKey;
            DescKey = data.DescKey;
            MapRef = data.MapRef;
        }
    }
}