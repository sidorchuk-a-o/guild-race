using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    public abstract class ItemSlotsFactory : ScriptableData
    {
        public abstract Type DataType { get; }

        protected InventoryState State { get; private set; }
        protected InventoryConfig Config { get; private set; }
        protected InventoryFactory InventoryFactory { get; private set; }

        public void Init(InventoryState state, InventoryConfig config, InventoryFactory inventoryFactory)
        {
            State = state;
            Config = config;
            InventoryFactory = inventoryFactory;
        }

        // == Info ==

        public ItemSlotInfo CreateInfo(ItemSlotData data)
        {
            var id = GuidUtils.Generate();
            var info = CreateInfo(id, data);

            State.AddSlot(info);

            return info;
        }

        protected abstract ItemSlotInfo CreateInfo(string id, ItemSlotData data);

        // == Save ==

        public abstract ItemSlotSM CreateSave(ItemSlotInfo info);

        public ItemSlotInfo ReadSave(ItemSlotSM save)
        {
            var data = Config.GetSlot(save.DataId);
            var info = ReadSave(data, save);

            State.AddSlot(info);

            return info;
        }

        protected abstract ItemSlotInfo ReadSave(ItemSlotData data, ItemSlotSM save);
    }
}