using System;

namespace Game.Inventory
{
    public class EquipItemsVMFactory : ItemsVMFactory
    {
        public override Type InfoType { get; } = typeof(EquipItemInfo);

        public override ItemVM Create(ItemInfo info, InventoryVMFactory inventoryVMF)
        {
            return new EquipItemVM(info as EquipItemInfo, inventoryVMF);
        }
    }
}