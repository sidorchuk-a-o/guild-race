using AD.Services.Router;
using Game.Inventory;

namespace Game.Craft
{
    public class IngredientVM : ViewModel
    {
        public ItemDataVM ReagentVM { get; }
        public int Count { get; }

        public IngredientVM(IngredientData data, InventoryVMFactory inventoryVMF)
        {
            ReagentVM = inventoryVMF.CreateItemData(data.ReagentId);
            Count = data.Count;
        }

        protected override void InitSubscribes()
        {
        }
    }
}