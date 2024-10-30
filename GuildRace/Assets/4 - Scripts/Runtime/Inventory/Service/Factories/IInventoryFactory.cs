using System.Collections.Generic;

namespace Game.Inventory
{
    public interface IInventoryFactory
    {
        ItemInfo CreateItem(ItemData data);
        ItemSM CreateItemSave(ItemInfo info);
        ItemInfo ReadItemSave(ItemSM save);

        ItemSlotInfo CreateSlot(ItemSlotData data);
        ItemSlotSM CreateSlotSave(ItemSlotInfo info);
        ItemSlotInfo ReadSlotSave(ItemSlotSM save);

        ItemsGridInfo CreateGrid(ItemsGridData data);
        ItemsGridSM CreateGridSave(ItemsGridInfo info);
        ItemsGridInfo ReadGridSave(ItemsGridSM save);

        IItemsGridsCollection CreateItemsGrids(IEnumerable<ItemsGridData> grids);
        ItemsGridsSM CreateItemsGridsSave(IItemsGridsCollection values);
        IItemsGridsCollection ReadItemsGridsSave(ItemsGridsSM save);
    }
}