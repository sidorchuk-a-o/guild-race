using AD.ToolsCollection;
using System.Collections.Generic;
using System.Linq;

namespace Game.Inventory
{
    public class ItemSlotsFactory
    {
        private readonly InventoryState state;
        private readonly InventoryConfig config;
        private readonly IInventoryFactory inventoryFactory;

        public ItemSlotsFactory(InventoryState state, InventoryConfig config, IInventoryFactory inventoryFactory)
        {
            this.state = state;
            this.config = config;
            this.inventoryFactory = inventoryFactory;
        }

        // == Slot ==

        public ItemSlotInfo CreateInfo(ItemSlot slot)
        {
            var id = GuidUtils.Generate();
            var data = config.ItemSlotsParams.GetSlot(slot);

            var info = new ItemSlotInfo(id, data);

            state.AddSlot(info);

            return info;
        }

        public ItemSlotInfo RemoveInfo(string id)
        {
            return state.RemoveSlot(id);
        }

        public ItemSlotSM CreateSave(ItemSlotInfo info)
        {
            if (info == null)
            {
                return null;
            }

            return new ItemSlotSM(info, inventoryFactory);
        }

        public ItemSlotInfo ReadSave(ItemSlotSM save)
        {
            if (save == null)
            {
                return null;
            }

            var info = save.GetValue(config, inventoryFactory);

            state.AddSlot(info);

            return info;
        }

        // == Slots ==

        public IItemSlotsCollection CreateSlots(IReadOnlyList<ItemSlot> slots)
        {
            var values = slots.Select(CreateInfo);

            return new ItemSlotsCollection(values);
        }

        public ItemSlotsSM CreateSave(IItemSlotsCollection slots)
        {
            return new ItemSlotsSM(slots, inventoryFactory);
        }

        public IItemSlotsCollection ReadSave(ItemSlotsSM save)
        {
            var slots = save.GetValues(inventoryFactory);

            return new ItemSlotsCollection(slots);
        }
    }
}