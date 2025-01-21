using Game.Inventory;

namespace Game.Craft
{
    public class RemoveItemSlotVM : ItemSlotVM
    {
        public RemoveItemSlotVM(RemoveItemSlotInfo info, InventoryVMFactory inventoryVMF)
            : base(info, inventoryVMF)
        {
        }
    }
}