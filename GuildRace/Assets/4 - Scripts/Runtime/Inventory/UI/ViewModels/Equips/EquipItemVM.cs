using AD.Services.Router;

namespace Game.Inventory
{
    public class EquipItemVM : ItemVM
    {
        public int Level { get; }
        public Rarity Rarity { get; }

        public EquipTypeVM TypeVM { get; }
        public EquipGroupVM GroupVM { get; }
        public ItemSlotDataVM SlotVM { get; }

        public CharacterParamsVM CharacterParamsVM { get; }

        public EquipItemVM(EquipItemInfo info, InventoryVMFactory inventoryVMF) : base(info, inventoryVMF)
        {
            Level = info.Level;
            Rarity = info.Rarity;

            SlotVM = inventoryVMF.GetSlotData(info.Slot);
            TypeVM = inventoryVMF.GetEquipType(info.Type);
            GroupVM = inventoryVMF.GetEquipGroup(info.Group);
            CharacterParamsVM = new CharacterParamsVM(info.CharacterParams);
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            SlotVM.AddTo(this);
            TypeVM.AddTo(this);
            GroupVM.AddTo(this);
            CharacterParamsVM.AddTo(this);
        }
    }
}