using AD.ToolsCollection;
using System.Collections.Generic;
using System.Linq;

namespace Game.Inventory
{
    public class ItemsGridsFactory
    {
        private readonly InventoryState state;
        private readonly InventoryConfig config;
        private readonly IInventoryFactory inventoryFactory;

        public ItemsGridsFactory(InventoryState state, InventoryConfig config, IInventoryFactory inventoryFactory)
        {
            this.state = state;
            this.config = config;
            this.inventoryFactory = inventoryFactory;
        }

        public ItemsGridInfo CreateGrid(ItemsGridData data)
        {
            var id = GuidUtils.Generate();
            var info = new ItemsGridInfo(id, data);

            state.AddGrid(info);

            return info;
        }

        public ItemsGridSM CreateGridSave(ItemsGridInfo info)
        {
            if (info == null)
            {
                return null;
            }

            return new ItemsGridSM(info, inventoryFactory);
        }

        public ItemsGridInfo ReadGridSave(ItemsGridSM save)
        {
            if (save == null)
            {
                return null;
            }

            var info = save.GetValue(config, inventoryFactory);

            state.AddGrid(info);

            return info;
        }

        public IItemsGridsCollection CreateItemsGrids(IEnumerable<ItemsGridData> grids)
        {
            var values = grids.Select(CreateGrid);

            return new ItemsGridsCollection(values);
        }

        public ItemsGridsSM CreateItemsGridsSave(IItemsGridsCollection grids)
        {
            return new ItemsGridsSM(grids, inventoryFactory);
        }

        public IItemsGridsCollection ReadItemsGridsSave(ItemsGridsSM save)
        {
            var values = save.GetCollection(inventoryFactory);

            return new ItemsGridsCollection(values);
        }
    }
}