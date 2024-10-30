using AD.Services.Router;

namespace Game.Inventory
{
    public class ItemSlotsVM : VMCollection<ItemSlotInfo, ItemSlotVM>
    {
        private readonly InventoryVMFactory inventoryVMF;

        public ItemSlotVM this[ItemSlot slot] => GetOrCreate(x => x.Slot == slot);

        public ItemSlotsVM(IItemSlotsCollection values, InventoryVMFactory inventoryVMF) : base(values)
        {
            this.inventoryVMF = inventoryVMF;
        }

        protected override ItemSlotVM Create(ItemSlotInfo value)
        {
            return inventoryVMF.CreateSlot(value);
        }
    }
}