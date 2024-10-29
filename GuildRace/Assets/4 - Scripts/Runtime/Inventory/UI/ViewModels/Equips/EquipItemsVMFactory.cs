using System;

namespace Game.Inventory
{
    public class EquipItemsVMFactory : ItemsVMFactory
    {
        public override Type InfoType { get; } = typeof(EquipItemInfo);

        public override ItemVM Create(ItemInfo itemInfo, InventoryVMFactory inventoryVMF)
        {
            return new EquipItemVM(itemInfo as EquipItemInfo, inventoryVMF);
        }
    }
}