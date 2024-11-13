namespace Game.Inventory
{
    public class EquipItemInfo : ItemInfo
    {
        public int Level { get; }

        public Rarity Rarity { get; }
        public EquipType Type { get; }

        public CharacterParams CharacterParams { get; }

        public EquipItemInfo(string id, EquipItemData data) : base(id, data)
        {
            Level = data.Level;
            Rarity = data.Rarity;
            Type = data.Type;
            CharacterParams = data.CharacterParams;
        }

        public override bool CheckSlotParams(ItemSlotInfo itemSlot)
        {
            return base.CheckSlotParams(itemSlot)
                && itemSlot is EquipSlotInfo equipSlot
                && equipSlot.EquipType == Type;
        }
    }
}