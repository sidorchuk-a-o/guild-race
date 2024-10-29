using AD.ToolsCollection;
using System.Collections.Generic;
using System.Linq;
using VContainer;

namespace Game.Inventory
{
    public class InventoryFactory : IInventoryFactory
    {
        private readonly InventoryState state;
        private readonly InventoryConfig config;

        private readonly ItemSlotsFactory itemSlotsFactory;
        private readonly ItemsGridsFactory itemsGridsFactory;
        private readonly Dictionary<string, ItemsFactory> itemsFactories;

        public InventoryFactory(InventoryState state, InventoryConfig config, IObjectResolver resolver)
        {
            this.state = state;
            this.config = config;

            var itemsParams = config.ItemsParams;

            itemsFactories = itemsParams.Factories.ToDictionary(x => x.DataType.Name, x => x);
            itemsFactories.ForEach(x => resolver.Inject(x.Value));

            itemSlotsFactory = new(config, this);
            itemsGridsFactory = new(config, this);
        }

        // == Items ==

        public ItemInfo CreateItem(string id)
        {
            var data = config.GetItem(id);

            return CreateItem(data);
        }

        public ItemInfo CreateItem(ItemData data)
        {
            if (data == null)
            {
                return null;
            }

            var itemsFactory = GetItemsFactory(data);

            if (itemsFactory == null)
            {
                return null;
            }

            var item = itemsFactory.CreateInfo(data);

            state.AddItem(item);

            return item;
        }

        public ItemInfo RemoveItem(string itemId)
        {
            return state.RemoveItem(itemId);
        }

        public ItemSM CreateItemSave(ItemInfo info)
        {
            if (info == null)
            {
                return null;
            }

            var itemData = GetItemData(info.DataId);
            var itemsFactory = GetItemsFactory(itemData);

            if (itemsFactory == null)
            {
                return null;
            }

            return itemsFactory.CreateSave(info);
        }

        public ItemInfo ReadItemSave(ItemSM save)
        {
            if (save == null)
            {
                return null;
            }

            var itemData = GetItemData(save.DataId);
            var itemsFactory = GetItemsFactory(itemData);

            if (itemsFactory == null)
            {
                return null;
            }

            var item = itemsFactory.ReadSave(save);

            state.AddItem(item);

            return item;
        }

        private ItemData GetItemData(string itemId)
        {
            if (itemId.IsNullOrEmpty())
            {
                return null;
            }

            return config.GetItem(itemId);
        }

        private ItemsFactory GetItemsFactory(ItemData itemData)
        {
            if (itemData == null)
            {
                return null;
            }

            var typeName = itemData.GetType().Name;

            itemsFactories.TryGetValue(typeName, out var factory);

            return factory;
        }

        // == Slot ==

        public ItemSlotInfo CreateSlot(ItemSlot slot)
        {
            var info = itemSlotsFactory.CreateInfo(slot);

            state.AddSlot(info);

            return info;
        }

        public ItemSlotInfo RemoveSlot(string slotId)
        {
            return state.RemoveSlot(slotId);
        }

        public ItemSlotSM CreateSlotSave(ItemSlotInfo info)
        {
            return itemSlotsFactory.CreateSave(info);
        }

        public ItemSlotInfo ReadSlotSave(ItemSlotSM save)
        {
            var info = itemSlotsFactory.ReadSave(save);

            state.AddSlot(info);

            return info;
        }

        // == SLots ==

        public IItemSlotsCollection CreateSlots(IReadOnlyList<ItemSlot> slots)
        {
            return itemSlotsFactory.CreateSlots(slots);
        }

        public ItemSlotsSM CreateSlotsSave(IItemSlotsCollection values)
        {
            return itemSlotsFactory.CreateSave(values);
        }

        public IItemSlotsCollection ReadSlotsSave(ItemSlotsSM save)
        {
            return itemSlotsFactory.ReadSave(save);
        }

        // == Items Grid ==

        public ItemsGridInfo CreateGrid(ItemsGridData data)
        {
            var info = itemsGridsFactory.CreateGrid(data);

            state.AddGrid(info);

            return info;
        }

        public ItemsGridInfo RemoveGrid(string gridId)
        {
            return state.RemoveGrid(gridId);
        }

        public ItemsGridSM CreateGridSave(ItemsGridInfo info)
        {
            return itemsGridsFactory.CreateGridSave(info);
        }

        public ItemsGridInfo ReadGridSave(ItemsGridSM save)
        {
            var info = itemsGridsFactory.ReadGridSave(save);

            state.AddGrid(info);

            return info;
        }

        // == Items Grids ==

        public IItemsGridsCollection CreateItemsGrids(IEnumerable<ItemsGridData> grids)
        {
            return itemsGridsFactory.CreateItemsGrids(grids);
        }

        public ItemsGridsSM CreateItemsGridsSave(IItemsGridsCollection values)
        {
            return itemsGridsFactory.CreateItemsGridsSave(values);
        }

        public IItemsGridsCollection ReadItemsGridsSave(ItemsGridsSM save)
        {
            return itemsGridsFactory.ReadItemsGridsSave(save);
        }
    }
}