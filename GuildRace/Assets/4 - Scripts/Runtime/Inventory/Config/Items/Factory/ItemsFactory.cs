using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    public abstract class ItemsFactory : ScriptableEntity<int>
    {
        public ItemType ItemType => Id;
        public abstract Type DataType { get; }

        protected InventoryState InventoryState { get; private set; }
        protected InventoryConfig InventoryConfig { get; private set; }

        public void Init(InventoryState state, InventoryConfig config)
        {
            InventoryState = state;
            InventoryConfig = config;
        }

        // == Info ==

        public ItemInfo CreateInfo(ItemData data)
        {
            var id = GuidUtils.Generate();
            var info = CreateInfo(id, data);

            InventoryState.AddItem(info);

            return info;
        }

        protected abstract ItemInfo CreateInfo(string id, ItemData data);

        // == Save ==

        public abstract ItemSM CreateSave(ItemInfo info);

        public ItemInfo ReadSave(ItemSM save)
        {
            var data = InventoryConfig.GetItem(save.DataId);
            var info = ReadSave(data, save);

            InventoryState.AddItem(info);

            return info;
        }

        protected abstract ItemInfo ReadSave(ItemData data, ItemSM save);
    }
}