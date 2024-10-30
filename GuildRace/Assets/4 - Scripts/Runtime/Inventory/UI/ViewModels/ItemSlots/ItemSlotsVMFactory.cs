using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    public abstract class ItemSlotsVMFactory : ScriptableData
    {
        public abstract Type InfoType { get; }

        public abstract ItemSlotVM Create(ItemSlotInfo info, InventoryVMFactory inventoryVMF);
    }
}