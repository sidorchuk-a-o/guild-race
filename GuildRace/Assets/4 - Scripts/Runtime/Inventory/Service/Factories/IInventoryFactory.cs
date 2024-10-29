using System.Collections.Generic;

namespace Game.Inventory
{
    public interface IInventoryFactory
    {
        ItemInfo CreateItem(ItemData data);
        ItemSM CreateItemSave(ItemInfo info);
        ItemInfo ReadItemSave(ItemSM save);

        ItemSlotInfo CreateSlot(ItemSlot slot);
        ItemSlotSM CreateSlotSave(ItemSlotInfo info);
        ItemSlotInfo ReadSlotSave(ItemSlotSM save);

        IItemSlotsCollection CreateSlots(IReadOnlyList<ItemSlot> slots);
        ItemSlotsSM CreateSlotsSave(IItemSlotsCollection values);
        IItemSlotsCollection ReadSlotsSave(ItemSlotsSM save);

        ItemsGridInfo CreateGrid(ItemsGridData data);
        ItemsGridSM CreateGridSave(ItemsGridInfo info);
        ItemsGridInfo ReadGridSave(ItemsGridSM save);

        IItemsGridsCollection CreateItemsGrids(IEnumerable<ItemsGridData> grids);
        ItemsGridsSM CreateItemsGridsSave(IItemsGridsCollection values);
        IItemsGridsCollection ReadItemsGridsSave(ItemsGridsSM save);
    }
}