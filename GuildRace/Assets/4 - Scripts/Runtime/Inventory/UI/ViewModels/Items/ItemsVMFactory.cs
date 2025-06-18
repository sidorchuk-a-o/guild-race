using System;
using AD.ToolsCollection;

namespace Game.Inventory
{
    public abstract class ItemsVMFactory : ScriptableData
    {
        public abstract Type DataType { get; }

        public abstract ItemVM Create(ItemInfo info, InventoryVMFactory inventoryVMF);
        public abstract ItemDataVM Create(ItemData data, InventoryVMFactory inventoryVMF);
    }
}