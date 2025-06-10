using AD.Services.Router;

namespace Game.Inventory
{
    public class EquipItemVM : ItemVM
    {
        public int Level { get; }

        public RarityDataVM RarityVM { get; }
        public EquipTypeVM TypeVM { get; }
        public EquipGroupVM GroupVM { get; }
        public ItemSlotDataVM SlotVM { get; }

        public CharacterParamsVM CharacterParamsVM { get; }

        public EquipItemVM(EquipItemInfo info, InventoryVMFactory inventoryVMF) : base(info, inventoryVMF)
        {
            Level = info.Level;

            RarityVM = inventoryVMF.GetRarity(info.Rarity);
            SlotVM = inventoryVMF.GetSlotData(info.Slot);
            TypeVM = inventoryVMF.GetEquipType(info.Type);
            GroupVM = inventoryVMF.GetEquipGroup(info.Group);
            CharacterParamsVM = new CharacterParamsVM(info.CharacterParams);
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            RarityVM.AddTo(this);
            SlotVM.AddTo(this);
            TypeVM.AddTo(this);
            GroupVM.AddTo(this);
            CharacterParamsVM.AddTo(this);
        }
    }
}