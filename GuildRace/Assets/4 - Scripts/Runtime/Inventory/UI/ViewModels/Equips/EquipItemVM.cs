using AD.Services.Router;

namespace Game.Inventory
{
    public class EquipItemVM : ItemVM
    {
        public new EquipDataVM DataVM { get; }

        public EquipItemVM(EquipItemInfo info, InventoryVMFactory inventoryVMF) : base(info, inventoryVMF)
        {
            DataVM = inventoryVMF.CreateItemData(DataId) as EquipDataVM;
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            DataVM.AddTo(this);
        }
    }
}