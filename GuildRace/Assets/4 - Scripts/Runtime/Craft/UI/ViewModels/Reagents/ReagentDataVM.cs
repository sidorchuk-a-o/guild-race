using AD.Services.Router;
using Game.Inventory;

namespace Game.Craft
{
    public class ReagentDataVM : ItemDataVM
    {
        public RarityDataVM RarityVM { get; }

        public ReagentDataVM(ReagentItemData data, InventoryVMFactory inventoryVMF) : base(data, inventoryVMF)
        {
            RarityVM = inventoryVMF.GetRarity(data.Rarity);
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            RarityVM.AddTo(this);
        }
    }
}