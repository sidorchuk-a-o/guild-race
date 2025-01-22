using AD.Services.Router;
using Game.Guild;
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
        private readonly IGuildService guildService;

        private readonly InventoryVMFactory inventoryVMF;

        public CraftVMFactory(
            CraftConfig craftConfig,
            ICraftService craftService,
            IGuildService guildService,
            InventoryVMFactory inventoryVMF)
        {
            this.craftConfig = craftConfig;
            this.craftService = craftService;
            this.guildService = guildService;
            this.inventoryVMF = inventoryVMF;
        }

        public VendorsVM GetVendors()
        {
            return new VendorsVM(craftService.Vendors, inventoryVMF);
        }

        public ItemDataVM GetRecipeProduct(int recipeId)
        {
            var recipeData = craftConfig.GetRecipe(recipeId);
            var itemDataVM = inventoryVMF.CreateItemData(recipeData.ProductItemId);

            return itemDataVM;
        }

        public ItemCounterVM GetReagentItemCounter(int reagentId)
        {
            var reagentsParams = craftConfig.ReagentsParams;
            var reagentCellTypes = reagentsParams.GridParams.CellTypes;

            var reagentGrids = guildService.BankTabs
                .Where(x => reagentCellTypes.Contains(x.Grid.CellType))
                .Select(x => x.Grid);

            return inventoryVMF.CreateItemCounter(reagentId, reagentGrids);
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

        // == Recycle Items ==

        public RecycleSlotVM GetRecycleSlot()
        {
            return new RecycleSlotVM(craftService.RecycleSlot, inventoryVMF);
        }

        public RecyclingResultVM GetRecyclingParams(string itemId)
        {
            var data = craftService.GetRecyclingResult(itemId);

            return data != null ? new RecyclingResultVM(data, inventoryVMF) : null;
        }
    }
}