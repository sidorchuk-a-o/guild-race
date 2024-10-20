namespace Game.Items
{
    public class EquipItemVM : ItemVM
    {
        public int Level { get; }
        public int Power { get; }
        public Rarity Rarity { get; }
        public EquipType Type { get; }
        public EquipSlot Slot { get; }

        public EquipItemVM(EquipItemInfo info, ItemsVMFactory itemsVMF) : base(info, itemsVMF)
        {
            Level = info.Level;
            Power = info.Power;
            Rarity = info.Rarity;
            Type = info.Type;
            Slot = info.Slot;
        }
    }
}