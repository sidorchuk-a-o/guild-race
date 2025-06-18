using AD.Services.Router;
using AD.Services.Store;
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

        public InventoryVMFactory InventoryVMF { get; }
        public StoreVMFactory StoreVMF { get; }

        public CraftVMFactory(
            CraftConfig craftConfig,
            ICraftService craftService,
            IGuildService guildService,
            InventoryVMFactory inventoryVMF,
            StoreVMFactory storeVMF)
        {
            this.craftConfig = craftConfig;
            this.craftService = craftService;
            this.guildService = guildService;

            InventoryVMF = inventoryVMF;
            StoreVMF = storeVMF;
        }

        public VendorsVM GetVendors()
        {
            return new VendorsVM(craftService.Vendors, this);
        }

        public ItemDataVM GetRecipeProduct(int recipeId)
        {
            var recipeData = craftConfig.GetRecipe(recipeId);
            var itemDataVM = InventoryVMF.CreateItemData(recipeData.ProductItemId);

            return itemDataVM;
        }

        public ItemCounterVM GetReagentItemCounter(int reagentId)
        {
            var reagentsParams = craftConfig.ReagentsParams;
            var reagentCellTypes = reagentsParams.GridParams.CellTypes;

            var reagentGrids = guildService.BankTabs
                .Where(x => reagentCellTypes.Contains(x.Grid.CellType))
                .Select(x => x.Grid);

            return InventoryVMF.CreateItemCounter(reagentId, reagentGrids);
        }

        public IReadOnlyList<IngredientVM> GetRecipeIngredients(int recipeId)
        {
            var recipeData = craftConfig.GetRecipe(recipeId);

            var ingredientsVM = recipeData.Ingredients
                .Select(x => new IngredientVM(x, this))
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
            return new RecycleSlotVM(craftService.RecycleSlot, this);
        }

        public RecyclingResultVM GetRecyclingParams(string itemId)
        {
            var result = craftService.GetRecyclingResult(itemId);

            return result switch
            {
                RecyclingItemResult item => new RecyclingItemResultVM(item, this),
                RecyclingReagentResult reagent => new RecyclingReagentResultVM(reagent, this),
                _ => throw new NotImplementedException()
            };
        }
    }
}