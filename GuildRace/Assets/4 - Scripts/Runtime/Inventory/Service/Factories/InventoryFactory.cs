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

        private readonly Dictionary<string, ItemsFactory> itemsFactories;
        private readonly Dictionary<string, ItemSlotsFactory> slotsFactories;
        private readonly Dictionary<string, ItemsGridsFactory> gridsFactories;

        public InventoryFactory(InventoryState state, InventoryConfig config, IObjectResolver resolver)
        {
            this.state = state;
            this.config = config;

            var itemsParams = config.ItemsParams;
            var slotsParams = config.ItemSlotsParams;
            var gridsParams = config.ItemsGridParams;

            itemsFactories = itemsParams.Factories.ToDictionary(x => x.DataType.Name, x => x);
            itemsFactories.ForEach(x =>
            {
                resolver.Inject(x.Value);

                x.Value.Init(state, config);
            });

            slotsFactories = slotsParams.Factories.ToDictionary(x => x.DataType.Name, x => x);
            slotsFactories.ForEach(x =>
            {
                resolver.Inject(x.Value);

                x.Value.Init(state, config, this);
            });

            gridsFactories = gridsParams.Factories.ToDictionary(x => x.DataType.Name, x => x);
            gridsFactories.ForEach(x =>
            {
                resolver.Inject(x.Value);

                x.Value.Init(state, config, this);
            });
        }

        // == Items ==

        public ItemInfo CreateItem(int dataId)
        {
            var data = config.GetItem(dataId);

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

            return itemsFactory.CreateInfo(data);
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

            return itemsFactory.ReadSave(save);
        }

        private ItemData GetItemData(int itemId)
        {
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

        public ItemSlotInfo CreateSlot(int id)
        {
            var data = config.GetSlot(id);

            return CreateSlot(data);
        }

        public ItemSlotInfo CreateSlot(ItemSlotData data)
        {
            if (data == null)
            {
                return null;
            }

            var slotsFactory = GetSlotsFactory(data);

            if (slotsFactory == null)
            {
                return null;
            }

            return slotsFactory.CreateInfo(data);
        }

        public ItemSlotInfo RemoveSlot(string slotId)
        {
            return state.RemoveSlot(slotId);
        }

        public ItemSlotSM CreateSlotSave(ItemSlotInfo info)
        {
            if (info == null)
            {
                return null;
            }

            var slotData = GetSlotData(info.DataId);
            var slotsFactory = GetSlotsFactory(slotData);

            if (slotsFactory == null)
            {
                return null;
            }

            return slotsFactory.CreateSave(info);
        }

        public ItemSlotInfo ReadSlotSave(ItemSlotSM save)
        {
            if (save == null)
            {
                return null;
            }

            var slotData = GetSlotData(save.DataId);
            var slotsFactory = GetSlotsFactory(slotData);

            if (slotsFactory == null)
            {
                return null;
            }

            return slotsFactory.ReadSave(save);
        }

        private ItemSlotData GetSlotData(int slotId)
        {
            return config.GetSlot(slotId);
        }

        private ItemSlotsFactory GetSlotsFactory(ItemSlotData slotData)
        {
            if (slotData == null)
            {
                return null;
            }

            var typeName = slotData.GetType().Name;

            slotsFactories.TryGetValue(typeName, out var factory);

            return factory;
        }

        // == Items Grid ==

        public ItemsGridInfo CreateGrid(ItemsGridData data)
        {
            var gridsFactory = GetGridsFactory(data);

            return gridsFactory.CreateGrid(data);
        }

        public ItemsGridInfo RemoveGrid(string gridId)
        {
            return state.RemoveGrid(gridId);
        }

        public ItemsGridSM CreateGridSave(ItemsGridInfo info)
        {
            var gridData = GetGridData(info.DataId);
            var gridsFactory = GetGridsFactory(gridData);

            return gridsFactory.CreateGridSave(info);
        }

        public ItemsGridInfo ReadGridSave(ItemsGridSM save)
        {
            var gridData = GetGridData(save.DataId);
            var gridsFactory = GetGridsFactory(gridData);

            return gridsFactory.ReadGridSave(save);
        }

        private ItemsGridData GetGridData(int gridId)
        {
            return config.GetGrid(gridId);
        }

        private ItemsGridsFactory GetGridsFactory(ItemsGridData gridData)
        {
            if (gridData == null)
            {
                return null;
            }

            var typeName = gridData.GetType().Name;

            gridsFactories.TryGetValue(typeName, out var factory);

            return factory;
        }

        // == Grids ==

        public IItemsGridsCollection CreateItemsGrids(IEnumerable<ItemsGridData> grids)
        {
            var values = grids.Select(CreateGrid);

            return new ItemsGridsCollection(values);
        }

        public ItemsGridsSM CreateItemsGridsSave(IItemsGridsCollection grids)
        {
            return new ItemsGridsSM(grids, this);
        }

        public IItemsGridsCollection ReadItemsGridsSave(ItemsGridsSM save)
        {
            var values = save.GetCollection(this);

            return new ItemsGridsCollection(values);
        }
    }
}