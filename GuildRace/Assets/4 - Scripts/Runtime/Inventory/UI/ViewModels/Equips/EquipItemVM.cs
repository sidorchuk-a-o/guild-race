using AD.Services.Router;

namespace Game.Inventory
{
    public class EquipItemVM : ItemVM
    {
        public int Level { get; }
        public Rarity Rarity { get; }
        public EquipType Type { get; }
        public ItemSlot Slot { get; }
        public CharacterParamsVM CharacterParamsVM { get; }

        public EquipItemVM(EquipItemInfo info, InventoryVMFactory inventoryVMF) : base(info, inventoryVMF)
        {
            Level = info.Level;
            Rarity = info.Rarity;
            Type = info.Type;
            Slot = info.Slot;
            CharacterParamsVM = new CharacterParamsVM(info.CharacterParams);
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            CharacterParamsVM.AddTo(this);
        }
    }
}