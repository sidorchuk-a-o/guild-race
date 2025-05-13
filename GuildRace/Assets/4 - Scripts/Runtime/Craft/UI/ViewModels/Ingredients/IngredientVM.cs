using AD.Services.Router;
using Game.Inventory;

namespace Game.Craft
{
    public class IngredientVM : ViewModel
    {
        public ItemDataVM ReagentVM { get; }
        public int Count { get; }

        public IngredientVM(IngredientData data, CraftVMFactory craftVMF)
        {
            ReagentVM = craftVMF.InventoryVMF.CreateItemData(data.ReagentId);
            Count = data.Count;
        }

        protected override void InitSubscribes()
        {
        }
    }
}