using AD.Services.Router;
using Game.Inventory;

namespace Game.Craft
{
    public class RecipeVM : ViewModel
    {
        public int Id { get; }
        public ItemDataVM ProductVM { get; }

        public RecipeVM(RecipeData data, InventoryVMFactory inventoryVMF)
        {
            Id = data.Id;
            ProductVM = inventoryVMF.CreateItemData(data.ProductItemId);
        }

        protected override void InitSubscribes()
        {
            ProductVM.AddTo(this);
        }
    }
}