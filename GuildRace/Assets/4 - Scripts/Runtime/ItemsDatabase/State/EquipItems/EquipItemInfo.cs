namespace Game.Items
{
    public class EquipItemInfo : ItemInfo
    {
        public int Level { get; }
        public int Power { get; }
        public Rarity Rarity { get; }
        public EquipType Type { get; }
        public EquipSlot Slot { get; }

        public EquipItemInfo(string id, EquipItemData data) : base(id, data)
        {
            Level = data.Level;
            Power = data.Power;
            Rarity = data.Rarity;
            Type = data.Type;
            Slot = data.Slot;
        }
    }
}