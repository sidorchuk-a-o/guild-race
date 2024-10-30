namespace Game.Inventory
{
    public class EquipItemInfo : ItemInfo
    {
        public int Level { get; }
        public int Power { get; }

        public Rarity Rarity { get; }
        public EquipType Type { get; }

        public EquipItemInfo(string id, EquipItemData data) : base(id, data)
        {
            Level = data.Level;
            Power = data.Power;
            Rarity = data.Rarity;
            Type = data.Type;
        }

        public override bool CheckSlotParams(ItemSlotInfo itemSlot)
        {
            return base.CheckSlotParams(itemSlot)
                && itemSlot is EquipSlotInfo equipSlot
                && equipSlot.EquipType == Type;
        }
    }
}