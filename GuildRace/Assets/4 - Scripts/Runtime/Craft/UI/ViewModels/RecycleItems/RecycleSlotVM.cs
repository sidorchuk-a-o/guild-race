using Game.Inventory;

namespace Game.Craft
{
    public class RecycleSlotVM : ItemSlotVM
    {
        public RecycleSlotVM(RecycleSlotInfo info, CraftVMFactory craftVMF)
            : base(info, craftVMF.InventoryVMF)
        {
        }
    }
}