namespace Game.Inventory
{
    public class EquipSlotInfo : ItemSlotInfo
    {
        public EquipGroup EquipGroup { get; }
        public EquipType EquipType { get; private set; }

        public EquipSlotInfo(string id, EquipSlotData data) : base(id, data)
        {
            EquipGroup = data.EquipGroup;
        }

        public void SetEquipType(EquipType value)
        {
            EquipType = value;
        }
    }
}