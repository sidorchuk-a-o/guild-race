using AD.Services.Router;
using Game.Inventory;

namespace Game.Craft
{
    public class RecyclingResultVM : ViewModel
    {
        public ItemDataVM ReagentVM { get; }
        public string RecyclingResult { get; }

        public RecyclingResultVM(RecyclingResult data, InventoryVMFactory inventoryVMF)
        {
            RecyclingResult = $"+{data.Count}";

            ReagentVM = inventoryVMF.CreateItemData(data.ReagentId);
        }

        protected override void InitSubscribes()
        {
            ReagentVM.AddTo(this);
        }
    }
}