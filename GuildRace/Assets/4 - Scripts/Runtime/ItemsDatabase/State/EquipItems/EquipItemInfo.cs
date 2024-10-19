namespace Game.Items
{
    public class EquipItemInfo : ItemInfo
    {
        private readonly EquipItemData data;

        public int Level => data.Level;
        public int Power => data.Power;
        public Rarity Rarity => data.Rarity;
        public EquipType Type => data.Type;
        public EquipSlot Slot => data.Slot;

        public EquipItemInfo(string id, EquipItemData data) : base(id, data)
        {
            this.data = data;
        }
    }
}