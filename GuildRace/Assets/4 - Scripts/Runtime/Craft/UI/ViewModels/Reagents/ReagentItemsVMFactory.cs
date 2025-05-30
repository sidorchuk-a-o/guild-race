﻿using Game.Inventory;
using System;

namespace Game.Craft
{
    public class ReagentItemsVMFactory : ItemsVMFactory
    {
        public override Type InfoType { get; } = typeof(ReagentItemInfo);

        public override ItemVM Create(ItemInfo itemInfo, InventoryVMFactory inventoryVMF)
        {
            return new ReagentItemVM(itemInfo as ReagentItemInfo, inventoryVMF);
        }
    }
}