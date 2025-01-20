using AD.Services.Localization;
using AD.Services.Router;
using Game.Inventory;

namespace Game.Craft
{
    public class VendorVM : ViewModel
    {
        public int Id { get; }
        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }

        public RecipesVM RecipesVM { get; }

        public VendorVM(VendorInfo info, InventoryVMFactory inventoryVMF)
        {
            Id = info.Id;
            NameKey = info.NameKey;
            DescKey = info.DescKey;
            RecipesVM = new(info.Recipes, inventoryVMF);
        }

        protected override void InitSubscribes()
        {
            RecipesVM.AddTo(this);
        }
    }
}