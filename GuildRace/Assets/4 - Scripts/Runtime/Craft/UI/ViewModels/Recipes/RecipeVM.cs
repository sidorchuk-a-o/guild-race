using AD.Services.Router;
using Game.Inventory;

namespace Game.Craft
{
    public class RecipeVM : ViewModel
    {
        public int Id { get; }
        public ItemDataVM ProductVM { get; }

        public RecipeVM(RecipeData data, CraftVMFactory craftVMF)
        {
            Id = data.Id;
            ProductVM = craftVMF.InventoryVMF.CreateItemData(data.ProductItemId);
        }

        protected override void InitSubscribes()
        {
            ProductVM.AddTo(this);
        }
    }
}