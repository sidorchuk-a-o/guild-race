using AD.Services.Router;
using Game.Inventory;

namespace Game.Craft
{
    public class RecipesVM : VMCollection<RecipeData, RecipeVM>
    {
        private readonly InventoryVMFactory inventoryVMF;

        public RecipesVM(IRecipesCollection values, InventoryVMFactory inventoryVMF) : base(values)
        {
            this.inventoryVMF = inventoryVMF;
        }

        protected override RecipeVM Create(RecipeData value)
        {
            return new RecipeVM(value, inventoryVMF);
        }
    }
}