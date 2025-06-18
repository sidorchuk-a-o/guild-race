using AD.Services.Router;

namespace Game.Inventory
{
    public class EquipDataVM : ItemDataVM
    {
        public int Level { get; }

        public RarityDataVM RarityVM { get; }
        public EquipTypeVM TypeVM { get; }
        public EquipGroupVM GroupVM { get; }
        public ItemSlotDataVM SlotVM { get; }

        public CharacterParamsVM CharacterParamsVM { get; }

        public EquipDataVM(EquipItemData data, InventoryVMFactory inventoryVMF) : base(data, inventoryVMF)
        {
            Level = data.Level;

            RarityVM = inventoryVMF.GetRarity(data.Rarity);
            TypeVM = inventoryVMF.GetEquipType(data.Type);
            GroupVM = inventoryVMF.GetEquipGroup(data.Group);
            SlotVM = inventoryVMF.GetSlotData(data.Slot);
            CharacterParamsVM = new(data.CharacterParams);
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            RarityVM.AddTo(this);
            TypeVM.AddTo(this);
            GroupVM.AddTo(this);
            SlotVM.AddTo(this);
            CharacterParamsVM.AddTo(this);
        }
    }
}