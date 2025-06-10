using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    public abstract class ItemsVMFactory : ScriptableData
    {
        public abstract Type InfoType { get; }

        public abstract ItemVM Create(ItemInfo info, InventoryVMFactory inventoryVMF);
    }
}