using AD.Services.Router;
using Game.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Craft
{
    public class CraftVMFactory : VMFactory
    {
        private readonly CraftConfig craftConfig;
        private readonly ICraftService craftService;
        private readonly InventoryVMFactory inventoryVMF;

        public CraftVMFactory(
            CraftConfig craftConfig,
            ICraftService craftService,
            InventoryVMFactory inventoryVMF)
        {
            this.craftConfig = craftConfig;
            this.craftService = craftService;
            this.inventoryVMF = inventoryVMF;
        }

        public VendorsVM GetVendors()
        {
            return new VendorsVM(craftService.Vendors);
        }

        public ItemDataVM GetRecipeProduct(int recipeId)
        {
            var recipeData = craftConfig.GetRecipe(recipeId);
            var itemDataVM = inventoryVMF.CreateItemData(recipeData.ProductItemId);

            return itemDataVM;
        }

        public IReadOnlyList<IngredientVM> GetRecipeIngredients(int recipeId)
        {
            var recipeData = craftConfig.GetRecipe(recipeId);

            var ingredientsVM = recipeData.Ingredients
                .Select(x => new IngredientVM(x, inventoryVMF))
                .ToList();

            return ingredientsVM;
        }

        public void StartCraftingProcess(StartCraftingEM craftingEM)
        {
            craftService.StartCraftingProcess(craftingEM);
        }
    }
}