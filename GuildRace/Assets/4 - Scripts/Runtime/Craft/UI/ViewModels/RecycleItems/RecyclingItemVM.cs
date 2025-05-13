using AD.Services.Router;
using Cysharp.Threading.Tasks;
using Game.Inventory;

namespace Game.Craft
{
    public class RecyclingItemVM : ViewModel
    {
        public ItemDataVM ReagentVM { get; }
        public string RecyclingResult { get; }

        public RecyclingItemVM(RecyclingItemInfo info, CraftVMFactory craftVMF)
        {
            RecyclingResult = $"+{info.Count}";
            ReagentVM = craftVMF.InventoryVMF.CreateItemData(info.ReagentId);
        }

        protected override void InitSubscribes()
        {
            ReagentVM.AddTo(this);
        }
    }
}