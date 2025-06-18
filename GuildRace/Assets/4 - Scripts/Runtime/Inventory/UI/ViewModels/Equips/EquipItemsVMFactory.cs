using System;

namespace Game.Inventory
{
    public class EquipItemsVMFactory : ItemsVMFactory
    {
        public override Type DataType { get; } = typeof(EquipItemData);

        public override ItemVM Create(ItemInfo info, InventoryVMFactory inventoryVMF)
        {
            return new EquipItemVM(info as EquipItemInfo, inventoryVMF);
        }

        public override ItemDataVM Create(ItemData data, InventoryVMFactory inventoryVMF)
        {
            return new EquipDataVM(data as EquipItemData, inventoryVMF);
        }
    }
}