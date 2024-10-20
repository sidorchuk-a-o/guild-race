using UnityEngine.AddressableAssets;

namespace Game.Items
{
    public abstract class ItemInfo
    {
        public string Id { get; }
        public string DataId { get; }

        public AssetReference IconRef { get; }

        public ItemInfo(string id, ItemData data)
        {
            Id = id;
            DataId = data.Id;
            IconRef = data.IconRef;
        }
    }
}