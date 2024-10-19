using UnityEngine.AddressableAssets;

namespace Game.Items
{
    public abstract class ItemInfo
    {
        private readonly ItemData data;

        public string Id { get; }
        public string DataId => data.Id;

        public AssetReference IconRef => data.IconRef;

        public ItemInfo(string id, ItemData data)
        {
            Id = id;

            this.data = data;
        }
    }
}