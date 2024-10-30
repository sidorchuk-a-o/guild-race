namespace Game.Inventory
{
    public class EquipSlotVM : ItemSlotVM
    {
        public EquipGroup EquipGroup { get; }
        public EquipType EquipType { get; }

        public EquipSlotVM(EquipSlotInfo info, InventoryVMFactory inventoryVMF)
            : base(info, inventoryVMF)
        {
            EquipGroup = info.EquipGroup;
            EquipType = info.EquipType;
        }
    }
}