using Game.Inventory;

namespace Game.Craft
{
    public class RecycleSlotVM : ItemSlotVM
    {
        public RecycleSlotVM(RecycleSlotInfo info, InventoryVMFactory inventoryVMF)
            : base(info, inventoryVMF)
        {
        }
    }
}