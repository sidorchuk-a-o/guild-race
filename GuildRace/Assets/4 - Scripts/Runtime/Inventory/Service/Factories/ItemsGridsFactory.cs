using AD.ToolsCollection;
using System.Collections.Generic;
using System.Linq;

namespace Game.Inventory
{
    public class ItemsGridsFactory
    {
        private readonly InventoryConfig config;
        private readonly IInventoryFactory inventoryFactory;

        public ItemsGridsFactory(InventoryConfig config, IInventoryFactory inventoryFactory)
        {
            this.config = config;
            this.inventoryFactory = inventoryFactory;
        }

        public ItemsGridInfo CreateGrid(ItemsGridData data)
        {
            var id = GuidUtils.Generate();

            return new ItemsGridInfo(id, data);
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

            return save.GetValue(config, inventoryFactory);
        }

        public IItemsGridsCollection CreateItemsGrids(IEnumerable<ItemsGridData> grids)
        {
            var values = grids.Select(inventoryFactory.CreateGrid);

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