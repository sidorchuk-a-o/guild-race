using AD.Services.Localization;
using AD.Services.Router;
using UnityEngine.AddressableAssets;

namespace Game.Instances
{
    public class InstanceVM : ViewModel
    {
        public int Id { get; }
        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }

        public AssetReference UIRef { get; }

        public InstanceVM(InstanceInfo info)
        {
            Id = info.Id;
            NameKey = info.NameKey;
            DescKey = info.DescKey;
            UIRef = info.UIRef;
        }

        protected override void InitSubscribes()
        {
        }
    }
}