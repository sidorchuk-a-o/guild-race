using AD.Services.Router;
using UnityEngine;

namespace Game.Inventory
{
    public class ItemsVM : VMCollection<ItemInfo, ItemVM>
    {
        private readonly InventoryVMFactory inventoryVMF;

        public ItemsVM(IItemsCollection values, InventoryVMFactory inventoryVMF) : base(values)
        {
            this.inventoryVMF = inventoryVMF;
        }

        protected override ItemVM Create(ItemInfo value)
        {
            return inventoryVMF.CreateItem(value);
        }

        public ItemVM ElementAtOrDefault(Vector3Int positionOnGrid)
        {
            return GetOrCreate(x => x.Bounds.Value.Contains2D(positionOnGrid));
        }
    }
}