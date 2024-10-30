using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    public abstract class ItemsFactory : ScriptableData
    {
        public abstract Type DataType { get; }

        protected InventoryState State { get; private set; }
        protected InventoryConfig Config { get; private set; }

        public void Init(InventoryState state, InventoryConfig config)
        {
            State = state;
            Config = config;
        }

        // == Info ==

        public ItemInfo CreateInfo(ItemData data)
        {
            var id = GuidUtils.Generate();
            var info = CreateInfo(id, data);

            State.AddItem(info);

            return info;
        }

        protected abstract ItemInfo CreateInfo(string id, ItemData data);

        // == Save ==

        public abstract ItemSM CreateSave(ItemInfo info);

        public ItemInfo ReadSave(ItemSM save)
        {
            var data = Config.GetItem(save.DataId);
            var info = ReadSave(data, save);

            State.AddItem(info);

            return info;
        }

        protected abstract ItemInfo ReadSave(ItemData data, ItemSM save);
    }
}