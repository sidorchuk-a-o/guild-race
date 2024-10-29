namespace Game.Inventory
{
    public class EquipItemVM : ItemVM
    {
        public int Level { get; }
        public int Power { get; }
        public Rarity Rarity { get; }
        public EquipType Type { get; }
        public ItemSlot Slot { get; }

        public EquipItemVM(EquipItemInfo info, InventoryVMFactory inventoryVMF) : base(info, inventoryVMF)
        {
            Level = info.Level;
            Power = info.Power;
            Rarity = info.Rarity;
            Type = info.Type;
            Slot = info.Slot;
        }
    }
}